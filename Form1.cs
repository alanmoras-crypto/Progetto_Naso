using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace Progetto_Naso
{
    public partial class Form1 : Form
    {
        public string cS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\morasa\\Desktop\\NasoV01\\DataBase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public class Proprietario
        {
            public int Id;
            public string Nome;
            public string Cognome;
            public string citta;
            public string e_mail;
        }
        List<Proprietario> ListPers = new List<Proprietario>();

        public Form1()
        {
            InitializeComponent();
            button2.Click += button2_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(cS))
            {
                conn.Open();
                string query = "SELECT * FROM Proprietario";
                var cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Proprietario p = new Proprietario();
                        p.Id = reader.GetInt32(0);
                        p.Nome = reader.GetString(1);
                        p.Cognome = reader.GetString(2);
                        p.citta = reader.GetString(3);
                        p.e_mail = reader.GetString(4);
                        ListPers.Add(p);
                    }
                }
            }

            comboBox1.Items.Clear();
            foreach (var item in ListPers)
            {
                comboBox1.Items.Add(item.Nome);
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormProprietari FormProprietari = new FormProprietari();
            FormProprietari.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;
            var selected = ListPers[comboBox1.SelectedIndex];

            using (var conn = new SqlConnection(cS))
            {
                conn.Open();


                string queryDati = @"
                    SELECT 
                        Dati.Data_e_Ora, 
                        Macchina.note, 
                        Sensore.Sensori, 
                        Dati.Dato 
                    FROM Dati 
                    INNER JOIN Sensore ON Dati.IdSensore = Sensore.Id_Sensore 
                    INNER JOIN Macchina ON Dati.IdMacchina = Macchina.Id
                    INNER JOIN AbbMacchinaProp ON AbbMacchinaProp.IdMacchina = Dati.IdMacchina 
                    INNER JOIN Proprietario ON Proprietario.Id = AbbMacchinaProp.Id_Proprietario 
                    WHERE Proprietario.Nome = @nome";

                using (var cmd1 = new SqlCommand(queryDati, conn))
                {
                    cmd1.Parameters.AddWithValue("@nome", selected.Nome);
                    using (var reader = cmd1.ExecuteReader())
                    {
                        var dt1 = new DataTable();
                        dt1.Load(reader);
                        dataGridView1.DataSource = dt1;
                    }
                }


                string queryMacchine = @"
                    SELECT 
                        Macchina.note, 
                        Macchina.Data_Creazione 
                    FROM Macchina 
                    INNER JOIN AbbMacchinaProp ON Macchina.Id = AbbMacchinaProp.IdMacchina 
                    INNER JOIN Proprietario ON Proprietario.Id = AbbMacchinaProp.Id_Proprietario 
                    WHERE Proprietario.Nome = @nome";

                using (var cmd2 = new SqlCommand(queryMacchine, conn))
                {
                    cmd2.Parameters.AddWithValue("@nome", selected.Nome);
                    using (var reader = cmd2.ExecuteReader())
                    {
                        var dt2 = new DataTable();
                        dt2.Load(reader);
                        dataGridView2.DataSource = dt2;
                    }
                }
            }
        }

        private void Macchine_Click(object sender, EventArgs e)
        {
            AggiungiMacchina FormAggiungiMacchina = new AggiungiMacchina();
            FormAggiungiMacchina.ShowDialog();
        }
    }
}