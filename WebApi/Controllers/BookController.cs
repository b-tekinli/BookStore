using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers 
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    {
        private static List<Book> BookList = new List<Book>() 
        { 
            new Book { 
                Id = 1, 
                Title = "Lean Startup", 
                GenreId = 1, // Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001, 04, 12)
            },

            new Book {
                Id = 2,
                Title = "Herland", 
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010, 05, 23)
            },

            new Book {
                Id = 3,
                Title = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2001, 12, 21)
            }
        };

        
        [HttpGet]
        public List<Book> GetBooks() 
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
        


        [HttpGet("{id}")]
        public Book GetById(int id) 
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }


        /*
        [HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
        */


        // Post  ==>   kendi static listemize 1 tane kitap ekleyeceğiz.
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook) 
        {
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

            if(book is not null) 
            {
                return BadRequest();
            }

            BookList.Add(newBook);
            return Ok();
        }


        // Put   ==>   mevcut kitap üzerinde değişiklik, güncelleme yapacağız.
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);

            if(book is null)
            {
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        }

    }
}