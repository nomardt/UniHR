using System;
using System.ComponentModel;

namespace UniHR
{
    /// <summary>
    /// [АБСТРАКЦИЯ] Базовый класс, описывающий общую концепцию Person в системе.
    /// Скрывает детали реализации, предоставляя общий интерфейс для наследников.
    /// </summary>
    public abstract class Person
    {
        /// <summary>Полное имя (ФИО) человека.</summary>
        public string FullName { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Person"/>.
        /// </summary>
        /// <param name="fullName">ФИО человека.</param>
        public Person(string fullName)
        {
            FullName = fullName;
        }

        /// <summary>
        /// [ПОЛИМОРФИЗМ] Абстрактный метод вывода информации. 
        /// Обязует все дочерние классы реализовать собственную логику отображения данных.
        /// </summary>
        public abstract void DisplayInfo();
    }

    /// <summary>
    /// [НАСЛЕДОВАНИЕ] Класс сотрудника университета. 
    /// Наследует базовые атрибуты от <see cref="Person"/> и расширяет их профессиональными полями.
    /// </summary>
    public class Worker : Person
    {
        private double _salary;

        /// <summary>
        /// [ИНКАПСУЛЯЦИЯ] Оклад сотрудника. 
        /// Прямой доступ к полю _salary закрыт. Свойство содержит встроенную логику валидации:
        /// недопустимо устанавливать значение ниже или равное 0 (сбрасывается до МРОТ).
        /// </summary>
        public double Salary
        {
            get { return _salary; }
            set { _salary = value > 0 ? value : 27093; }
        }

        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; }

        /// <summary>
        /// [ПОЛИМОРФИЗМ - Перегрузка] Конструктор по умолчанию.
        /// Создает базовую "заглушку" в виде стажера с минимальной ставкой.
        /// </summary>
        public Worker() : base("Intern (not specified)")
        {
            Position = "Стажер";
            Department = "Общий";
            Salary = 27093.00; // МРОТ
            HireDate = DateTime.Now;
        }

        /// <summary>
        /// [ПОЛИМОРФИЗМ - Перегрузка] Полный конструктор.
        /// Используется для жесткого задания всех параметров сотрудника вручную.
        /// </summary>
        public Worker(string fullName, string position, double salary, DateTime hireDate, string department) 
            : base(fullName)
        {
            Position = position;
            Salary = salary;
            HireDate = hireDate;
            Department = department;
        }

        /// <summary>
        /// [ПОЛИМОРФИЗМ - Перегрузка] "Умный" частичный конструктор.
        /// Автоматически вычисляет рыночную заработную плату на основе переданной должности.
        /// </summary>
        public Worker(string fullName, string department, string position, DateTime hireDate) 
            : base(fullName)
        {
            Position = position;
            Department = department;
            HireDate = hireDate;
            Salary = EstimateSalaryFor2026(position);
        }

        /// <summary>
        /// Деструктор (финализатор). Вызывается сборщиком мусора перед очисткой памяти.
        /// </summary>
        ~Worker()
        {
            Console.WriteLine($"Система: Данные пользователя {FullName} удалены!");
        }

        /// <summary>
        /// [ПОЛИМОРФИЗМ - Переопределение] Выводит отформатированную карточку сотрудника в консоль.
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"ФИО: {FullName, -20} | Факультет: {Department, -20} | Должность: {Position, -20} | ЗП: {Salary, -8:F2} | Выход на работу: {HireDate:dd.MM.yyyy}");
        }

        /// <summary>
        /// Внутренний метод для оценки актуальной рыночной заработной платы в Синергии на 2026 год.
        /// </summary>
        /// <param name="positionInput">Название должности для анализа.</param>
        /// <returns>Примерный оклад в рублях.</returns>
        private double EstimateSalaryFor2026(string positionInput)
        {
            string pos = positionInput.ToLower();

            if (pos.Contains("методист")) return 80000.00;
            if (pos.Contains("ассистент")) return 35000.00;
            if (pos.Contains("старший преподаватель")) return 100000.00;
            if (pos.Contains("доцент")) return 120000.00;
            if (pos.Contains("профессор")) return 150000.00;
            if (pos.Contains("декан") || pos.Contains("зав")) return 200000.00;
            if (pos.Contains("стажер") || pos.Contains("практикант")) return 20000.00;

            return 94800.00; 
        }

        /// <summary>
        /// Вычисляет точный календарный стаж работы сотрудника с момента приема на работу.
        /// </summary>
        /// <returns>Кортеж, содержащий количество полных лет, месяцев и дней.</returns>
        public (int Years, int Months, int Days) GetExperience()
        {
            DateTime now = DateTime.Now;
            TimeSpan workspan = now - HireDate;
            DateTime zeroTime = new DateTime(1, 1, 1);
            return ((zeroTime + workspan).Year - 1, (zeroTime + workspan).Month - 1, (zeroTime + workspan).Day - 1);
        }

        /// <summary>
        /// Обновляет кадровые данные сотрудника (перевод на новую должность с изменением оклада).
        /// </summary>
        /// <param name="newPosition">Новая должность.</param>
        /// <param name="newSalary">Новый оклад.</param>
        public void UpdatePositionAndSalary(string newPosition, double newSalary)
        {
            Position = newPosition;
            Salary = newSalary;
            Console.WriteLine($"Система: {FullName} переведен на должность \"{Position}\" с окладом {Salary} руб");
        }

        // --- БЛОК СИМУЛЯЦИИ КОРПОРАТИВНЫХ БИЗНЕС-ПРОЦЕССОВ ---

        /// <summary>
        /// Симулирует запрос бюджета на инновации, демонстрируя сложность финансового комплаенса 
        /// и задержки в ERP-системе при согласовании инвестиций.
        /// </summary>
        /// <param name="projectDetails">Описание инновационного проекта или статьи расходов.</param>
        public void RequestInnovationBudget(string projectDetails)
        {
            Console.WriteLine($"[ФИНАНСОВЫЙ КОМПЛАЕНС] {FullName} запрашивает бюджет на: '{projectDetails}'.");
            Console.WriteLine("Процесс: Риск-менеджмент -> Финансовый комитет -> Одобрение бюджета в ERP.");
            Console.WriteLine("Статус: Заявка отложена до следующего цикла квартального планирования.");
        }

        /// <summary>
        /// Демонстрирует проблему "Data Silos" (изолированности данных) и отсутствия единого API 
        /// при попытке межкафедрального обмена информацией.
        /// </summary>
        /// <param name="colleague">Сотрудник смежного подразделения, к данным которого запрашивается доступ.</param>
        public void RequestCrossFacultyData(Worker colleague)
        {
            Console.WriteLine($"\n[DATA SILOS] Запрос данных: {FullName} ({Department}) <-> {colleague.FullName} ({colleague.Department})");
            if (this.Department == colleague.Department)
            {
                Console.WriteLine("Успех: Прямой доступ к локальной базе данных кафедры.");
            }
            else
            {
                Console.WriteLine("Ошибка интеграции: Отсутствует внутренний API. Доступ к базам других факультетов заблокирован политиками безопасности.");
            }
        }
    }
}