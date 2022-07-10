using System.Collections.Generic;

namespace Web_API.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<int> OrdersIds { get; set; }
        public string AccessLevel { get; set; }
    }
}
