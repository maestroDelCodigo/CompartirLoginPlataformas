using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoC.Models
{
    public class AccessLogin
    {
        [Key]
        public int? IdAccessLogin { get; set; }
        public string PlataformLink {get; set;}
        public string? UserPlatform { get; set;}
        [DataType(DataType.Password)]
        public string? Password {get; set;}

        public List<User>? User {get; set;}

    }
}
