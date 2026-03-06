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

            // universityStaff.Add(new Worker());

            int count = 0;
            while (true)
            {
                Console.Write("Укажите число сотрудников для добавления в систему: ");
                if (int.TryParse(Console.ReadLine(), out count) && count >= 0) break;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"--- Сотрудник {i + 1} ---");

                string name;
                while (true)
                {
                    Console.Write("ФИО: ");
                    name = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(name)) break;
                    Console.WriteLine("Ошибка: Поле ФИО не может быть пустым.");
                }

                string department;
                while (true)
                {
                    Console.Write("Факультет или Отдел (например, \"Кафедра Цифровой Экономики\"): ");
                    department = (Console.ReadLine() ?? "").Trim();
                    if (!string.IsNullOrWhiteSpace(department)) break;
                    Console.WriteLine("Ошибка: Укажите факультет или отдел.");
                }

                string position;
                while (true)
                {
                    Console.Write("Должность: ");
                    position = (Console.ReadLine() ?? "").Trim();
                    if (!string.IsNullOrWhiteSpace(department)) break;
                    Console.WriteLine("Ошибка: Укажите факультет или отдел.");
                }

                double salary = 0;
                while (true)
                {
                    Console.Write("Зарплата: ");
                    if (double.TryParse((Console.ReadLine() ?? "").Replace('.', ','), out salary)) break;
                }

                DateTime hireDate = DateTime.Now;
                while (true)
                {
                    Console.Write("Дата приема (формат - ДД.ММ.ГГГГ) [Нажмите Enter для текущей даты]: ");
                    string input = (Console.ReadLine() ?? "").Trim();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine($"По умолчанию установлена текущая дата: {hireDate:dd.MM.yyyy}");
                        break;
                    }

                    // Нужно использовать ММ вместо мм, чтобы указать месяцы вместо минут
                    if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out hireDate)) break;

                    Console.WriteLine("Ошибка: Убедитесь, что вводите дату строго в формате ДД.ММ.ГГГГ (например, 01.01.2000).");
                }

                universityStaff.Add(new Worker(name, position, salary, hireDate, department));
            }

            // Вывести всех сотрудников
            Console.WriteLine("\n== ВСЕ СОТРУДНИКИ ==");
            foreach (var worker in universityStaff)
            {
                worker.DisplayInfo();
            }

            // Под кейс-задачу 5
            Console.WriteLine("\n== ДЕМО ПРОБЛЕМ ОРГ ПРОЦЕССОВ ==");
            if (universityStaff.Count >= 2) {
                Worker first = universityStaff[0];
                Worker last = universityStaff[universityStaff.Count - 1];

                // Демо бюрократия
                first.RequestSoftwareBuy("Приобрести лицензию Битрикс24");
                // Демо разрозненность
                first.CollaborateWith(last);
            } else
            {
                Console.WriteLine("Ошибка: Добавьте мин 1 сотрудника (помимо стажера) для демо");
            }

            Console.WriteLine("\n\nПрограмма завершила работу, введите любой символ для выхода...");
            Console.ReadLine(); // Чтобы программа не завершилась сразу
        }
    }
}