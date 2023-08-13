using System.ComponentModel;

namespace AstronBase.Domain.ViewModels.RegisterState;

public class RegisterStateCreateViewModel
{
    public int Id { get; set; }

    [DisplayName("Название статуса")]
    public string Name { get; set; }
}