using System.ComponentModel.DataAnnotations;

namespace SYNTAXdb.DAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
