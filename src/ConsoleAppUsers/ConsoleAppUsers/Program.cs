using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            while (true)
            {
                Console.WriteLine("Работа с таблицей Users.\nВыберите действие:\n1) Посмотреть все записи\n2) Добавить нового пользователя\n3) Обновить существующего пользователя\n4) Удалить существующего пользователя\n5) Авторизоваться в системе");
                string userChoiceNum = Console.ReadLine();
                Console.Clear();
                switch (userChoiceNum)
                {
                    case "1":
                        Console.WriteLine("Добавление пользователя:\nВведите следующие данные через запятую \",\"\nFirstname,Username,Password,Age");
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Ваш выбор не соответствует ни одному варианту. Попробуйте ещё раз");
                        break;
                }
                Console.Clear();
            }
        }
    }
}
