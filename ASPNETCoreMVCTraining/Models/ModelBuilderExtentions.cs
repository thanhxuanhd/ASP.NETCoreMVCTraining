using Microsoft.EntityFrameworkCore;

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
        modelBuilder.Entity<Employee>().HasData(new Employee
        {
            Id = 1,
            Name = "Kevin",
            DeptId = 1,
            Email = "evin@testemail.com"
        }, new Employee
        {
            Id = 2,
            Name = "John",
            DeptId = 2,
            Email = "John@testemail.com"
        });
    }
}