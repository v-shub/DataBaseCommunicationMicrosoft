using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUsers
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connString = "Server=.;Database=FamilyLife;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string wrongInputMessage = "Неверный формат, повторите еще раз";
            string sqlQuery;
            SqlCommand cmd;
            int id;
            string userName;
            string password;
            DateTime birthDate;
            int childrenCount;
            string extraInfo;

            while (true)
            {
                Console.WriteLine("Работа с таблицей Users.\nВыберите действие:\n1) Посмотреть все записи\n2) Добавить нового пользователя\n3) Обновить существующего пользователя\n4) Удалить существующего пользователя\n5) Авторизоваться в системе");
                string userChoiceNum = Console.ReadLine();
                Console.Clear();
                switch (userChoiceNum)
                {
                    case "1":
                        sqlQuery = "select * from Users where Deleted_at is null";
                        cmd = new SqlCommand(sqlQuery, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                            userName = reader.GetString(1);
                            birthDate = reader.GetDateTime(3);
                            childrenCount= reader.GetInt32(4);
                            Console.WriteLine($"{ id }\t{ userName }\t{ birthDate }\t{ childrenCount}");
                        }
                        reader.Close();
                        break;
                    case "2":
                        Console.WriteLine("Добавление пользователя:\nВведите следующие данные через запятую \",\"\nName,Password,Birthday_date,Children_count,Extra_info");
                        while (true)
                        {
                            string[] stringAddInput = Console.ReadLine().Split(',');
                            if (stringAddInput.Length != 5)
                            {
                                Console.WriteLine(wrongInputMessage);
                                continue;
                            }
                            userName = stringAddInput[0];
                            password = stringAddInput[1];
                            extraInfo = stringAddInput[4];
                            bool isChCountCorrect = Int32.TryParse(stringAddInput[3], out childrenCount);
                            bool isBirthDateCorrect= DateTime.TryParse(stringAddInput[2], out birthDate);
                            if (isBirthDateCorrect & isChCountCorrect & userName.Length < 51 & password.Length < 31 & extraInfo.Length < 4001)
                                break;
                            else
                                Console.WriteLine(wrongInputMessage);
                        }

                        break;
                    case "3":
                        break;
                    case "4":
                        Console.WriteLine("Удаление пользователя:\nВведите ID пользователя, которого хотите удалить");
                        bool isIDCorrect = Int32.TryParse(Console.ReadLine(), out id);
                        sqlQuery = $"UPDATE Users SET Deleted_at = getdate() WHERE ID = {id}";
                        cmd = new SqlCommand(sqlQuery, conn);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " user(s) deleted");
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Ваш выбор не соответствует ни одному варианту. Попробуйте ещё раз");
                        break;
                }
                Console.WriteLine("Нажмите enter чтобы вернуться");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
