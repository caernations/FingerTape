using System;
using MySql.Data.MySqlClient;

namespace Tubes3_TheTorturedInformaticsDepartment;

public class DB
{
    public static void Insert(string ascii, string name)
    {
        string connectionString = "Server=localhost;Database=;User ID=root;Password=;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection to database established successfully.");

                // Example: Querying the database
                string query = "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@berkas_citra, @nama)";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                
                Console.WriteLine("Inserting fingerprint data...");

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}