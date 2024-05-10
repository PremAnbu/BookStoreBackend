using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        Book AddBook(BookRequest requestDto);
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        string DeleteBook(int id);
    }
}
