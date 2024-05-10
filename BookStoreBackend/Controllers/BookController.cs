using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookBL bookService) : Controller
    {
        [HttpPost]
        public ResponceStructure<Book> AddBook([FromBody] BookRequest requestDto)
        {
            try
            {
                var result =  bookService.AddBook(requestDto);

                if (result != null)
                {
                    return new ResponceStructure<Book>(true, "Book added successfully", result);
                }
                else
                {
                    return new ResponceStructure<Book>(false,"Failed to add book.");
                }
            }
            catch (Exception ex)
            {
                return new ResponceStructure<Book>(false,$"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public ResponceStructure<List<Book>> GetAllBooks()
        {
            try
            {
                var books = bookService.GetAllBooks();

                if (books != null && books.Any())
                {
                    return new ResponceStructure<List<Book>>(true,"Books retrieved successfully", books);
                }
                else
                {
                    return new ResponceStructure<List<Book>>(false,"No books found.");
                }
            }
            catch (Exception ex)
            {
                return new ResponceStructure<List<Book>>(false,$"Error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public ResponceStructure<string> DeleteBook(int id)
        {
            try
            {
                var result = bookService.DeleteBook(id);

                if (result != null)
                {
                    return new ResponceStructure<string>("Book deleted successfully.");
                }
                else
                {
                    return new ResponceStructure<string>("Failed to delete book.");
                }
            }
            catch (Exception ex)
            {
                return new ResponceStructure<string>($"Error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public ResponceStructure<Book> GetBookById(int id)
        {
            try
            {
                var result = bookService.GetBookById(id);

                if (result != null)
                {
                    return new ResponceStructure<Book>(true,"Book retrieved successfully.", result);
                }
                else
                {
                    return new ResponceStructure<Book>(false,"Book not found.", null);
                }
            }
            catch (Exception ex)
            {
                return new ResponceStructure<Book>(false,$"Error: {ex.Message}");
            }
        }


    }
}
