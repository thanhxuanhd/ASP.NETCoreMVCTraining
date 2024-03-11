using CsvHelper;
using System.Globalization;

namespace ASPNETCoreMVCTraining.Models;

public static class ApplicationDbExtensions
{
    public static IApplicationBuilder SeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
            if (!dbContext.Depts.Any())
            {
                dbContext.Depts.AddRange(new List<Dept>
            {
                new Dept()
                {
                    Name = "IT"
                },
                new Dept()
                {
                    Name = "HR"
                },
                new Dept()
                {
                    Name = "Payroll"
                }
            });

                dbContext.SaveChanges();
            }

            if (!dbContext.Employees.Any())
            {
                using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Data/mock_data.csv"));
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<EmployeeDataModel>();

                dbContext.AddRange(records.Select(e => new Employee
                {
                    Name = e.Name,
                    DateOfBirth = e.DateOfBirth,
                    DeptId = e.DeptId,
                    Email = e.Email
                }));

                dbContext.SaveChanges();
            }
        }

        return app;
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