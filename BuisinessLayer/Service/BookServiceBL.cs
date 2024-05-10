using BusinessLayer.Interface;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class BookServiceBL(IBookRL bookRepo) : IBookBL
    {
        private Book MapToEntity(BookRequest request)
        {
            return new Book
            {
                title = request.title,
                author = request.author,
                price = request.price,
                description = request.description,
                imagePath = request.imagePath,
                quantity = request.quantity
            };
        }
        public Book AddBook(BookRequest requestDto)
        {
            var addedBook =bookRepo.AddBook(MapToEntity(requestDto));
            return addedBook;
        }

        public List<Book> GetAllBooks()
        {
            return  bookRepo.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return bookRepo.GetBookById(id);
        }

        public string DeleteBook(int id)
        {
            return bookRepo.DeleteBook(id);
        }
    }
}
