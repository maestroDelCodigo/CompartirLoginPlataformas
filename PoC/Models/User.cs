using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoC.Models
{
    public class User
    {
        [Key]
        public int? IdUser { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<AccessLogin>?  AccessLogins { get; set; }

    }
    
}
