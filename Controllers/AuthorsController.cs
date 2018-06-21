using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.API.Domain;
using SimpleLibrary.API.Models;
using SimpleLibrary.API.Services;
using System;
using System.Linq;

namespace SimpleLibrary.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository repository)
        {
            _libraryRepository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var authors = _libraryRepository.GetAuthors().Select(a => new AuthorGetModel(a));
            return new JsonResult(authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(Guid id)
        {
            var author = _libraryRepository.GetAuthor(id);

            if (author == null)
                return NotFound();

            return Ok(new AuthorGetModel(author));
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthorPostModel postModel)
        {
            if (postModel == null)
                return BadRequest();

            _libraryRepository.AddAuthor(postModel.Entity);

            _libraryRepository.Save();

            return CreatedAtRoute("GetAuthor", new { id = postModel.Id }, new AuthorGetModel(postModel.Entity));

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var author = _libraryRepository.GetAuthor(id);

            if (author == null)
                return NotFound();

            _libraryRepository.DeleteAuthor(author);

            if (!_libraryRepository.Save())
                throw new Exception($"Failed Deleting Author Id:{id}");

            return NoContent();
        }
    }
}
