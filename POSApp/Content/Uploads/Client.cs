using POSApp.Core.Domain;

namespace POSApp.Core.Domain
{
    public class Client:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Image { get; set; }



    }
}