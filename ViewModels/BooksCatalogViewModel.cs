using electronic_library_6.Domain.Entities;

namespace electronic_library_6.ViewModels
{
    public class BooksCatalogViewModel
    {
        public List<Book> Books { get; set; }
        public List<Categories> Categories { get; set; }
        public List<PollingStations> PollingStations { get; set; }

    }
}
