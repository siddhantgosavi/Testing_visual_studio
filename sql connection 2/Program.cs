using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql_connection_2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = "Server=HJD024652\\SQLEXPRESS;" + "Database=Test;" + "User id=sa;" + "Password=P@ssw0rd0000;";
                    conn.Open();
                    Console.WriteLine("Connection Successfull");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                while (true)
                {
                    try
                    {
                        Console.WriteLine("1 to select all \n2 to insert");
                        int Option = int.Parse(Console.ReadLine().ToString());

                        switch (Option)
                        {
                            case 1:
                                {
                                    SelectAll(conn);
                                    break;
                                }
                            case 2:
                                {
                                    InsertInto(conn);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }            
        }

        private static void SelectAll(SqlConnection conn)
        {
            SqlCommand SelectCommand = new SqlCommand("SELECT * from TEST_table", conn);

            Console.WriteLine("Creating reader and getting data");

            using (SqlDataReader reader = SelectCommand.ExecuteReader())
            {
                Console.WriteLine("FirstColumn\tSecond column");
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0} \t {1}", reader[0], reader[1]));
                }
            }
        }

        private static void InsertInto(SqlConnection conn)
        {
            Console.Write("Enter value to enter into table : ");
            string ValueToInsert = Console.ReadLine().ToString();

            SqlCommand InsertCommand = new SqlCommand("insert into test_table values(@0)", conn);
            InsertCommand.Parameters.Add(new SqlParameter("0", ValueToInsert));
            Console.WriteLine("Commands executed! Total rows affected are " + InsertCommand.ExecuteNonQuery());
            Console.ReadLine();
            //Console.Clear();
        }
    }
}