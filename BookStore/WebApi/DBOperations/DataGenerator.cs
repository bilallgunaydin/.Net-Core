using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {

        protected static void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {           
            
            using(var context= new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange
                (
                    new Genre{
                        Name="Personal Growth"
                    },

                     new Genre{
                        Name="Science Fiction"
                    },

                     new Genre{
                        Name="Romance"
                    }
                );
                
                context.Books.AddRange(
                new Book{
                
                Title="Martin Eden",
                GenreId=1, 
                PageCount=200,
                PublishDate= new DateTime(2001,06,12)
                 },

                new Book{
           
                    Title="Hayatın Kaynağı",
                    GenreId=2, 
                    PageCount=250,
                    PublishDate= new DateTime(2010,05,23)
                },

                new Book{
                //    Id=3,
                    Title="İnce Memed",
                    GenreId=2, 
                    PageCount=540,
                    PublishDate= new DateTime(2010,05,23)
                });

                context.Authors.AddRange(
                    new Author{
                        FirstName="Jack",
                        LastName="London",
                        BirthDate=DateTime.Parse("1900-09-01"),
                        BookId=1
                    },
                    new Author{
                        FirstName="Ayn",
                        LastName="Rand",
                        BirthDate=DateTime.Parse("1905-02-02"),
                        BookId=2
                    },

                    new Author{
                        FirstName="Yaşar",
                        LastName="Kemal",
                        BirthDate=DateTime.Parse("1923-10-06"),
                        BookId=3
                    }
                );

                context.SaveChanges();
            }
        }
    }
    
}