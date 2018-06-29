using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SimpleLibrary.API.Helpers;
using SimpleLibrary.API.Models;
using SimpleLibrary.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrary.API.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollections : Controller
    {
        private readonly ILibraryRepository _repo;

        public AuthorCollections(ILibraryRepository repo)
        {
            _repo = repo;
        }


        [HttpPost]
        public IActionResult Post([FromBody]ICollection<AuthorPostModel> authors)
        {
            if (authors == null)
                return BadRequest();

            var entities = authors.Select(a => a.GetEntity()).ToList();

            foreach (var entity in entities)
            {
                _repo.AddAuthor(entity);
            }

            if (!_repo.Save())
            {
                throw new Exception("Creating author collection failed");
            }

            var ids = string.Join(",", entities.Select(e => e.Id));

            return CreatedAtRoute("AuthorCollections.GetAll",
                new { ids },
                entities.Select(e => new AuthorGetModel(e)));

        }

        [HttpGet("({ids})", Name = "AuthorCollections.GetAll")]
        public IActionResult Get([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var entities = _repo.GetAuthors(ids);

            if (entities == null || entities.Count() != ids.Count())
                return NotFound();

            return Ok(entities.Select(e => new AuthorGetModel(e)));
        }
    }
}
