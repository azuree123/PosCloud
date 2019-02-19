namespace POSApp.Core.Models
{
    public class Location:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

    }
}