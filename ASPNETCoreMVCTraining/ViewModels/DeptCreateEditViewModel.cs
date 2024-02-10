using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreMVCTraining.ViewModels;

public class DeptCreateEditViewModel
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }
}