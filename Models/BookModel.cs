using SimpleLibrary.API.Domain;
using System;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MaxLength(100)]
        public string Title
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }

        [Required]
        [MaxLength(100)]
        public string Description
        {
            set { SetPropertyValue(value); }
            get { return GetPropertyValue<string>(); }
        }
    }

    public class BookPutModel : ModelBase<Book>
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }

        public string Title
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

        public string Description
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

    }

    public class BookPatchModel : ModelBase<Book>
    {
        public BookPatchModel(Book entity) : base(entity)
        {
        }

        public string Title
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

        public string Description
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }
    }
}
