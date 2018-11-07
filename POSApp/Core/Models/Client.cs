namespace POSApp.Core.Models
{
    public class Client:AuditableEntity
    {
        public int Id { get; set; }
        public string BusinessReference { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string BusinessHID { get; set; }

        public string ApiURL { get; set; }

        public int NoOfBranches { get; set; }

        public int NoOfLicences { get; set; }









    }
}