namespace Tubes3_TheTorturedInformaticsDepartment;

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class DB
{
    public static void InsertSidikJari(string path, string name)
    {
        string connectionString = "Server=localhost;Database=tubes3stima;User ID=root;Password=456088;";

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
                command.Parameters.AddWithValue("@berkas_citra", path);
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

    public static void InsertIdentitas(string NIK, string nama_alay, string tempat_lahir, string tanggal_lahir, string jenis_kelamin, string golongan_darah, string alamat, string agama, string status_perkawinan, string pekerjaan, string kewarganegaraan)
    {
        string connectionString = "Server=localhost;Database=tubes3stima;User ID=root;Password=456088;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection to database established successfully.");

                // Query to insert fingerprint data
                string query = "INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (@NIK, @nama, @tempat_lahir, @tanggal_lahir, @jenis_kelamin, @golongan_darah, @alamat, @agama, @status_perkawinan, @pekerjaan, @kewarganegaraan)";
                MySqlCommand command = new MySqlCommand(query, connection);

                // Adding parameters to prevent SQL injection
                command.Parameters.AddWithValue("@NIK", NIK);
                command.Parameters.AddWithValue("@nama", nama_alay);
                command.Parameters.AddWithValue("@tempat_lahir", tempat_lahir);
                command.Parameters.AddWithValue("@tanggal_lahir", tanggal_lahir);
                command.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin);
                command.Parameters.AddWithValue("@golongan_darah", golongan_darah);
                command.Parameters.AddWithValue("@alamat", alamat);
                command.Parameters.AddWithValue("@agama", agama);
                command.Parameters.AddWithValue("@status_perkawinan", status_perkawinan);
                command.Parameters.AddWithValue("@pekerjaan", pekerjaan);
                command.Parameters.AddWithValue("@kewarganegaraan", kewarganegaraan);

                Console.WriteLine("Inserting identitas data...");

                // Execute the command
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} row(s) inserted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.ToString()}");
            }
        }
    }

    public static string GetNamaFromPath(string path)
    {
        string connectionString = "Server=localhost;Database=tubes3stima;User ID=root;Password=456088;";
        string query = "SELECT name FROM sidik_jari WHERE path = @path"; // Replace 'your_table' and 'name' with your actual table and column names

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@path", path);

            try
            {
                connection.Open();
                string name = command.ExecuteScalar() as string; // ExecuteScalar returns the first column of the first row, cast it to string
                return name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

    public static List<List<string>> GetBiodata()
    {
        string connectionString = "Server=localhost;Database=tubes3stima;User ID=root;Password=456088;";
        string query = "SELECT * FROM biodata";
        List<List<string>> finalBiodata = new List<List<string>>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    List<string> biodata = new List<string>(); // Move this line inside the while loop

                    string NIK = reader["NIK"].ToString();
                    biodata.Add(NIK);
                    string nama = reader["nama"].ToString();
                    biodata.Add(nama);
                    string tempat_lahir = reader["tempat_lahir"].ToString();
                    biodata.Add(tempat_lahir);
                    string tanggal_lahir = reader["tanggal_lahir"].ToString();
                    biodata.Add(tanggal_lahir);
                    string jenis_kelamin = reader["jenis_kelamin"].ToString();
                    biodata.Add(jenis_kelamin);
                    string golongan_darah = reader["golongan_darah"].ToString();
                    biodata.Add(golongan_darah);
                    string alamat = reader["alamat"].ToString();
                    biodata.Add(alamat);
                    string agama = reader["agama"].ToString();
                    biodata.Add(agama);
                    string status_perkawinan = reader["status_perkawinan"].ToString();
                    biodata.Add(status_perkawinan);
                    string pekerjaan = reader["pekerjaan"].ToString();
                    biodata.Add(pekerjaan);
                    string kewarganegaraan = reader["kewarganegaraan"].ToString();
                    biodata.Add(kewarganegaraan);

                    finalBiodata.Add(biodata); // Use Add instead of AddRange
                }
                return finalBiodata;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

    public static List<string> SelectAllPaths()
    {
        string connectionString = "Server=localhost;Database=tubes3stima;User ID=root;Password=456088;";
        string query = "SELECT berkas_citra FROM sidik_jari";
        List<string> paths = new List<string>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string path = reader["berkas_citra"].ToString();
                    paths.Add(path);
                }
                return paths;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }

}

