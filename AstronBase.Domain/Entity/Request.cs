using System.ComponentModel;

namespace AstronBase.Domain.Entity
{
    public class Request
    {
        public int Id { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Номер заявки")]
        public int Number { get; set; }

        [DisplayName("Магазин")]
        public int? StoreId { get; set; }
        public Store Store { get; set; }

        [DisplayName("Организация")]
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        [DisplayName("Контакт")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [DisplayName("Номер ФР")]
        public int? FiscalId { get; set; }
        public Fiscal? Fiscal { get; set; }

        [DisplayName("Номер рабочего места")]
        public int? NumberPos { get; set; }

        [DisplayName("Причина обращения")]
        public string? ReasonPetition { get; set; }

        [DisplayName("Инженер")]
        public int? EngineerId { get; set; }
        public Engineer? Engineer { get; set; }

        [DisplayName("Статус заявки")]
        public int? StatusRequestId { get; set; }
        public StatusRequest? StatusRequest { get; set; }

        [DisplayName("Выполненные работы")]
        public string? Works { get; set; }

        [DisplayName("Примечание")]
        public string? Note { get; set; }

        [DisplayName("Тип")]
        public int? TypeRequestId { get; set; }
        public TypeRequest TypeRequest { get; set; }

        [DisplayName("Статус бланка")]
        public int? StatusBlankId { get; set; }
        public StatusBlank? StatusBlank { get; set; }

        [DisplayName("Номер акта")]
        public int AktNumber { get; set; }
    }
}