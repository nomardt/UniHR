using System;
using Xunit;
using UniHR; // This gives access to your Worker class

namespace UniHR.Tests
{
    public class WorkerTests
    {
        // 1. Testing ENCAPSULATION (Task 4 Requirement)
        // Verifies that a negative salary is blocked and set to the 27093 default
        [Fact]
        public void SalaryEncapsulation_NegativeValue_SetsDefaultMinimum()
        {
            // Arrange
            var worker = new Worker();
            
            // Act
            worker.Salary = -5000;
            
            // Assert
            Assert.Equal(27093.00, worker.Salary);
        }

        // 2. Testing METHODS / LOGIC (Task 4 Requirement)
        // Verifies the automatic salary estimation for a "методист"
        [Fact]
        public void PartialConstructor_EstimatesSalaryCorrectly()
        {
            // Arrange & Act
            var worker = new Worker("Иванов И.И.", "Кафедра ЦЭ", "Старший методист", DateTime.Now);
            
            // Assert
            // The EstimateSalaryFor2026 method should catch "методист" and assign 80000
            Assert.Equal(80000.00, worker.Salary);
        }

        // 3. Testing BEHAVIOR / STATE (Task 4 Requirement)
        // Verifies that the exact experience logic works accurately
        [Fact]
        public void GetExperience_CalculatesExactYearsCorrectly()
        {
            // Arrange: Set hire date to exactly 5 years ago
            DateTime fiveYearsAgo = DateTime.Now.AddYears(-5);
            var worker = new Worker("Петров П.П.", "Доцент", 120000, fiveYearsAgo, "ИТ");

            // Act
            var experience = worker.GetExperience();

            // Assert
            Assert.Equal(5, experience.Years);
        }
    }
}