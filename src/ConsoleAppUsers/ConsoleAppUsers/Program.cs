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

            string connString = @"Server=.;AttachDBFileName=C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FamilyLife.mdf;Database=FamilyLife;Trusted_Connection=True;";
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
                        Console.WriteLine("Добавление пользователя:\nВведите следующие данные через запятую \",\"\nUsername,Password,Birthday_date,Children_count,Extra_info");
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
                            {
                                sqlQuery = $@"insert into Users (Username,Password,Birthday_date,Children_count,Extra_info) values ('{userName}', '{password}', ('{birthDate}'), {childrenCount}, '{extraInfo}')";
                                cmd = new SqlCommand(sqlQuery, conn);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                Console.WriteLine(rowsAffected + " user(s) added");
                                break;
                            }
                            else
                                Console.WriteLine(wrongInputMessage);
                        }

                        break;
                    case "3":
                        Console.WriteLine("Изменение данных пользователя:\nВведите ID пользователя, которого хотите изменить");
                        while (true)
                        {
                            bool isIDCorrect = Int32.TryParse(Console.ReadLine(), out id);
                            if (isIDCorrect)
                            {
                                Console.WriteLine("Введите следующие данные через запятую \",\"\nUsername,Password,Birthday_date,Children_count,Extra_info");
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
                                    bool isBirthDateCorrect = DateTime.TryParse(stringAddInput[2], out birthDate);
                                    if (isBirthDateCorrect & isChCountCorrect & userName.Length < 51 & password.Length < 31 & extraInfo.Length < 4001)
                                    {
                                        sqlQuery = $@"UPDATE Users SET Username = '{userName}', Password = '{password}', Birthday_date = ('{birthDate}'), Children_count = {childrenCount}, Extra_info = '{extraInfo}' WHERE ID = {id}";
                                        cmd = new SqlCommand(sqlQuery, conn);
                                        int rowsAffected = cmd.ExecuteNonQuery();
                                        Console.WriteLine(rowsAffected + " user(s) updated");
                                        break;
                                    }
                                    else
                                        Console.WriteLine(wrongInputMessage);
                                }
                                break;
                            }
                            else
                                Console.WriteLine(wrongInputMessage);
                        }
                        break;
                    case "4":
                        Console.WriteLine("Удаление пользователя:\nВведите ID пользователя, которого хотите удалить");
                        while (true)
                        {
                            bool isIDCorrect = Int32.TryParse(Console.ReadLine(), out id);
                            if (isIDCorrect)
                            {
                                sqlQuery = $"UPDATE Users SET Deleted_at = getdate() WHERE ID = {id}";
                                cmd = new SqlCommand(sqlQuery, conn);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                Console.WriteLine(rowsAffected + " user(s) deleted");
                                break;
                            }
                            else
                                Console.WriteLine(wrongInputMessage);
                        }
                        break;
                    case "5":
                        Console.WriteLine("Авторизация:\nВведите свой ID");
                        while (true)
                        {
                            bool isIDCorrect = Int32.TryParse(Console.ReadLine(), out id);
                            if (isIDCorrect)
                            {
                                Console.WriteLine("Введите свой пароль");
                                password = Console.ReadLine();
                                sqlQuery = $"select Password from Users where ID = {id} and Deleted_at is null";
                                cmd = new SqlCommand(sqlQuery, conn);
                                if(password.Equals(cmd.ExecuteScalar()))
                                {
                                    Console.WriteLine("Авторизация прошла успешно. Вот все ваши данные");
                                    sqlQuery = $"select * from Users where ID = {id}";
                                    cmd = new SqlCommand(sqlQuery, conn);
                                    SqlDataReader userReader = cmd.ExecuteReader();
                                    while (userReader.Read())
                                    {
                                        id = userReader.GetInt32(0);
                                        userName = userReader.GetString(1);
                                        birthDate = userReader.GetDateTime(3);
                                        childrenCount = userReader.GetInt32(4);
                                        extraInfo = userReader.GetString(5);
                                        Console.WriteLine($"{id}\t{userName}\t{password}\t{birthDate}\t{childrenCount}\t{extraInfo}");
                                    }
                                    userReader.Close();
                                }
                                else
                                    Console.WriteLine("Неверный ID или пароль. Возможно, аккаунт удалён");
                                break;
                            }
                            else
                                Console.WriteLine(wrongInputMessage);
                        }
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
