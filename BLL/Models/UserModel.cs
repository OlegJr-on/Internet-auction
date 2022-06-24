using System;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<int> OrdersIds { get; set; }
        public Role AccessLevel { get; set; }
    }
}
