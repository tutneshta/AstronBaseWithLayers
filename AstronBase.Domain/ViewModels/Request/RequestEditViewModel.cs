using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Request
{
    public class RequestEditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Номер заявки")]
        public int Number { get; set; }

        [DisplayName("Организация")]
        public int? CompanyId { get; set; }

        [DisplayName("Магазин")]
        public int StoreId { get; set; }

        [DisplayName("Контакт")]
        public int ClientId { get; set; }

        [DisplayName("Номер ФР")]
        public int? FiscalId { get; set; }

        [DisplayName("Номер рабочего места")]
        public int? NumberPos { get; set; }

        [DisplayName("Причина обращения")]
        public string? ReasonPetition { get; set; }

        [DisplayName("Инженер")]
        public int? EngineerId { get; set; }

        [DisplayName("Статус заявки")]
        public int? StatusRequestId { get; set; }

        [DisplayName("Выполненные работы")]
        public string? Works { get; set; }

        [DisplayName("Примечание")]
        public string? Note { get; set; }

        [DisplayName("Тип")]
        public int TypeRequestId { get; set; }

        [DisplayName("Статус бланка")]
        public int? StatusBlankId { get; set; }

        [DisplayName("Номер акта")]
        public int AktNumber { get; set; }
    }
}
