namespace BankRadar.Models
{
    public class Simulacao
    {
        public int Id { get; set; }
        public string Banco { get; set; }
        public decimal TaxaMensal { get; set; }
        public decimal TaxaAnual { get; set; }
        public DateTime DataColeta { get; set; }
    }
}