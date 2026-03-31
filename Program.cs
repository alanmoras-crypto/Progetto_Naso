using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MQTTnet;
using MQTTnet.Client;

class Program
{
    // --------------------------------------------------------
    // CLASSE DATI: Fa da "stampo" per tradurre il JSON
    // --------------------------------------------------------
    public class DatoSensore
    {
        public int IdSensore { get; set; }
        public int IdMacchina { get; set; }
        public string Dato { get; set; }
    }

    // La tua stringa di connessione globale
    static string stringaConnessione = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\morasa\Desktop\Naso\DataBase.mdf;Integrated Security=True;Connect Timeout=30";

    static async Task Main()
    {
        Console.WriteLine("Avvio programma MQTT in formato JSON...");

        // --------------------------------------------------------
        // CONFIGURAZIONE E CONNESSIONE MQTT
        // --------------------------------------------------------
        string broker = "broker.hivemq.com";
        int porta = 1883;
        string topic = "2IoT-vfa";

        var factory = new MqttFactory();
        var client = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(broker, porta)
            .WithCleanSession()
            .Build();

        var result = await client.ConnectAsync(options);
        if (result.ResultCode != MqttClientConnectResultCode.Success)
        {
            Console.WriteLine("Connessione MQTT fallita!");
            return;
        }

        await client.SubscribeAsync(topic);
        Console.WriteLine("Connesso! In ascolto sul topic: " + topic);

        // --------------------------------------------------------
        // RICEZIONE E TRADUZIONE DEL MESSAGGIO
        // --------------------------------------------------------
        client.ApplicationMessageReceivedAsync += async e =>
        {
            string testoRicevuto = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            Console.WriteLine($"\n[MQTT] Messaggio Ricevuto: {testoRicevuto}");

            try
            {
                DatoSensore datoSpacchettato = JsonSerializer.Deserialize<DatoSensore>(testoRicevuto);

                if (datoSpacchettato != null)
                {
                    await SalvaNelDatabase(datoSpacchettato);
                }
            }
            catch (JsonException)
            {
                Console.WriteLine(" -> [ERRORE]: Il messaggio ricevuto non era un JSON valido.");
            }
        };

        // --------------------------------------------------------
        // MENU INTERATTIVO DA TASTIERA
        // --------------------------------------------------------
        Console.WriteLine("\nCOMANDI TASTIERA:");
        Console.WriteLine("[L]     -> Leggi e mostra tutti i dati dal Database");
        Console.WriteLine("[ESC]   -> Esci dal programma\n");

        while (true)
        {
            var tasto = Console.ReadKey(true);

            if (tasto.Key == ConsoleKey.Escape)
            {
                break;
            }

            if (tasto.Key == ConsoleKey.L)
            {
                await LeggiDalDatabase();
            }
        }

        await client.UnsubscribeAsync(topic);
        await client.DisconnectAsync();
        Console.WriteLine("Disconnesso. Programma terminato.");
    }

    // =====================================================================
    // FUNZIONI DATABASE (Utilizzando Microsoft.Data.SqlClient)
    // =====================================================================

    static async Task SalvaNelDatabase(DatoSensore dato)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(stringaConnessione))
            {
                await conn.OpenAsync();

                string query = "INSERT INTO Dati (Data_e_Ora, IdSensore, Dato, IdMacchina) VALUES (@DataOra, @IdSens, @Dato, @IdMacc)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DataOra", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IdSens", dato.IdSensore);
                    cmd.Parameters.AddWithValue("@Dato", dato.Dato ?? "");
                    cmd.Parameters.AddWithValue("@IdMacc", dato.IdMacchina);

                    await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine(" -> [DB] Salvato nel database con successo!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(" -> [ERRORE SCRITTURA DB]: " + ex.Message);
        }
    }

    static async Task LeggiDalDatabase()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(stringaConnessione))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM Dati";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        Console.WriteLine("\n--- DATI PRESENTI NEL DATABASE ---");
                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            DateTime data = reader.GetDateTime(1);
                            int idSensore = reader.GetInt32(2);
                            string dato = reader.GetString(3);
                            int idMacchina = reader.GetInt32(4);

                            Console.WriteLine($"Id: {id} | Data: {data} | Macchina: {idMacchina} | Sensore: {idSensore} | Dato: {dato}");
                        }
                        Console.WriteLine("----------------------------------\n");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(" -> [ERRORE LETTURA DB]: " + ex.Message);
        }
    }
}