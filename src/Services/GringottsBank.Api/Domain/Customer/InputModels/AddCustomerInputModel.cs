namespace GringottsBank.Api.Domain.Customer.InputModels
{
    public record AddCustomerInputModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public ushort BirthYear { get; set; }
    }
}
