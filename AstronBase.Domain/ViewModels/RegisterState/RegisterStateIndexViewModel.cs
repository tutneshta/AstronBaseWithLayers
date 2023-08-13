using AstronBase.Domain.ViewModels.Pagination;

namespace AstronBase.Domain.ViewModels.RegisterState;

public class RegisterStateIndexViewModel
{
    public IEnumerable<Entity.RegisterState> RegisterStates { get; }
    public PageViewModel PageViewModel { get; }

    public RegisterStateIndexViewModel(List<Entity.RegisterState> registerStates, PageViewModel viewModel)
    {
        RegisterStates = registerStates;
        PageViewModel = viewModel;

    }
}