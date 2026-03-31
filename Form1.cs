using Microsoft.Data.SqlClient;
using System.Collections;
using System.Net.Mail;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Progetto_Naso
{
    public partial class Form1 : Form
    {
        public string cS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dallantoniav\\Desktop\\NasoV01\\DataBase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
        public class Persona
        {
            public int Id;
            public string Nome;
            public string Cognome;
            public string citta;
            public string e_mail;
        }

        List<Persona> ListPers = new List<Persona>();

        public Form1()
        {
            InitializeComponent();
            // Associa il bottone Read per caricare i dati nella textBox
            button2.Click += button2_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(cS))
            {
                conn.Open();
                string query = "SELECT * FROM Persone";
                var cmd = new SqlCommand(query, conn);
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Persona p = new Persona();
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
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in ListPers)
            {
                comboBox1.Items.Add(item.Nome);
            }   
        }
    }
}
