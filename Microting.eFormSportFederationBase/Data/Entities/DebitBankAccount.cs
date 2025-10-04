namespace Microting.eFormSportFederationBase.Data.Entities
{
    public class DebitBankAccount : BaseEntity
    {
        public string BankName { get; set; } = null!;
        public string AccountHolderName { get; set; } = null!;
        public string IBAN { get; set; } = null!;
        public string SWIFTCode { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string Currency { get; set; } = null!;
    }
}
