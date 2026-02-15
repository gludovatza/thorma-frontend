namespace ThormaFrontend.Dtos
{
    public class KepDto
    {
        public string Leltar { get; set; } = null!;
        public int Fazon { get; set; }
        public string Cim { get; set; } = string.Empty;
        public int Keszult { get; set; }
        public string Anyag { get; set; } = string.Empty;
        public string Technika { get; set; } = string.Empty;
        public decimal Szeles { get; set; }
        public decimal Magas { get; set; }

        public string? FestoNev { get; set; }   // EZ NINCS AZ ENTITY-BEN
    }
}
