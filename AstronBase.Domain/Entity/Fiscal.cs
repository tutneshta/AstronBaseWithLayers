namespace AstronBase.Domain.Entity
{
    public class Fiscal
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int RegisterStateId { get; set; }
        public RegisterState RegisterState { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int? StatusFiscalId { get; set; }
        public StatusFiscal StatusFiscal { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        public int? EngineerId { get; set; }
        public Engineer Engineer { get; set; }
        public string Note { get; set; }
        public List<Request> Requests { get; set; } = new();
    }
}