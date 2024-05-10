using Dapper;
using ModelLayer.DTO;
using ModelLayer.Entities;
using RepositaryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class BookServiceRL(DapperContext context) : IBookRL
    {
        public Book AddBook(Book book)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Title", book.title);
                parameters.Add("@Author", book.author);
                parameters.Add("@Price", book.price);
                parameters.Add("@Description", book.description);
                parameters.Add("@ImagePath", book.imagePath);
                parameters.Add("@Quantity", book.quantity);

                var connection = context.CreateConnection();
                var result =  connection.Execute("spAddBook", parameters, commandType: CommandType.StoredProcedure);

                if(result>0)
                {
                    return book;
                }
                else
                {
                    throw new Exception("Failed to add book.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public List<Book> GetAllBooks()
        {
            try
            {
                var connection = context.CreateConnection();
                var books =  connection.Query<Book>("spGetAllBooks", commandType: CommandType.StoredProcedure);

                if (books != null && books.Any())
                {
                    return  books.ToList();
                }
                else
                {
                    throw new Exception("No book found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public string DeleteBook(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var connection = context.CreateConnection();
                var result = connection.Execute("spDeleteBook", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    return "deleted successful";
                }
                else
                {
                    throw new Exception("No books found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public Book GetBookById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                using (var connection = context.CreateConnection())
                {
                    connection.Open();
                    var query = "SELECT * FROM Books WHERE bookId = @Id";
                    var book = connection.QueryFirstOrDefault<Book>(query, parameters);

                    if (book != null)
                    {
                        return book;
                    }
                    else
                    {
                        throw new Exception("No book found.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

    }
}
