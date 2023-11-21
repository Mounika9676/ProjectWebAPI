using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebAPI.DTO
{
    public class UserDTO
    {
        public string? UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int SiteID { get; set; }

    }
}