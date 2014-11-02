using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web.Mvc;

namespace BLL
{
    public interface IKundeLogikk
    {
        List<Produkten> hentalleordre();
        List<Kunde> hentalle();
        bool opprettAdmin(sikkerAdmin admin);
        bool slett(int id);
        bool endrekunde(Kunde innkunde);
        //string finnkundemednavn(string navn);
        bool endreprodukt(Produkten innkunde);

        bool endreadmin(Kunde innkunde);
        bool opprettprodukt(FormCollection innProdukt);

        /*bool endrekunde(Kunde innkunde);
        bool opprettprodukt(FormCollection innProdukt);*/
    }
}
