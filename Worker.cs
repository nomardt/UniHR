using System;
using System.ComponentModel;

namespace UniHR
{
    // Abstraction
    public abstract class Person
    {
        public string FullName {get; set;}

        public Person(string fullName)
        {
            FullName = fullName;
        }

        // Polymorphism - этот метод должен быть имплементирован в в дочерних классах
        public abstract void DisplayInfo();
    }

    // Inheritance
    public class Worker : Person
    {
        // Incapsulation
        private double _salary;
        public double Salary
        {
            get {return _salary;}
            set {_salary = value > 0 ? value : 27093;}
        }

        public string Position {get; set;}
        public string Department {get; set;}
        public DateTime HireDate {get; set;}

        // The defualt constructor
        public Worker() : base("Intern (not specified)")
        {
            Position = "Стажер";
            Department = "Общий";
            Salary = 27093.00; // МРОТ
            HireDate = DateTime.Now;
        }

        // Полный constructor
        public Worker(string fullName, string position, double salary, DateTime hireDate, string department) : base(fullName)
        {
            Position = position;
            Salary = salary;
            HireDate = hireDate;
            Department = department;
        }

        // Частичный конструктор с примерной оценкой ЗП
        public Worker(string fullName, string department, string position, DateTime hireDate) : base(fullName)
        {
            Position = position;
            Department = department;
            HireDate = hireDate;

            Salary = EstimateSalaryFor2026(position);
        }

        ~Worker()
        {
            Console.WriteLine($"Система: Данные пользователя {FullName} удалены!");
        }

        // Методы
        public override void DisplayInfo()
        {
            Console.WriteLine($"ФИО: {FullName, -20} | Факультет: {Department, -20} | Должность: {Position, -20} | ЗП: {Salary, -8:F2} | Выход на работу: {HireDate:dd.MM.yyyy}");
        }

        private double EstimateSalaryFor2026(string positionInput)
        {
            // Переводим ввод в нижний регистр для точного поиска
            string pos = positionInput.ToLower();

            if (pos.Contains("методист")) return 80000.00;
            if (pos.Contains("ассистент")) return 35000.00;
            if (pos.Contains("старший преподаватель")) return 100000.00;
            if (pos.Contains("доцент")) return 120000.00;
            if (pos.Contains("профессор")) return 150000.00;
            if (pos.Contains("декан") || pos.Contains("зав")) return 200000.00;
            if (pos.Contains("стажер") || pos.Contains("практикант")) return 20000.00;

            // Если должность неизвестна, ставим среднюю ЗП выпускников Синергии по Роструду
            return 94800.00; 
        }

        public (int Years, int Months, int Days) GetExperience()
        {
            DateTime now = DateTime.Now;
            TimeSpan workspan = now - HireDate;
            DateTime zeroTime = new DateTime(1, 1, 1);
            return ((zeroTime + workspan).Year - 1, (zeroTime + workspan).Month - 1, (zeroTime + workspan).Day - 1);
        }

        public void UpdatePositionAndSalary(string newPosition, double newSalary)
        {
            Position = newPosition;
            Salary = newSalary;
            Console.WriteLine($"Система: {FullName} переведен на должность \"{Position}\" с окладом {Salary} руб");
        }

        // Бюрократия
        public void RequestSoftwareBuy(string software)
        {
            Console.WriteLine($"[БЮРОКРАТИЯ] {FullName} запрашивает '{software}'.");
            Console.WriteLine("Процесс: Сбор подписей зав.кафедрой/декан -> IT-отдел -> Закупки -> Бухгалтерия -> Ректорат/Фин директор.");
            Console.WriteLine("Ожидаемое время согласования: 15-25 рабочих дней.");
        }

        // Коммуникация
        public void CollaborateWith(Worker colleague)
        {
            Console.WriteLine($"\n[КОММУНИКАЦИЯ] Попытка связи: {FullName} ({Department}) <-> {colleague.FullName} ({colleague.Department})");
            if (this.Department == colleague.Department)
            {
                Console.WriteLine("Успех: Быстрая связь внутри одного факультета.");
            }
            else
            {
                Console.WriteLine("Ошибка гибкости: Слабая горизонтальная связь. Требуется официальная служебная записка через руководителей.");
            }
        }
    }
}