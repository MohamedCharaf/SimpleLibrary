using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.API.Models;
using SimpleLibrary.API.Services;
using System;
using System.Linq;

namespace SimpleLibrary.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class AuthorBooksController : Controller
    {
        private readonly ILibraryRepository _rpeo;

        public AuthorBooksController(ILibraryRepository repo)
        {
            _rpeo = repo;
        }

        public IActionResult Get(Guid authorId)
        {
            var books = _rpeo.GetBooksForAuthor(authorId);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books.Select(b => new BookGetModel(b)));
        }

        [HttpGet("{id}", Name = "GetAuthorBook")]
        public IActionResult Get(Guid authorId, Guid id)
        {
            var book = _rpeo.GetBookForAuthor(authorId, id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(new BookGetModel(book));
        }

        [HttpPost]
        public IActionResult Post(Guid authorId, [FromBody] BookPostModel postModel)
        {
            if (postModel == null)
                return BadRequest();

            if (!_rpeo.AuthorExists(authorId))
                return NotFound();

            _rpeo.AddBookForAuthor(authorId, postModel.Entity);

            _rpeo.Save();

            return CreatedAtRoute("GetAuthorBook",
                new { authorId, id = postModel.Id },
                new BookGetModel(postModel.Entity)
                );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid authorId, Guid id)
        {
            if (!_rpeo.AuthorExists(authorId))
                return NotFound();

            var book = _rpeo.GetBookForAuthor(authorId, id);

            if (book == null)
                return NotFound();

            _rpeo.DeleteBook(book);

            if (!_rpeo.Save())
            {
                throw new Exception($"Failed Deleting Book AuthorId: {authorId}, BookId: {id} ");
            }

            return NoContent();
        }
    }
}
