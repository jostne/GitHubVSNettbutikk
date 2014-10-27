namespace Nettbutikkprosjekt.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sikkerkunde
    {
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
        public virtual Poststed poststed { get; set; }
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        [RegularExpression(pattern:"[0-9]{8}")]
        public int telefonnr { get; set; }
    }
    public class Kunder
    {
        [Key]
        public int kundeid { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string epost { get; set; }
        public byte[] passord { get; set; }
        public string postnr { get; set; }
        public string adresse {  get; set; }
        public virtual Poststed poststed { get; set; }
        public int telefonnr { get; set; }
    }
    public class Poststed
    {
        [Key]
        public string postnr { get; set; }
        public string poststed { get; set; }
    }


    public class Bestilling
    {
        [Key]
        public int bestillingsid { get; set; }
        public virtual Kunder kundeid { get; set; }
        public DateTime dato { get; set; }
        public decimal total { get; set; }
        public virtual Produkt produkt { get; set; }
        //public List<Produkt> produktdetaljer { get; set; }
    }
    public class Bestiltprodukt
    {
        [Key]
        public int bestiltproduktid { get; set; }
        public int bestillingid { get; set; }
        public int produktid { get; set; }
        public int antall { get; set; }
        public decimal pris { get; set; }
        public virtual Produkt produkt { get; set; }
        public virtual Bestilling bestilling { get; set; }
    }
    public class Produkt
    {
        [Key]
        public int produktid { get; set; }
        public string navn { get; set; }
        public decimal pris { get; set; }
        public string path { get; set; }
        public string kategori { get; set; }
        public string beskrivelse { get; set; }

    }
    public class vogn
    {
        [Key]
        public int viktigid { get; set; }
        public string vognid { get; set; }
        public int produktid { get; set; }
        public int antall { get; set; }
        public System.DateTime datolaget { get; set; }
        public virtual Produkt produkt { get; set; } 
    }
    public class Kundecontext : DbContext
    {
        public Kundecontext()
            : base("name=Kunde")
        {
            Database.CreateIfNotExists();
        }

        // Your context has been configured to use a 'Kunde' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Nettbutikk2.Models.Kunde' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Kunde' 
        // connection string in the application configuration file.


        public DbSet<Kunder> kundene { get; set; }
        public DbSet<Poststed> poststedene { get; set; }
        public DbSet<Bestilling> bestillingene { get; set; }
        public DbSet<Bestiltprodukt> bestilteprodukter { get; set; }
        public DbSet<Produkt> produkter { get; set; }

        public DbSet<vogn> vogner { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}