using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {

        private static void SaveFileToDatabase()
        {
            string connectionString = @"server=ngknn.ru;Trusted_Connection=No;DataBase=Registr;User=33П;PWD=12357";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO Pictures VALUES (@FileName, @Title, @ImageData)";
                command.Parameters.Add("@FileName", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);

                // путь к файлу для загрузки
                string filename = @"D:\Picture\Walpper.jpg";
                // заголовок файла
                string title = "Picturiis";
                // получаем короткое имя файла для сохранения в бд
                string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1); // cats.jpg
                                                                                           // массив для хранения бинарных данных файла
                byte[] imageData;
                using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                // передаем данные в команду через параметры
                command.Parameters["@FileName"].Value = shortFileName;
                command.Parameters["@Title"].Value = title;
                command.Parameters["@ImageData"].Value = imageData;

                command.ExecuteNonQuery();
            }
        }

        static void Main(string[] args)
        {
            SaveFileToDatabase();
        }
    }
}
