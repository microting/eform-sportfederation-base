namespace Microting.eFormSportFederationBase.Data.Entities
{
    public class Federation : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string Facebook { get; set; } = null!;
        public string Instagram { get; set; } = null!;
        public string Twitter { get; set; } = null!;

        // Foreign Keys
        public int? ContactAddressId { get; set; }
        public Address? ContactAddress { get; set; }

        public int? BillingAddressId { get; set; }
        public Address? BillingAddress { get; set; }

        public int? DebitBankAccountId { get; set; }
        public DebitBankAccount? DebitBankAccount { get; set; }
    }
}
