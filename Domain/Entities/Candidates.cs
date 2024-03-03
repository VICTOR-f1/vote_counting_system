using System.ComponentModel.DataAnnotations;

namespace electronic_library_6.Domain.Entities
{
    public class Candidates : Entity
    {
        [StringLength(150)]
        public string Name { get; set; } = null;
        public List<Book> Books { get; set; }
    }
}
