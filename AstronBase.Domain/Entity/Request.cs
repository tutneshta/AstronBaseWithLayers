namespace AstronBase.Domain.Entity
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public int? StoreId { get; set; }
        public Store? Store { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        public int? FiscalId { get; set; }
        public Fiscal? Fiscal { get; set; }
        public int? NumberPos { get; set; }
        public string? ReasonPetition { get; set; }
        public int? EngineerId { get; set; }
        public Engineer? Engineer { get; set; }
        public int? StatusRequestId { get; set; }
        public StatusRequest? StatusRequest { get; set; }
        public string? Works { get; set; }
        public string? Note { get; set; }
        public int TypeRequestId { get; set; }
        public TypeRequest TypeRequest { get; set; }
        public int? StatusBlankId { get; set; }
        public StatusBlank? StatusBlank { get; set; }
        public int AktNumber { get; set; }
    }
}