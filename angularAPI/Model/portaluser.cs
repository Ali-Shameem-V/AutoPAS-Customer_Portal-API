using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AutoPortal.Model
{
    public class portaluser
    {
        public portaluser()
        {
            this.userpolicylists = new HashSet<userpolicylist>();
        }
        [Key]
        public Guid? UserId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        [JsonIgnore]
        public ICollection<userpolicylist> userpolicylists { get; set; }
    }
}
