using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoC.Models
{
    [NotMapped]
    public class UserLogin
    {
        [Key]
        public int? Id { get; set; }
        public int IdUser { get; set; }
        public int IdAccessLogin { get; set; }
        [NotMapped]
        public User? User { get; set; }
        [NotMapped]
        public AccessLogin? AccessLogin { get; set; }
        [NotMapped]
        public List<User>? Users { get; set; }
        [NotMapped]
        public List<AccessLogin>? Logins { get; set; }
    }
}
