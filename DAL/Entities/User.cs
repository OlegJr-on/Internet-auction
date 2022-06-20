using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Role AccessLevel { get; set; }
    }
}
