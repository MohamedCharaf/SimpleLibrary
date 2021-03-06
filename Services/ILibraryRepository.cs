﻿using SimpleLibrary.API.Domain;
using SimpleLibrary.API.Helpers;
using System;
using System.Collections.Generic;

namespace SimpleLibrary.API.Services
{
    public interface ILibraryRepository
    {
        IEnumerable<Author> GetAuthors();
        IEnumerable<Author> GetAuthors(int pageNumber, int pageSize);
        PagedList<Author> Paginate(int pageNumber, int pageSize);
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        IEnumerable<Book> GetBooksForAuthor(Guid authorId);
        Book GetBookForAuthor(Guid authorId, Guid bookId);
        void AddBookForAuthor(Guid authorId, Book book);
        Book UpdateBookForAuthor(Book book);
        void DeleteBook(Book book);
        bool Save();
    }
}
