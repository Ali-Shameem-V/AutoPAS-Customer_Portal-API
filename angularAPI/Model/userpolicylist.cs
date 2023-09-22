using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AutoPortal.Model
{
    public class userpolicylist
    {
        [Key]
        public Guid? UserPolicyId { get; set; }
        [ForeignKey("portalusers")]
        public Guid? UserId { get; set; }

        public int PolicyNumber { get; set; }

        [JsonIgnore]
        public portaluser? portaluser { get; set; }
    }
}
