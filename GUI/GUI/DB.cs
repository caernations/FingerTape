using System;
using MySql.Data.MySqlClient;

namespace Tubes3_TheTorturedInformaticsDepartment
{
    public class DB
    {
        public static void Insert(string ascii, string name)
        {
            string connectionString = "Server=localhost;Database=tubes3stima;User ID=caernations;Password=Wayaewayae19622300;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to database established successfully.");

                    // Query to insert fingerprint data
                    string query = "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@berkas_citra, @nama)";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Adding parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@berkas_citra", ascii);
                    command.Parameters.AddWithValue("@nama", name);

                    Console.WriteLine("Inserting fingerprint data...");

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public static List<string> SelectAllPath()
        {
            string connectionString = "Server=localhost;Database=tubes3stima;User ID=root;Password=456088;";
            List<string> paths = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to database established successfully.");

                    // Query to select all fingerprint data
                    string query = "SELECT berkas_citra FROM sidik_jari";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    Console.WriteLine("Retrieving fingerprint data...");

                    // Execute the command
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var berkas_citra = reader["berkas_citra"].ToString();
                        if (!string.IsNullOrEmpty(berkas_citra))
                        {
                            paths.Add(berkas_citra);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return paths;
        }
    }
}
