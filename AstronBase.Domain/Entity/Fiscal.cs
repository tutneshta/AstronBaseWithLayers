using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AstronBase.Domain.Entity
{
    public class Fiscal
    {
        public int Id { get; set; }
        [DisplayName("Серийный номер")] 
        public string SerialNumber { get; set; }
        public int RegisterStateId { get; set; }
        public RegisterState RegisterState { get; set; }
        [DisplayName("Модель")]
        public int? ModelId { get; set; }
        
        public Model Model { get; set; }
        [DisplayName("Статус")]
        public int? StatusFiscalId { get; set; }
        public StatusFiscal StatusFiscal { get; set; }
        [DisplayName("Организация")]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        [DisplayName("Объект")]
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        [DisplayName("Инженер")]
        public int? EngineerId { get; set; }
        public Engineer Engineer { get; set; }
        [DisplayName("Заметки")]
        public string Note { get; set; }
        public List<Request> Requests { get; set; } = new();
    }
}