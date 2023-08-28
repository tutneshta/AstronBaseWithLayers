using System.ComponentModel.DataAnnotations;

namespace AstronBase.Domain.ViewModels.Roles;

public class RoleCreateViewModel
{
    [Required(ErrorMessage = "Поле Название обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Название", Prompt = "Название")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Описание обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Описание роли", Prompt = "Описание")]
    public string Description { get; set; }
}