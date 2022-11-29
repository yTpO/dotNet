using System.Data.SqlClient;

namespace Words
{
    internal class DataBase
    {
        static string connectionString = "Server=DESKTOP-LOSC5IU;Database=wordsDB;Trusted_Connection=True;";

        public static async Task<string> GetWordsByLvl(string lvl)
        {
            string sqlExpression = $"SELECT * FROM Words WHERE Lvl_Id = {lvl} AND IsActive = 1";

            Console.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    string columnName1 = reader.GetName(0);
                    string columnName2 = reader.GetName(1);

                    Console.WriteLine($"{columnName1}\t{columnName2}");

                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object value = reader.GetValue(1);

                        Console.WriteLine($"{id} \t{value}");
                    }
                }
            }

            Console.Write("\nВыберите слово для игры:  ");
            return Console.ReadLine();
        }

        public static async Task<string> GetWordById(string wordId, string LvlId)
        {
            string sqlExpression = $"SELECT Value FROM Words WHERE Id = {wordId} AND Lvl_Id = {LvlId}";
            string wordResult = "";

            Console.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        object value = reader.GetValue(0);

                        wordResult = Convert.ToString(value);
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка выбора слова");
                    Console.ReadLine();
                }
            }

            return wordResult;
        }

        public static async Task<string> GetAllLevels()
        {
            string sqlExpression = "SELECT * FROM Lvls";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    string columnName1 = reader.GetName(1);
                    string columnName2 = reader.GetName(2);
                    string columnName3 = reader.GetName(3);
                    string columnName4 = reader.GetName(4);

                    Console.WriteLine($"{columnName2}\t{columnName1}\t{columnName3}\t{columnName4}");

                    while (reader.Read()) // построчно считываем данные
                    {
                        object name = reader.GetValue(1);
                        object value = reader.GetValue(2);
                        object time = reader.GetValue(3);
                        object description = reader.GetValue(4);

                        Console.WriteLine($"{value})\t{name}\t{time}\t{description}");
                    }
                }
            }

            Console.Write("\nВыберите уровень сложности:  ");
            //string s = Console.ReadLine();
            return Console.ReadLine();
        }


        public static async Task<int> GetTimeByLvl(string lvl)
        {
            string sqlExpression = $"SELECT Time FROM Lvls WHERE Value = {lvl}";
            int time = 10;

            Console.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        object value = reader.GetValue(0);

                        time = Convert.ToInt32(value);
                    }
                }
            }

            return time;
        }
    }
}
