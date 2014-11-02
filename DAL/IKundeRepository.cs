using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web.Mvc;

namespace DAL
{
    public interface IKundeRepository
    {
        List<Produkten> ListAlleProdukter();
        List<Kunde> ListAlleKunder();
        bool opprettAdmin(sikkerAdmin innAdmin);
        bool Opprettprodukt(FormCollection innProdukt);
        bool slett(int id);
        bool endreBruker(Kunde innkunde);
        bool endreprodukt(Produkten innkunde);

        bool endreAdmin(Kunde innkunde);
        //bool Opprettprodukt(FormCollection innProdukt);
    }
}
