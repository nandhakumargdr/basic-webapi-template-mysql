using System;

namespace webapi_basic_mysql.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? LastActive { get; set; }
    }
}