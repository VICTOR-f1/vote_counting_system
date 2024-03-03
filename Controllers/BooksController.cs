using electronic_library_6.Domain.Entities;
using electronic_library_6.Domain.Services;
using electronic_library_6.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace electronic_library_6.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly IBooksReader reader;
        [Authorize]
        public async Task<IActionResult> Index(string searchString = "", int categoryId = 0, int pollingstationsId = 0)
        {
            var viewModel = new BooksCatalogViewModel
            {
                Books = await reader.FindBooksAsync(searchString, categoryId, pollingstationsId),
                Categories = await reader.GetCategoriesAsync(),
                PollingStations = await reader.GetPollingStationsAsync()
            };

            return View(viewModel);
        }

        public BooksController(IBooksReader reader, IBooksService booksService, IWebHostEnvironment appEnvironment)
        {
            this.reader = reader;
            this.booksService = booksService;
            this.appEnvironment = appEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "OlderAdmin")]
        public async Task<IActionResult> AddBook()
        {
            var viewModel = new BookViewModel();

            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();
            var pollingstations = await reader.GetPollingStationsAsync();

            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            var items2 = pollingstations.Select(c =>
               new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            // добавляем список в модель представления
            viewModel.Categories.AddRange(items);
            viewModel.PollingStations.AddRange(items2);
            return View(viewModel);
        }
      

        [HttpPost]
        [Authorize(Roles = "OlderAdmin")]
        public async Task<IActionResult> AddBook(BookViewModel bookVm)
        {
            if (!ModelState.IsValid)
            {
                return View(bookVm);
            }
            try
            {
                var book = new Book
                {
                    CategoryId = bookVm.CategoryId,
                    TotalNumberPeopleVoted = bookVm.TotalNumberPeopleVoted,
                    BallotsAllocatedSite = bookVm.BallotsAllocatedSite,
                    BallotsMissingIncorrect = bookVm.BallotsMissingIncorrect,
                    PollingStationsId = bookVm.PollingStationsId

                };
                string wwwroot = appEnvironment.WebRootPath; // получаем путь до wwwroot
             
                await booksService.AddBook(book);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(bookVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(bookVm);
            }
            return RedirectToAction("Index", "Books");
        }
      
      
        [HttpGet]
        [Authorize(Roles = "OlderAdmin")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var book = await reader.FindBookAsync(bookId);
            if (book is null)
            {
                return NotFound();
            }
            var bookVm = new DeleteBookViewModel
            {
                Id = book.Id,
                CategoryId = book.CategoryId,
                TotalNumberPeopleVoted = book.TotalNumberPeopleVoted,
                BallotsAllocatedSite = book.BallotsAllocatedSite,
                BallotsMissingIncorrect = book.BallotsMissingIncorrect,
                PollingStationsId = book.PollingStationsId,
            };

            var categories = await reader.GetCategoriesAsync();
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            bookVm.Categories.AddRange(items);

            return View(bookVm);
        }
        [HttpPost]
        [Authorize(Roles = "OlderAdmin")]
        public async Task<IActionResult> DeleteBook(DeleteBookViewModel bookVm)
        {
            if (!ModelState.IsValid)
            {
                return View(bookVm);
            }

            var book = await reader.FindBookAsync(bookVm.Id);

            if (book is null)
            {
                ModelState.AddModelError("not_found", "Книга не найдена!");
                return View(bookVm);
            }
            try
            {
                string wwwroot = appEnvironment.WebRootPath;
                booksService.DeleteBook(book);
             

            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось удалить файл.");
                return View(bookVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при удалении из базы данных.");
                return View(bookVm);
            }
            return RedirectToAction("Index", "Books");
        }

    }
}
