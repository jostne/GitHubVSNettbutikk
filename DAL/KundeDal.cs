using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web.Mvc;
using System.IO;


namespace DAL
{
    public class KundeDal : DAL.IKundeRepository
    {

        public bool Bruker_i_DB(Kunde innkunde)
            {
                using (var db = new Kundecontext())
                {
                    byte[] passordDb = lagHash(innkunde.passord);
                    var funnetkunde = db.kundene.FirstOrDefault
                        (b => b.passord == passordDb && b.epost == innkunde.epost);
                    if (funnetkunde == null)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }

                }
            }

            public bool Admin_i_DB(sikkerAdmin innadmin)
            {
                using (var db = new Kundecontext())
                {
                    byte[] passordDb = lagHash(innadmin.passord);
                    var funnetadmin = db.administratorer.FirstOrDefault
                        (b => b.password == passordDb && b.brukarnavn == innadmin.brukernavn);
                    if (funnetadmin == null)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }

                }
            }
            public bool slettordre(int id)
            {
                var db = new Kundecontext();
                Bestilling kunde = db.bestillingene.Find(id);
                if (kunde == null)
                {
                    return false;
                }
                else
                {
                    db.bestillingene.Remove(kunde);
                    db.SaveChanges();
                    return true;
                }
                
            }
            public bool finnbruker(string innKunde)
            {
                var db = new Kundecontext();
                var kunde = db.kundene.FirstOrDefault(p => p.epost == innKunde);
                if (kunde != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public bool finnbruker1(int id)
            {
                var db = new Kundecontext();
                var kunde = db.kundene.Find(id);
                if (kunde != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public bool endreOrdren(Bestillingen innbestilling)
            {
                var db = new Kundecontext();
                var ordre = db.bestillingene.Find(innbestilling.bestillingsid);
                if (ordre == null)
                {
                    return false;
                }
                else
                {
                    ordre.dato = innbestilling.dato;
                    ordre.kundeid = new Kunder()
                    {
                        fornavn = innbestilling.kundeid.fornavn,
                        etternavn = innbestilling.kundeid.etternavn,
                        epost = innbestilling.kundeid.epost,
                        telefonnr = innbestilling.kundeid.telefonnr,
                        adresse = innbestilling.kundeid.adresse,
                        postnr = innbestilling.kundeid.postnr,
                    };
                    ordre.produkt = new Produkt()
                    {
                        produktid = innbestilling.produkt.produktid,
                        navn = innbestilling.produkt.navn,
                        pris = innbestilling.produkt.pris,
                        path = innbestilling.produkt.path,
                        kategori = innbestilling.produkt.kategori,
                        beskrivelse = innbestilling.produkt.beskrivelse
                    };
                    ordre.total = innbestilling.total;
                    db.SaveChanges();
                    return true;


                } 
            }
            public Kunde finnkundemedepost(string id)
            {
                var db = new Kundecontext();
                var kunde = db.kundene.FirstOrDefault(k => k.epost == id);
                if (kunde == null)
                    return null;
                else
                {
                    var utkunde = new Kunde()
                    {
                        kundeid = kunde.kundeid,
                        fornavn = kunde.fornavn,
                        etternavn = kunde.etternavn,
                        epost = kunde.epost,
                        postnr = kunde.postnr,
                        poststed = kunde.poststed.poststed,
                        telefonnr = kunde.telefonnr,
                        adresse = kunde.adresse
                    };
                    return utkunde;

                }


            }
            public Kunde finnkunden(int id)
            {
                var db = new Kundecontext();
                var kunde = db.kundene.Find(id);
                if (kunde == null)
                    return null;
                else
                {
                    var utkunde = new Kunde()
                    {
                        kundeid = kunde.kundeid,
                        fornavn = kunde.fornavn,
                        etternavn = kunde.etternavn,
                        epost = kunde.epost,
                        postnr = kunde.postnr,
                        poststed = kunde.poststed.poststed,
                        telefonnr = kunde.telefonnr,
                        adresse = kunde.adresse
                    };
                    return utkunde;
                    
                }
              
                
            }
            public bool finnadmin(string navn)
            {
                var db = new Kundecontext();
                var admin = db.administratorer.FirstOrDefault(p => p.brukarnavn == navn);
                if (admin != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public List<Bestillingen> finnordre(string navn)
            {
                var db = new Kundecontext();
                var kunden = db.kundene.FirstOrDefault(p => p.epost == navn);

                List<Bestillingen> ordre = db.bestillingene.Select(p => new Bestillingen()
                    {
                        bestillingsid = p.bestillingsid,
                        kundeid = new Kunde() { 
                        kundeid = p.kundeid.kundeid,
                        fornavn = p.kundeid.fornavn,
                        etternavn = p.kundeid.etternavn,
                        epost = p.kundeid.epost,
                        postnr = p.kundeid.postnr,
                        poststed = p.kundeid.poststed.poststed,
                        telefonnr = p.kundeid.telefonnr
                        },
                        dato = p.dato,
                        total = p.total,
                        produkt = new Produkten()
                        {
                            produktid = p.produkt.produktid,
                            navn = p.produkt.navn,
                            pris = p.produkt.pris,
                            path = p.produkt.path,
                            kategori = p.produkt.kategori,
                            beskrivelse = p.produkt.beskrivelse
                        }
                    }).ToList();
                        
                
                return ordre;
            }
            public List<Kunde> ListAlleKunder()
            {
                var db = new Kundecontext();
                List<Kunde> listeavkunder = db.kundene.Select(p => new Kunde()
                {
                    kundeid = p.kundeid,
                    fornavn = p.fornavn,
                    etternavn = p.etternavn,
                    adresse = p.adresse,
                    poststed = p.poststed.poststed,
                    postnr = p.postnr,
                    telefonnr = p.telefonnr,
                    epost = p.epost
                    
                
                }).ToList();
                return listeavkunder;
            }
            
            public List<Produkten> ListAlleProdukter()
            {
                var db = new Kundecontext();
                List<Produkten> listavprodukter = db.produkter.Select(p => new Produkten()
                    {
                        produktid = p.produktid,
                        navn = p.navn,
                        kategori = p.kategori,
                        path = p.path,
                        beskrivelse = p.beskrivelse,
                        pris = p.pris
                    }).ToList();
                return listavprodukter;
            }
            public List<Bestillingen> ListAlleOrdre()
            {
                var db = new Kundecontext();
                List<Bestillingen> listeavordre = db.bestillingene.Select(p => new Bestillingen()
                    {
                        bestillingsid = p.bestillingsid,
                        kundeid = new Kunde()
                        {
                            kundeid = p.kundeid.kundeid,
                            fornavn = p.kundeid.fornavn,
                            etternavn = p.kundeid.etternavn,
                            epost = p.kundeid.epost,
                            postnr = p.kundeid.postnr,
                            poststed = p.kundeid.poststed.poststed,
                            telefonnr = p.kundeid.telefonnr
                        },
                        dato = p.dato,
                        total = p.total,
                        produkt = new Produkten()
                        {
                            produktid = p.produkt.produktid,
                            navn = p.produkt.navn,
                            pris = p.produkt.pris,
                            path = p.produkt.path,
                            kategori = p.produkt.kategori,
                            beskrivelse = p.produkt.beskrivelse
                        }


                    }).ToList();
                return listeavordre;
            }
            public Bestillingen finnordren(int id)
            {
                var db = new Kundecontext();
                Bestillingen ordre = (Bestillingen)db.bestillingene.Select(p => new Bestillingen() 
                {
                    bestillingsid = p.bestillingsid,
                    kundeid = new Kunde()
                    {
                        kundeid = p.kundeid.kundeid,
                        fornavn = p.kundeid.fornavn,
                        etternavn = p.kundeid.etternavn,
                        epost = p.kundeid.epost,
                        postnr = p.kundeid.postnr,
                        poststed = p.kundeid.poststed.poststed,
                        telefonnr = p.kundeid.telefonnr
                    },
                    dato = p.dato,
                    total = p.total,
                    produkt = new Produkten()
                    {
                        produktid = p.produkt.produktid,
                        navn = p.produkt.navn,
                        pris = p.produkt.pris,
                        path = p.produkt.path,
                        kategori = p.produkt.kategori,
                        beskrivelse = p.produkt.beskrivelse
                    }
                });
                return ordre;
            }
            public bool endreBruker( Kunde innkunde)
            {
                //bool loggetinn = (bool)Session["LoggetInn"];
                var db = new Kundecontext();
                
                try
                {
                    //Kunder person = db.kundene.FirstOrDefault(p => p.epost == navnet);
                    Kunder person = db.kundene.Find(innkunde.kundeid);
                    person.epost = innkunde.epost;
                    person.adresse = innkunde.adresse;
                    person.etternavn = innkunde.etternavn;
                    person.fornavn = innkunde.fornavn;
                    person.postnr = innkunde.postnr;
                    //person.poststed = innkunde.poststed;
                    person.telefonnr = innkunde.telefonnr;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public bool endreprodukt(Produkten innkunde)
            {
                //bool loggetinn = (bool)Session["LoggetInn"];
                var db = new Kundecontext();

                try
                {
                    //Kunder person = db.kundene.FirstOrDefault(p => p.epost == navnet);
                    Produkt produk = db.produkter.Find(innkunde.produktid);
                    produk.kategori = innkunde.kategori;
                    produk.navn = innkunde.navn;
                    produk.beskrivelse = innkunde.beskrivelse;
                    produk.path = innkunde.path;
                    produk.pris = innkunde.pris;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            
            public bool endreAdmin(Kunde innkunde)
            {
                var db = new Kundecontext();
                

                try
                {
                    Kunder person = db.kundene.Find(innkunde.kundeid);  
                    person.epost = innkunde.epost;
                    person.adresse = innkunde.adresse;
                    person.etternavn = innkunde.etternavn;
                    person.fornavn = innkunde.fornavn;
                    person.postnr = innkunde.postnr;
                    person.poststed.poststed = innkunde.poststed;
                    person.telefonnr = innkunde.telefonnr;
                    db.SaveChanges();
                     
                    return true;
                }
                catch
                {
                    return false;
                }
            }
           
       
            public bool Opprettprodukt(FormCollection innProdukt)
            {
                try
                {
                    using (var db = new Kundecontext())
                    {
                        var nyttProduk = new Produkt();
                        nyttProduk.navn = innProdukt["produktnavn"];
                        decimal prisen = decimal.Parse(innProdukt["pris"]);
                        nyttProduk.beskrivelse = innProdukt["beskrivelse"];
                        nyttProduk.kategori = innProdukt["kategori"];
                        nyttProduk.path = innProdukt["path"];
                        nyttProduk.pris = prisen;
                        db.produkter.Add(nyttProduk);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            
            public List<Produkten> sykkel()
            {
                var db = new Kundecontext();
                List<Produkten> Listeavprodukter = db.produkter.Select(p => new Produkten() 
                {
                    produktid = p.produktid,
                    navn = p.navn,
                    pris = p.pris,
                    path = p.path,
                    kategori = p.kategori,
                    beskrivelse = p.beskrivelse
                }).ToList();
                return Listeavprodukter;
            }
            public List<Produkten> del()
            {
                var db = new Kundecontext();
                List<Produkten> Listeavprodukter = db.produkter.Select(p => new Produkten()
                {
                    produktid = p.produktid,
                    navn = p.navn,
                    pris = p.pris,
                    path = p.path,
                    kategori = p.kategori,
                    beskrivelse = p.beskrivelse
                }).ToList();
                return Listeavprodukter;
            }

            public Produkten info(int id)
            {
                var db = new Kundecontext();
                Produkt produkt = db.produkter.Find(id);
                var produktet = new Produkten() 
                {
                    produktid = produkt.produktid,
                    navn = produkt.navn,
                    pris = produkt.pris,
                    path = produkt.path,
                    kategori = produkt.kategori,
                    beskrivelse = produkt.beskrivelse
                };
                if (produkt == null)
                {
                    return null;
                }
                return produktet;
            }
            public bool slett(int id)
            {
                var db = new Kundecontext();
                Kunder kunde = db.kundene.Find(id);
                if (kunde == null)
                {
                    return false;
                }
                else 
                { 
                db.kundene.Remove(kunde);
                db.SaveChanges();
                return true;
                }
            }
            public string getpath(int id)
            {
                var db = new Kundecontext();
                Produkt pro = db.produkter.Find(id);
                return pro.path;
            }
            public string getkat(int id)
            {
                var db = new Kundecontext();
                Produkt pro = db.produkter.Find(id);
                return pro.kategori;
            }
            public string getkate(string navn)
            {
                var db = new Kundecontext();
                Produkt pro = db.produkter.FirstOrDefault(p => p.path == navn);
                return pro.kategori;
            }
            public bool slettprodukt(int id)
            {
                var db = new Kundecontext();
                Produkt pro = db.produkter.Find(id);
                
                if (pro == null)
                {
                    return false;
                }
                else
                {
                    db.produkter.Remove(pro);
                    db.SaveChanges();
                    return true;
                }
            }
            public List<Produkten> utstyr()
            {
                var db = new Kundecontext();
                List<Produkten> Listeavprodukter = db.produkter.Select(p => new Produkten()
                {
                    produktid = p.produktid,
                    navn = p.navn,
                    pris = p.pris,
                    path = p.path,
                    kategori = p.kategori,
                    beskrivelse = p.beskrivelse
                }).ToList();
                return Listeavprodukter;
            }
            private static byte[] lagHash(string innpassord)
            {
                byte[] innData, utData;
                var algoritme = System.Security.Cryptography.SHA256.Create();
                innData = System.Text.Encoding.ASCII.GetBytes(innpassord);
                utData = algoritme.ComputeHash(innData);
                return utData;
            }
            
            public bool opprettAdmin(sikkerAdmin innadmin)
            {
                
                try
                {
                    using (var db = new Kundecontext())
                    {
                        var nyAdmin = new Admin();
                        byte[] passordDb = lagHash(innadmin.passord);
                        nyAdmin.password = passordDb;
                        nyAdmin.brukarnavn = innadmin.brukernavn;
                        db.administratorer.Add(nyAdmin);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            public int finnid(string navn)
            {
                var db = new Kundecontext();
                var kunden = db.kundene.FirstOrDefault(p => p.epost == navn);
                return kunden.kundeid;
            }
            public bool OprettBruker(Kunde innkunde)
            {
               
                try
                {
                    using (var db = new Kundecontext())
                    {

                        var nyKunde = new Kunder();
                        byte[] passordDb = lagHash(innkunde.passord);
                        nyKunde.passord = passordDb;
                        nyKunde.fornavn = innkunde.fornavn;
                        nyKunde.etternavn = innkunde.etternavn;
                        nyKunde.epost = innkunde.epost;
                        nyKunde.adresse = innkunde.adresse;
                        nyKunde.telefonnr = innkunde.telefonnr;

                        string innpostnr = innkunde.postnr;
                        /*nyKunde.fornavn = innListe["fornavn"];
                        nyKunde.etternavn = innListe["etternavn"];
                        nyKunde.adresse = innListe["adresse"];
                        nyKunde.epost = innListe["epost"];
                        nyKunde.passord = innListe["passord"];
                        string innpostnr = innListe["postnr"];
                        string inntlf = innListe["telefonnr"];*/

                        var funnetPoststed = db.poststedene.FirstOrDefault(p => p.postnr == innpostnr.ToString());
                        if (funnetPoststed == null)
                        {
                            var nyttPoststed = new Poststed();
                            nyttPoststed.postnr = innkunde.postnr.ToString();
                            nyttPoststed.poststed = innkunde.poststed.ToString();
                            //nyttPoststed.postnr = innkunde["postnr"];
                            //nyttPoststed.poststed = innListe["poststed"];
                            db.poststedene.Add(nyttPoststed);
                            nyKunde.poststed = nyttPoststed;
                            nyKunde.postnr = innkunde.postnr;
                            //nyKunde.postnr = int.Parse(innListe["postnr"]);
                        }
                        else
                        {
                            nyKunde.poststed = funnetPoststed;
                            nyKunde.postnr = innkunde.postnr;
                            //nyKunde.postnr = int.Parse(innListe["postnr"]);
                        }
                        db.kundene.Add(nyKunde);
                        db.SaveChanges();
                        return true;



                    }
                }
                catch
                {
                    return false;
                }
            }

           
        }
    
}
