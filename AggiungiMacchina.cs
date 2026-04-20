using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Progetto_Naso
{
    public partial class AggiungiMacchina : Form
    {
        // Stringa di connessione al database locale
        public string cS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\morasa\\Desktop\\NasoV01\\DataBase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        // Classi di supporto con ID per gestire le relazioni nel database
        public class Proprietari
        {
            public int Id;
            public string Nome;
            public string Cognome;

            public override string ToString() => Nome + " " + Cognome;
        }

        public class SensoreInfo
        {
            public int Id;
            public string NomeSensore;

            public override string ToString() => NomeSensore;
        }

        List<Proprietari> ListProp = new List<Proprietari>();
        List<SensoreInfo> ListSens = new List<SensoreInfo>();

        public AggiungiMacchina()
        {
            InitializeComponent();
        }

        private void AggiungiMacchina_Load(object sender, EventArgs e)
        {
            CaricaDatiIniziali();
            CaricaGriglia();
        }

        private void CaricaDatiIniziali()
        {
            using (var conn = new SqlConnection(cS))
            {
                conn.Open();

                // 1. Caricamento Proprietari
                string queryProp = "SELECT Id, Nome, Cognome FROM Proprietario";
                using (var cmdProp = new SqlCommand(queryProp, conn))
                {
                    using (var reader = cmdProp.ExecuteReader())
                    {
                        ListProp.Clear();
                        while (reader.Read())
                        {
                            ListProp.Add(new Proprietari
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Cognome = reader.GetString(2)
                            });
                        }
                    }
                }

                // 2. Caricamento Sensori
                string querySens = "SELECT Id_Sensore, Sensori FROM Sensore";
                using (var cmdSens = new SqlCommand(querySens, conn))
                {
                    using (var reader = cmdSens.ExecuteReader())
                    {
                        ListSens.Clear();
                        while (reader.Read())
                        {
                            ListSens.Add(new SensoreInfo
                            {
                                Id = reader.GetInt32(0),
                                NomeSensore = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            // Popolamento controlli UI
            comboBox1.Items.Clear();
            comboBox1.Items.Add("NESSUN PROPRIETARIO");
            foreach (var p in ListProp) comboBox1.Items.Add(p);
            comboBox1.SelectedIndex = 0;

            checkedListBox1.Items.Clear();
            foreach (var s in ListSens) checkedListBox1.Items.Add(s);
        }

        private void CaricaGriglia()
        {
            using (var conn = new SqlConnection(cS))
            {
                conn.Open();
                // Utilizzo di LEFT JOIN per visualizzare anche macchine senza proprietario (NULL)
                // Utilizzo di TRIM per pulire gli spazi bianchi dai dati
                string queryGrid = @"
                    SELECT Macchina.note, 
                           Macchina.Data_Creazione, 
                           Proprietario.Nome, 
                           Proprietario.Cognome, 
                           TRIM(Proprietario.e_mail) AS [e_mail]
                    FROM Macchina 
                    LEFT JOIN AbbMacchinaProp ON Macchina.Id = AbbMacchinaProp.IdMacchina 
                    LEFT JOIN Proprietario ON Proprietario.Id = AbbMacchinaProp.Id_Proprietario";

                using (var cmdGrid = new SqlCommand(queryGrid, conn))
                {
                    DataTable dt = new DataTable();
                    dt.Load(cmdGrid.ExecuteReader());
                    MacchineVisual.DataSource = dt;
                    MacchineVisual.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        private void AGGIUNGI_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Inserire una nota.");
                return;
            }

            using (var conn = new SqlConnection(cS))
            {
                conn.Open();

                // 1. Inserimento Macchina e recupero nuovo ID
                string queryInsMac = "INSERT INTO Macchina (note, Data_Creazione) OUTPUT INSERTED.Id VALUES (@n, @d)";
                int newId;
                using (var cmd = new SqlCommand(queryInsMac, conn))
                {
                    cmd.Parameters.AddWithValue("@n", textBox1.Text);
                    cmd.Parameters.AddWithValue("@d", DateTime.Now);
                    newId = (int)cmd.ExecuteScalar();
                }

                // 2. Abbinamento Proprietario (solo se selezionato != NESSUN PROPRIETARIO)
                if (comboBox1.SelectedIndex > 0)
                {
                    var p = (Proprietari)comboBox1.SelectedItem;
                    string queryAbbP = "INSERT INTO AbbMacchinaProp (IdMacchina, Id_Proprietario, Data_Abb) VALUES (@m, @p, @d)";
                    using (var cmd = new SqlCommand(queryAbbP, conn))
                    {
                        cmd.Parameters.AddWithValue("@m", newId);
                        cmd.Parameters.AddWithValue("@p", p.Id);
                        cmd.Parameters.AddWithValue("@d", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }

                // 3. Abbinamento Sensori spuntati
                foreach (SensoreInfo s in checkedListBox1.CheckedItems)
                {
                    string queryAbbS = "INSERT INTO AbbSensMacchina (IdMacchina, IdSensore, Data_Abb) VALUES (@m, @s, @d)";
                    using (var cmd = new SqlCommand(queryAbbS, conn))
                    {
                        cmd.Parameters.AddWithValue("@m", newId);
                        cmd.Parameters.AddWithValue("@s", s.Id);
                        cmd.Parameters.AddWithValue("@d", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Reset e aggiornamento
            CaricaGriglia();
            textBox1.Clear();
            comboBox1.SelectedIndex = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++) checkedListBox1.SetItemChecked(i, false);

            MessageBox.Show("Registrazione completata!");
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}