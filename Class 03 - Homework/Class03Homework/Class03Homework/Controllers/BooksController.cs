using Class03Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace Class03Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet] // http://localhost:[port]/api/books
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("queryString")] //http://localhost:[port]/api/books/queryString?index=1
        public ActionResult <Book> GetBookByIndex (int? index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }

                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Books[index.Value]);
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("multipleQueryParams")]
        public ActionResult<List<Book>> FilterBooks(string? author, string? title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && title == null)
                {
                    return BadRequest("Please enter at least one filter parameter!");
                }

                if (string.IsNullOrEmpty(author))
                {
                    List<Book> filteredBooks = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return Ok(filteredBooks);
                }

                if (string.IsNullOrEmpty(title))
                {
                    List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return Ok(filteredBooks);
                }

                List<Book> booksDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower()) && (string)x.Title.ToLower() == title.ToLower()).ToList();

                return Ok(booksDb);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost] 
        public IActionResult AddNewBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author) && string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Book name and book author required!");
                }

                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book added");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }
    }
}
