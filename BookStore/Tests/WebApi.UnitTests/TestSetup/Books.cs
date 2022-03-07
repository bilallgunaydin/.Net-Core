using WebApi.DBOperations;
using WebApi.Entities;
using System;

namespace Tests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
            new Book{Title="Martin Eden", GenreId=1,  PageCount=200, PublishDate= new DateTime(2001,06,12)},
            new Book{Title="Hayatın Kaynağı", GenreId=2, PageCount=250, PublishDate= new DateTime(2010,05,23)},
            new Book{Title="İnce Memed",GenreId=2, PageCount=540, PublishDate= new DateTime(2010,05,23)});
        }
    }
}



