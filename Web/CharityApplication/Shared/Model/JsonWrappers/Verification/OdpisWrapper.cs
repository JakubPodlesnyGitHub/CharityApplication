using System.Text.Json.Serialization;

namespace CharityApplication.Shared.Model.JsonWrappers.Verification
{
    public class Adres
    {
        [JsonPropertyName("ulica")]
        public string Ulica { get; set; }

        [JsonPropertyName("nrDomu")]
        public string NrDomu { get; set; }

        [JsonPropertyName("miejscowosc")]
        public string Miejscowosc { get; set; }

        [JsonPropertyName("kodPocztowy")]
        public string KodPocztowy { get; set; }

        [JsonPropertyName("poczta")]
        public string Poczta { get; set; }

        [JsonPropertyName("kraj")]
        public string Kraj { get; set; }
    }

    public class Dane
    {
        [JsonPropertyName("dzial1")]
        public Dzial1 Dzial1 { get; set; }
    }

    public class DanePodmiotu
    {
        [JsonPropertyName("formaPrawna")]
        public string FormaPrawna { get; set; }

        [JsonPropertyName("identyfikatory")]
        public Identyfikatory Identyfikatory { get; set; }

        [JsonPropertyName("nazwa")]
        public string Nazwa { get; set; }

        [JsonPropertyName("czyProwadziDzialalnoscZInnymiPodmiotami")]
        public bool CzyProwadziDzialalnoscZInnymiPodmiotami { get; set; }

        [JsonPropertyName("czyPosiadaStatusOPP")]
        public bool CzyPosiadaStatusOPP { get; set; }
    }

    public class Dzial1
    {
        [JsonPropertyName("danePodmiotu")]
        public DanePodmiotu DanePodmiotu { get; set; }

        [JsonPropertyName("siedzibaIAdres")]
        public SiedzibaIAdres SiedzibaIAdres { get; set; }
    }

    public class Identyfikatory
    {
        [JsonPropertyName("regon")]
        public string Regon { get; set; }

        [JsonPropertyName("nip")]
        public string Nip { get; set; }
    }

    public class NaglowekA
    {
        [JsonPropertyName("rejestr")]
        public string Rejestr { get; set; }

        [JsonPropertyName("numerKRS")]
        public string NumerKRS { get; set; }

        [JsonPropertyName("stanZDnia")]
        public string StanZDnia { get; set; }

        [JsonPropertyName("dataRejestracjiWKRS")]
        public string DataRejestracjiWKRS { get; set; }

        [JsonPropertyName("numerOstatniegoWpisu")]
        public int NumerOstatniegoWpisu { get; set; }

        [JsonPropertyName("stanPozycji")]
        public int StanPozycji { get; set; }
    }

    public class CopyModel
    {
        [JsonPropertyName("odpis")]
        public OdpisWrapper Odpis { get; set; }
    }

    public class OdpisWrapper
    {
        [JsonPropertyName("rodzaj")]
        public string Type { get; set; }

        [JsonPropertyName("naglowekA")]
        public NaglowekA NaglowekA { get; set; }

        [JsonPropertyName("dane")]
        public Dane Dane { get; set; }
    }

    public class Siedziba
    {
        [JsonPropertyName("kraj")]
        public string Kraj { get; set; }

        [JsonPropertyName("wojewodztwo")]
        public string Wojewodztwo { get; set; }

        [JsonPropertyName("powiat")]
        public string Powiat { get; set; }

        [JsonPropertyName("gmina")]
        public string Gmina { get; set; }

        [JsonPropertyName("miejscowosc")]
        public string Miejscowosc { get; set; }
    }

    public class SiedzibaIAdres
    {
        [JsonPropertyName("siedziba")]
        public Siedziba Siedziba { get; set; }

        [JsonPropertyName("adres")]
        public Adres Adres { get; set; }
    }
}