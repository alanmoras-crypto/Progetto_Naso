using Microsoft.Data.SqlClient;
using System.Collections;
using System.Net.Mail;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Progetto_Naso
{
    public partial class Form1 : Form
    {
        public string cS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\morasa\\Desktop\\NasoV01\\DataBase.mdf;Integrated Security=True;Connect Timeout=30";
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
                {
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

            }

            // Popola la ComboBox una sola volta dopo aver caricato i dati
            comboBox1.Items.Clear();
            foreach (var item in ListPers)
            {
                comboBox1.Items.Add(item.Nome);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mostra i dettagli dell'elemento selezionato nella textBox
            if (comboBox1.SelectedIndex < 0) return;
            var selected = ListPers[comboBox1.SelectedIndex];
            string query = "SELECT * FROM Proprietario WHERE IdMacchina = @IdMacchina";
            using (var conn = new SqlConnection(cS))
            {
                conn.Open();
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdMacchina", selected.Id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"ID: {reader.GetInt32(0)}");
                        sb.AppendLine($"Nome: {reader.GetString(1)}");
                        sb.AppendLine($"Cognome: {reader.GetString(2)}");
                        sb.AppendLine($"Città: {reader.GetString(3)}");
                        sb.AppendLine($"E-mail: {reader.GetString(4)}");
                        
                    }
                }
            }
        }
    }

}