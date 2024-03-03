using electronic_library_6.Domain.Entities;
using electronic_library_6.Domain.Services;
using vote_counting_system.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace electronic_library_6.Infrastructure
{
    public class BooksReader :IBooksReader
    {
        private readonly IRepository<Book> books;
        private readonly IRepository<Categories> categories;
        private readonly IRepository<PollingStations> pollingstations;


        public BooksReader(IRepository<Book> books, IRepository<Categories> categories, IRepository<PollingStations> pollingstations)
        {
            this.books = books;
            this.categories = categories;
            this.pollingstations = pollingstations;
        }
        public async Task<Book?> FindBookAsync (int bookId) =>
            await books.FindAsync(bookId);

        public async Task<List<Book>> FindBooksAsync (string searchString, int categoryId,int pollingstationsId) => (searchString, categoryId, pollingstationsId) switch
        {
            ("" or null, 0,0) => await books.GetAllAsync(),
            (_, _, _) => await books.FindWhere(book => book.CategoryId == categoryId),

             /*(_, 0) => await books.FindWhere(book => book.Title.Contains(searchString) || book.Author.Contains(searchString)),
            (_, _) => await books.FindWhere(book => book.CategoryId == categoryId &&
                (book.Title.Contains(searchString) || book.Author.Contains(searchString))),*/
        };

        public async Task<List<Book>> GetAllBooksAsync () => await books.GetAllAsync();

        public async Task<List<Categories>> GetCategoriesAsync () => await categories.GetAllAsync();

        public async Task<List<PollingStations>> GetPollingStationsAsync() => await pollingstations.GetAllAsync();

    }
}
