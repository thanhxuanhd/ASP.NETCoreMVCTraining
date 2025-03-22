namespace ASPNETCoreMVCTraining.Domain.Models;

public class Dept
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}