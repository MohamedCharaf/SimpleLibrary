using SimpleLibrary.API.Domain;
using System;

namespace SimpleLibrary.API.Models
{
    public class BookGetModel : ModelBase<Book>
    {
        public BookGetModel(Book entity) : base(entity)
        {
        }

        public Guid Id
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<Guid>(); }
        }

        public string Title
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }


        public string Description
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }

        public Guid AuthorId
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<Guid>(); }
        }
    }

    public class BookPostModel : ModelBase<Book>
    {
        public BookPostModel()
        {
        }

        public BookPostModel(Book entity)
        {
            Title = entity.Title;
            Description = entity.Description;
        }

        public Guid Id
        {
            get { return GetPropertyValue<Guid>(); }
        }

        public string Title
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }

        public string Description
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }
    }
}
