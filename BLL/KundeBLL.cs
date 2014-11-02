using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Web.Mvc;
namespace BLL
{
    public class KundeBLL : BLL.IKundeLogikk
    {

        private IKundeRepository _repository;

        public KundeBLL()
        {
            _repository = new KundeDal();
        }

        public KundeBLL(IKundeRepository stub)
        {
            _repository = stub;
        }
        public List<Kunde> hentalle()
        {
            var KundeDal = new KundeDal();
            List<Kunde> allepersoner = _repository.ListAlleKunder();
            return allepersoner;
        }
        public List<Produkten> hentalleordre()
        {
            var KundeDal = new KundeDal();
            List<Produkten> alleprodukter = _repository.ListAlleProdukter();
            //List<Produkten> alleprodukter = KundeDal.ListAlleProdukter();
            return alleprodukter;
        }

        public List<Bestillingen> hentordre()
        {
            var KundeDal = new KundeDal();
            List<Bestillingen> alleordre = KundeDal.ListAlleOrdre();
            return alleordre;
        }
        public bool slettordre(int id)
        {
            var Kundedal = new KundeDal();
            if (Kundedal.slettordre(id))
                return true;
            else
                return false;
        }
        public List<Produkten> sykkel()
        {
            var Kundedal = new KundeDal();
            List<Produkten> allesykler = Kundedal.sykkel();
            return allesykler;
        }
        public List<Produkten> del()
        {
            var Kundedal = new KundeDal();
            List<Produkten> alledeler = Kundedal.del();
            return alledeler;
        }
        public List<Produkten> utstyr()
        {
            var Kundedal = new KundeDal();
            List<Produkten> altutstyr = Kundedal.utstyr();
            return altutstyr;
        }
        public Bestillingen finnordren(int id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.finnordren(id);
        }
        public bool endrekunde(Kunde innkunde)
        {
            var KundeDal = new KundeDal();
            return _repository.endreBruker(innkunde);
        }
        public bool endreORdre(Bestillingen inn)
        {
            var Kundedal = new KundeDal();
            return Kundedal.endreOrdren(inn);
        }
        public bool endreprodukt(Produkten innprodukt)
        {
            var KundeDal = new KundeDal();
            return _repository.endreprodukt(innprodukt);
        }
        public Produkten endreprodukten(int innprodukt)
        {
            var KundeDal = new KundeDal();
            return KundeDal.info(innprodukt);
        }
        public bool endreadmin(Kunde innkunde)
        {
            var KundeDal = new KundeDal();
            return KundeDal.endreAdmin(innkunde);
        }

        public bool opprettprodukt(FormCollection innprodukt)
        {
            var KundeDal = new KundeDal();
            return _repository.Opprettprodukt(innprodukt);
        }
        public Produkten info(int id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.info(id);
        }
        public bool slett(int id)
        {
            var kundeDal = new KundeDal();
            return _repository.slett(id);
        }
        public string getpath(int id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.getpath(id);
        }
        public string getkat(int id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.getkat(id);
        }
        public string getkate(string navn)
        {
            var kundedal = new KundeDal();
            return kundedal.getkate(navn);
        }
        public bool slettprodukt(int id)
        {
            var kundeDal = new KundeDal();

            return kundeDal.slettprodukt(id);
        }
        
        public bool opprettAdmin(sikkerAdmin admin)
        {
            var KundeDal = new KundeDal();
            return _repository.opprettAdmin(admin);
        }
        public bool opprettBruker(Kunde innkunde)
        {
            var KundeDal = new KundeDal();
            return KundeDal.OprettBruker(innkunde);
        }

        public bool Bruker_I_DB(Kunde innkunde)
        {
            var KundeDal = new KundeDal();
            return KundeDal.Bruker_i_DB(innkunde);
        }
        public bool Admin_I_DB(sikkerAdmin innAdmin)
        {
            var KundeDal = new KundeDal();
            return KundeDal.Admin_i_DB(innAdmin);
        }
        public bool finnbruker(string navn)
        {
            var KundeDal = new KundeDal();
            return KundeDal.finnbruker(navn);
        }
        public Kunde finnbrukeren(int id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.finnkunden(id);

        }
        public Kunde finnkundemednavn(string id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.finnkundemedepost(id);
        }
        public bool finnbruker1(int navn)
        {
            var KundeDal = new KundeDal();
            return KundeDal.finnbruker1(navn);
        }
        public List<Bestillingen> finnordre(string navn)
        {
            var Kundedal = new KundeDal();
            return Kundedal.finnordre(navn);
        }
        public bool finnadmin(string navn)
        {
            var KundeDal = new KundeDal();
            return KundeDal.finnadmin(navn);
        }
        public Kunde finnkunde(int id)
        {
            var Kundedal = new KundeDal();
            return Kundedal.finnkunden(id);
            
        }
        public int finnid(string navn)
        {
            var Kundedal = new KundeDal();
            return Kundedal.finnid(navn);
        }
    }
}
