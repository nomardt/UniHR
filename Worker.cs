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
            set {_salary = value > 0 ? value : 20000;}
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

        ~Worker()
        {
            Console.WriteLine($"[Система] Данные пользователя {FullName} удалены!");
        }

        // Методы
        public override void DisplayInfo()
        {
            Console.WriteLine($"ФИО: {FullName, -20} | Факультет: {Department, -20} | Должность: {Position, -20} | ЗП: {Salary, -8:F2} | Выход на работу: {HireDate:dd.MM.yyyy}");
        }

        public (int Years, int Months, int Days) GetExperience()
        {
            DateTime now = DateTime.Now;
            TimeSpan workspan = now - HireDate;
            DateTime zeroTime = new DateTime(1, 1, 1);
            return ((zeroTime + workspan).Year - 1, (zeroTime + workspan).Month - 1, (zeroTime + workspan).Day - 1);
        }

        // Бюрократия
        public void RequestSoftwareBuy(string software)
        {
            Console.WriteLine($"\n[БЮРОКРАТИЯ] {FullName} запрашивает '{software}'.");
            Console.WriteLine(" -> Процесс: Сбор подписей зав.кафедрой/декан -> IT-отдел -> Закупки -> Бухгалтерия -> Ректорат/Фин директор.");
            Console.WriteLine(" -> Ожидаемое время согласования: 10-15 рабочих дней.");
        }

        // Коммуникация
        public void CollaborateWith(Worker colleague)
        {
            Console.WriteLine($"\n[КОММУНИКАЦИЯ] Попытка связи: {FullName} ({Department}) <-> {colleague.FullName} ({colleague.Department})");
            if (this.Department == colleague.Department)
            {
                Console.WriteLine(" -> Успех: Быстрая связь внутри одного факультета.");
            }
            else
            {
                Console.WriteLine(" -> Ошибка гибкости: Слабая горизонтальная связь. Требуется официальная служебная записка через руководителей.");
            }
        }
    }
}