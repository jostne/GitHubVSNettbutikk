using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Model
{
    public class Kunde
    {
        public int kundeid { get; set; }
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(pattern: "[a-åA-Å]{1,30}")]
        public string fornavn { get; set; }
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(pattern: "[a-åA-Å]{1,30}")]
        public string etternavn { get; set; }
        [Required(ErrorMessage = "Epost må oppgis")]
        public string epost { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        [RegularExpression(pattern: "[0-9a-åA-Å]{4,30}")]
        public string passord { get; set; }
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(pattern: "[0-9]{4}")]
        public string postnr { get; set; }
        [Required(ErrorMessage = "Adresse må oppgis")]
        public string adresse { get; set; }
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string poststed { get; set; }
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(pattern: "[0-9]{8}")]
        public int telefonnr { get; set; }
    }
    public class sikkerAdmin
    {
        public int adminid { get; set; }
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public string brukernavn { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        [RegularExpression(pattern: "[0-9a-åA-Å]{4,30}")]
        public string passord { get; set; }
    }
    public class Produkten
    {
        public int produktid { get; set; }
        public string navn { get; set; }
        public decimal pris { get; set; }
        public string path { get; set; }
        public string kategori { get; set; }
        public string beskrivelse { get; set; }
    }
    public class vognen
    {
        public int viktigid { get; set; }
        public string vognid { get; set; }
        public int produktid { get; set; }
        public int antall { get; set; }
        public System.DateTime datolaget { get; set; }
        public virtual Produkten produkt { get; set; }
    }
    public class Bestiltprodukten
    {
        public int bestiltproduktid { get; set; }
        public int bestillingid { get; set; }
        public int produktid { get; set; }
        public int antall { get; set; }
        public decimal pris { get; set; }
        public virtual Produkten produkt { get; set; }
        public virtual Bestillingen bestilling { get; set; }
    }
    public class Bestillingen
    {
        public int bestillingsid { get; set; }
        public virtual Kunde kundeid { get; set; }
        public DateTime dato { get; set; }
        public decimal total { get; set; }
        public virtual Produkten produkt { get; set; }
        //public List<Produkt> produktdetaljer { get; set; }
    }
    public class Poststedet
    {
        public string postnr { get; set; }
        public string poststed { get; set; }
    }
}
