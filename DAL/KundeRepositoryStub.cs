using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web.Mvc;
using DAL;


namespace DAL
{
    public class KundeRepositoryStub : DAL.IKundeRepository
    { 
        public  List<Produkten> ListAlleProdukter()
        {
            var produkter = new List<Produkten>();
            var pro = new Produkten()
            {
                produktid = 1,
                beskrivelse = "løk",
                kategori = "Sykkel",
                navn = "",
                path = "",
                pris = 200

            };
            produkter.Add(pro);
            produkter.Add(pro);
            produkter.Add(pro);
            return produkter;


        }
      
        public List<Kunde> ListAlleKunder()
        {
            var kundeliste = new List<Kunde>();
            var kunde = new Kunde()
            {
                kundeid = 1,
                fornavn = "thanh",
                etternavn = "test",
                telefonnr = 12345678,
                postnr = "1234",
                poststed = "agder",
                adresse = "oslo 11",
                epost = "dad@hotmail.no",
                passord = "test"


            };

            kundeliste.Add(kunde);
            kundeliste.Add(kunde);
            kundeliste.Add(kunde);
            return kundeliste;

        }
        public bool opprettAdmin(sikkerAdmin innAdmin)
        {
            if(innAdmin.adminid == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool slett(int id)
        {
            if(id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool endreBruker(Kunde innkunde)
        {
            if (innkunde.kundeid == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

       public bool endreprodukt(Produkten innkunde)
        {
            
            if(innkunde.kategori == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

      public bool Opprettprodukt(FormCollection innProdukt)
       {
          if(innProdukt.ToString() == null)
          {
              return false;
          }
          else
          {
              return true;
          }
       }
        public bool endreAdmin(Kunde innkunde)
       {
            if(innkunde.kundeid == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
       }

        /*
        public bool endreBruker(Kunde innkunde)
        {
            if(innkunde.kundeid == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool Opprettprodukt(FormCollection innProdukt)
        {
           if(innProdukt["kategori"] == null)
           {
               return false;
           }
           else
           {
               return true;
           }
        }*/
    }
}
