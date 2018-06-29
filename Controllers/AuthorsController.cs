using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleLibrary.API.Domain;
using SimpleLibrary.API.Helpers;
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
        private readonly IUrlHelper _urlHelper;
        private const int _maxAuthorzPageSize = 20;

        public AuthorsController(ILibraryRepository repository, IUrlHelper urlHelper)
        {
            _libraryRepository = repository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetAuthors")]
        public IActionResult Get(AuthorsResourceParameters page)
        {
            var result = _libraryRepository.Paginate(page.PageNumber, page.PageSize);
            var authors = result.Select(a => new AuthorGetModel(a));

            var previousPageLink = CreateAuthorResourcesUri(page, ResourceUriType.PreviousPage);
            var nextPageLink = CreateAuthorResourcesUri(page, ResourceUriType.NextPage);

            var metadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages,
                previous = previousPageLink,
                next = nextPageLink
            };

            Response.Headers.Add("X-Paginatoin", JsonConvert.SerializeObject(metadata));
            return Ok(authors);
        }

        private string CreateAuthorResourcesUri(AuthorsResourceParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetAuthors", new
                    {
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetAuthors", new
                    {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize
                    });
                default:
                    return _urlHelper.Link("GetAuthors", new
                    {
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize
                    });
            }
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

            _libraryRepository.AddAuthor(postModel.GetEntity());

            _libraryRepository.Save();

            return CreatedAtRoute("GetAuthor", new { id = postModel.Id }, new AuthorGetModel(postModel.GetEntity()));

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
