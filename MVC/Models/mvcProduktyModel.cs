using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcProduktyModel
    {
        public int IdProduktu { get; set; }
        [Required(ErrorMessage = "Musisz podać nazwę produktu")]
        public string NazwaProduktu { get; set; }
        public string Opis { get; set; }
        public Nullable<int> VAT { get; set; }
        public Nullable<int> Ilosc { get; set; }
        public Nullable<decimal> CenaNetto { get; set; }
        public Nullable<decimal> CenaBrutto { get { return CenaBrutto = CenaNetto * (1 + (Convert.ToDecimal(VAT) / 100)); } set {; } }
    }
}