using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        Book AddBook(Book book);
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        string DeleteBook(int id);
    }
}
