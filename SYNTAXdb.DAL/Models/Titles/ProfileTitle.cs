using SYNTAXdb.DAL.Models.Profiles;
using System.ComponentModel.DataAnnotations.Schema;

namespace SYNTAXdb.DAL.Models.Titles
{
    public class ProfileTitle : Entity
    {
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        public int TitleId { get; set; }
        [ForeignKey("TitleId")]
        public Title Title { get; set; }
    }
}
