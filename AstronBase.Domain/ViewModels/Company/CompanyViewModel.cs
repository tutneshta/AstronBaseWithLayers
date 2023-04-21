namespace AstronBase.Domain.ViewModels.Company
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? DirectorName { get; set; }
        public string? PositionName { get; set; }
        public string? StatuteName { get; set; }
        public int? Unn { get; set; }
        public string? Address { get; set; }
        public int? CheckingAccount { get; set; }
        public string? Bank { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}