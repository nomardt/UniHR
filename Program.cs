using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.NetworkInformation;
using UniHR;

namespace SynergyHR
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Worker> universityStaff = new List<Worker>();

            Console.WriteLine("== Добро пожаловать в HR-систему университета Синергия! ==");

            universityStaff.Add(new Worker());

            int count = 0;
            while (true)
            {
                Console.Write("Сколько сотрудников добавить в систему?");
                if (int.TryParse(Console.ReadLine(), out count) && count >= 0) break;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"--- Сотрудник {i + 1} ---");

                Console.Write("ФИО: ");
                string name = Console.ReadLine() ?? "";

                Console.Write("Факультет или Отдел (например, Кафедра Цифровой Экономики): ");
                string department = Console.ReadLine() ?? "";

                Console.Write("Должность: ");
                string position = Console.ReadLine() ?? "";

                double salary = 0;
                while (true)
                {
                    Console.Write("Зарплата: ");
                    if (double.TryParse((Console.ReadLine() ?? "").Replace('.', ','), out salary)) break;
                }

                DateTime hireDate = DateTime.Now;
                while (true)
                {
                    Console.Write("Дата приема (ДД.ММ.ГГГГ): ");
                    if (DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out hireDate)) break;
                }

                universityStaff.Add(new Worker(name, position, salary, hireDate, department));
            }

            // Вывести всех сотрудников
            Console.WriteLine("\n== ВСЕ СОТРУДНИКИ ==");
            foreach (var worker in universityStaff)
            {
                worker.DisplayInfo();
            }
        }
    }
}