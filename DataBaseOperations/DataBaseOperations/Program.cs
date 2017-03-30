using System;
using System.Data.SqlClient;

namespace DataBaseOperations
{
    class Program
    {

        private static string cs = @"Data Source=songqiang.database.windows.net;Initial Catalog=AW;Persist Security Info=True;User ID=songqiang;Password=";

        public static void ReadWithInject()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string injection = @"Orlando' OR ''='";
                string cmd = $@"SELECT FirstName,LastName FROM SalesLT.Customer WHERE FirstName='{injection}'";
                using (SqlCommand command = new SqlCommand(cmd, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
                        }
                    }
                }
            }
        }


        public static void ReadWithOutInject()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                //string injection = @"Orlando' OR ''='";
                string injection = @"Orlando";
                string cmd = @"SELECT FirstName,LastName FROM SalesLT.Customer WHERE FirstName=@FirstName";
                using (SqlCommand command = new SqlCommand(cmd, conn))
                {
                    command.Parameters.AddWithValue("FirstName", injection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
                        }
                    }
                }

            }
        }


        static void Main(string[] args)
        {

            ReadWithInject();
            Console.WriteLine();
            ReadWithOutInject();
        }
    }
}