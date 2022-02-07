using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebApi.Entities
{
    public class Author
    {
        public Author()
        {

        }
        private readonly ILazyLoader _lazyLoader;
        public Author(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int? BookId { get; set; }
        private Book _book;
        public virtual Book Book 
        {
            get => _lazyLoader.Load(this, ref _book);
            set => _book = value;
        }
        
    }
}