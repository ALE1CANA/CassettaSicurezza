
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassettaSicurezza
{
    class OggettoSegreto
    {
        private string id;
        private double valoreDichiarato;
        private double valoreAssicurato;

        public string Id
        {
            get { return id; }
        }

        public double ValoreDichiarato
        {
            get { return valoreDichiarato; }
        }

        public double ValoreAssicurato
        {
            get { return valoreAssicurato; }
            protected set { valoreAssicurato = value; }
        }

        public OggettoSegreto(string id, double valoreDichiarato)
        {
            this.id = id;
            this.valoreDichiarato = valoreDichiarato;
            this.valoreAssicurato = 0; // Default
        }

    }

    class GioielloPrezioso : OggettoSegreto
    {
        private string tipo;
        public GioielloPrezioso(string id, double valoreDichiarato, string tipo) : base(id, valoreDichiarato)
        {
            this.tipo = tipo;
            CalcolaValoreAssicurato();
        }

        public void CalcolaValoreAssicurato()
        {
            ValoreAssicurato = ValoreDichiarato * 5;
        }
    }

    class DocumentoLegale : OggettoSegreto
    {
        private string tipo;
        public DocumentoLegale(string id, double valoreDichiarato, string tipo) : base(id, valoreDichiarato)
        {
            this.tipo = tipo;
            CalcolaValoreAssicurato();
        }

        public void CalcolaValoreAssicurato()
        {
            if (ValoreDichiarato >= 100)
            {
                ValoreAssicurato = ValoreDichiarato;
            }
            else
            {
                ValoreAssicurato = 50;
            }
        }
    }

    class ChiaveDiAccesso : OggettoSegreto
    {
        private string tipo;
        public ChiaveDiAccesso(string id, double valoreDichiarato, string tipo) : base(id, valoreDichiarato)
        {
            this.tipo = tipo;
            CalcolaValoreAssicurato();
        }

        public void CalcolaValoreAssicurato()
        {
            ValoreAssicurato = ValoreDichiarato * 1000;
        }
    }

    class CassettaDiSicurezza
    {
        private string codiceSeriale;
        private string pin;
        private OggettoSegreto oggetto;

        public string CodiceSeriale
        {
            get { return codiceSeriale; }
        }

        public OggettoSegreto Oggetto
        {
            get { return oggetto; }
        }

        public CassettaDiSicurezza(string codiceSeriale, string pin)
        {
            this.codiceSeriale = codiceSeriale;
            this.pin = pin;
            this.oggetto = null;
        }

        public void InserisciOggetto(OggettoSegreto oggetto, string pin)
        {
            if (this.pin == pin && this.oggetto == null)
            {
                this.oggetto = oggetto;
            }
            return;
        }

        public void RimuoviOggetto(string pin)
        {
            if (this.pin == pin && this.oggetto != null)
            {
                this.oggetto = null;
            }
            return;
        }
    }

    class CassettaDiSicurezzaSpeciale : CassettaDiSicurezza
    {
        private double valoreAssicurato;

        public double ValoreAssicurato
        {
            get { return valoreAssicurato; }
        }

        public CassettaDiSicurezzaSpeciale(string codiceSeriale, string pin) : base(codiceSeriale, pin)
        { }

        public void CalcolaValoreAssicurato()
        {
            OggettoSegreto oggetto = Oggetto;

            if (oggetto == null)
            {
                valoreAssicurato = 0;
            }
            else if (oggetto is GioielloPrezioso)
            {
                valoreAssicurato = oggetto.ValoreAssicurato * 0.9;
            }
            else if (oggetto is DocumentoLegale)
            {
                valoreAssicurato = oggetto.ValoreAssicurato * 0.8;
            }
            else if (oggetto is ChiaveDiAccesso)
            {
                valoreAssicurato = oggetto.ValoreAssicurato * 0.7;
            }
            else
            {
                valoreAssicurato = 0;
            }
        }
    }


    class Program
    {
        static void Main()
        {
            CassettaDiSicurezza cassetta1 = new CassettaDiSicurezza("001", "11111");
            CassettaDiSicurezza cassetta2 = new CassettaDiSicurezza("002", "22222");
            CassettaDiSicurezza cassetta3 = new CassettaDiSicurezza("003", "33333");

            GioielloPrezioso gioiello = new GioielloPrezioso("J001", 1000, "anello");

            DocumentoLegale documento = new DocumentoLegale("D001", 50, "testamento");

            ChiaveDiAccesso chiave = new ChiaveDiAccesso("C001", 5, "fisico");

            cassetta1.InserisciOggetto(gioiello, "11111");
            cassetta2.InserisciOggetto(documento, "22222");
            cassetta3.InserisciOggetto(chiave, "33333");

            Console.WriteLine("Cassette di Sicurezza Normali:");
            VisualizzaCassetta(cassetta1);
            VisualizzaCassetta(cassetta2);
            VisualizzaCassetta(cassetta3);

            CassettaDiSicurezzaSpeciale cassettaSpec1 = new CassettaDiSicurezzaSpeciale("S001", "44444");
            CassettaDiSicurezzaSpeciale cassettaSpec2 = new CassettaDiSicurezzaSpeciale("S002", "55555");
            CassettaDiSicurezzaSpeciale cassettaSpec3 = new CassettaDiSicurezzaSpeciale("S003", "66666");

            OggettoSegreto oggettoRimosso;

            oggettoRimosso = cassetta1.Oggetto;
            cassetta1.RimuoviOggetto("11111");
            cassettaSpec1.InserisciOggetto(oggettoRimosso, "44444");

            oggettoRimosso = cassetta2.Oggetto;
            cassetta2.RimuoviOggetto("22222");
            cassettaSpec2.InserisciOggetto(oggettoRimosso, "55555");

            oggettoRimosso = cassetta3.Oggetto;
            cassetta3.RimuoviOggetto("33333");
            cassettaSpec3.InserisciOggetto(oggettoRimosso, "66666");


            cassettaSpec1.CalcolaValoreAssicurato();
            cassettaSpec2.CalcolaValoreAssicurato();
            cassettaSpec3.CalcolaValoreAssicurato();

            Console.WriteLine("\nCassette di Sicurezza Speciali:");
            VisualizzaCassettaSpeciale(cassettaSpec1);
            VisualizzaCassettaSpeciale(cassettaSpec2);
            VisualizzaCassettaSpeciale(cassettaSpec3);
            Console.ReadLine();
        }

        static void VisualizzaCassetta(CassettaDiSicurezza cassetta)
        {
            if (cassetta.Oggetto != null)
            {
                OggettoSegreto oggetto = cassetta.Oggetto;
                Console.WriteLine(
                    "ID: " + oggetto.Id +
                    ", Tipo: " + oggetto.GetType().Name +
                    ", Valore Dichiarato: " + oggetto.ValoreDichiarato +
                    ", Valore Assicurato: " + oggetto.ValoreAssicurato
                );
            }
            else
            {
                Console.WriteLine("La cassetta Ã¨ vuota.");
            }
        }

        static void VisualizzaCassettaSpeciale(CassettaDiSicurezzaSpeciale cassettaSpeciale)
        {
            if (cassettaSpeciale.Oggetto != null)
            {
                OggettoSegreto oggetto = cassettaSpeciale.Oggetto;
                Console.WriteLine(
                    "ID: " + oggetto.Id +
                    ", Tipo: " + oggetto.GetType().Name +
                    ", Valore Dichiarato: " + oggetto.ValoreDichiarato +
                    ", Valore Assicurato Oggetto: " + oggetto.ValoreAssicurato +
                    ", Valore Assicurato Cassetta: " + cassettaSpeciale.ValoreAssicurato
                );
            }
            else
            {
                Console.WriteLine("La cassetta speciale Ã¨ vuota.");
            }
        }
    }


}
