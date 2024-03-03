using electronic_library_6.Domain.Entities;

namespace electronic_library_6.Domain.Services
{
    public interface IBooksReader
    {
        Task<List<Book>> GetAllBooksAsync ();
        Task<List<Book>> FindBooksAsync (string searchString, int categoryId, int pollingstationsId);
        Task<Book?> FindBookAsync (int bookId);
        Task<List<Categories>> GetCategoriesAsync ();
        Task<List<PollingStations>> GetPollingStationsAsync();

    }
}
