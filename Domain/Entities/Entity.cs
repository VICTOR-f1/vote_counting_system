using System.ComponentModel.DataAnnotations;

namespace electronic_library_6.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
