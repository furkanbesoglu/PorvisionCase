using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.API.Models.DBModels
{
    public partial class Kurlartbl
    {
        public int KurId { get; set; }
        public int? CrossOrder { get; set; }
        public string Kod { get; set; }
        public string CurrencyCode { get; set; }
        public int? Unit { get; set; }
        public string Name { get; set; }
        public string CurrencyName { get; set; }
        public decimal? ForexBuying { get; set; }
        public decimal? ForexSelling { get; set; }
        public decimal? BanknoteBuying { get; set; }
        public decimal? BanknoteSelling { get; set; }
        public decimal? CrossRateUsd { get; set; }
    }
}
