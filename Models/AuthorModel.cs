using SimpleLibrary.API.Domain;
using SimpleLibrary.API.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleLibrary.API.Models
{
    public class AuthorGetModel : ModelBase<Author>
    {
        public AuthorGetModel(Author entity) : base(entity)
        {
        }

        public Guid Id => GetPropertyValue<Guid>();
        public string Genre => GetPropertyValue<string>();
        public string Name => $"{Entity.FirstName} {Entity.LastName}";
        public int Age => DateTimeOffsetExtensions.GetCurrentAge(Entity.DateOfBirth);
    }

    public class AuthorPostModel : ModelBase<Author>
    {
        public AuthorPostModel() : base()
        {
            Books = new ObservableCollection<BookPostModel>();

            RegisterCollection(Books, Entity.Books);
        }

        public Guid Id
        {
            get { return GetPropertyValue<Guid>(); }
        }

        public string FirstName
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }

        public string LastName
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }

        public DateTimeOffset DateOfBirth
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<DateTimeOffset>(); }
        }

        public string Genre
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }

        public ObservableCollection<BookPostModel> Books { set; get; }

    }

}
