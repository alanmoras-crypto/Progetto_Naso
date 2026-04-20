using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Progetto_Naso
{
    public partial class FormProprietari : Form
    {
        SqlDataAdapter adapter;
        DataTable tabellaDati;
        SqlCommandBuilder builder; 

        public string cS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\morasa\\Desktop\\NasoV01\\DataBase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public FormProprietari()
        {
            InitializeComponent();
        }

        private void FormProprietari_Load(object sender, EventArgs e)
        {
            CaricaDati();
        }

        private void CaricaDati()
        {
            try
            {
                
                string query = "SELECT * FROM Proprietario";

                adapter = new SqlDataAdapter(query, cS);

                
                builder = new SqlCommandBuilder(adapter);

                tabellaDati = new DataTable();
                adapter.Fill(tabellaDati);

                tabellaDati.Columns["Id"].AutoIncrement = true;
                tabellaDati.Columns["Id"].AutoIncrementSeed = -1;
                tabellaDati.Columns["Id"].AutoIncrementStep = -1;

                dataGridView1.DataSource = tabellaDati;

                // --- ESTETICA ---
                if (dataGridView1.Columns.Contains("Id"))
                {
                   dataGridView1.Columns["Id"].Visible = false; // NASCONDE ID
                }

                dataGridView1.Columns["e_mail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns["Cognome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento: " + ex.Message);
            }
        }

        private void Salva_Click(object sender, EventArgs e)
        {
            try
            {
                // Salva le modifiche (Update, Insert, Delete)
                adapter.Update(tabellaDati);
                MessageBox.Show("Dati salvati con successo!");

                // Opzionale: ricarica per vedere gli ID reali del DB invece di quelli negativi
                CaricaDati();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio: " + ex.Message);
            }
        }

        private void Cancella_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
                {
                    // 1. Rimuove la riga dalla Grid
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                    // 2. Invia subito la cancellazione al Database
                    adapter.Update(tabellaDati);

                    MessageBox.Show("Eliminato correttamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la cancellazione: " + ex.Message);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}