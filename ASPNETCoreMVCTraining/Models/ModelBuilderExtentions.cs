using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ASPNETCoreMVCTraining.Models;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dept>().HasData(
            new Dept()
            {
                Id = 1,
                Name = "IT"
            },
            new Dept()
            {
                Id = 2,
                Name = "HR"
            },
            new Dept()
            {
                Id = 3,
                Name = "Payroll"
            }
        );

        using (var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Data/mock_data.csv")))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<EmployeeDataModel>();

            modelBuilder.Entity<Employee>().HasData(records.Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name,
                DateOfBirth = e.DateOfBirth,
                DeptId = e.DeptId,
                Email = e.Email
            }));
        }
    }
}

public class EmployeeDataModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public int DeptId { get; set; }

    public DateTime? DateOfBirth { get; set; }
}