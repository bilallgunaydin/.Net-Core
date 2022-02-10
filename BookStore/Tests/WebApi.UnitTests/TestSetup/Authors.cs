using WebApi.DBOperations;
using WebApi.Entities;
using System;

namespace Tests.TestSetup
{
   
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                    new Author{
                        FirstName="Jack",
                        LastName="London",
                        BirthDate=DateTime.Parse("1900-09-01"),
                        BookId=1,
                    },
                    new Author{
                        FirstName="Ayn",
                        LastName="Rand",
                        BirthDate=DateTime.Parse("1905-02-02"),
                        BookId=2,
                    },

                    new Author{
                        FirstName="Yaşar",
                        LastName="Kemal",
                        BirthDate=DateTime.Parse("1923-10-06"),
                        BookId=3,
                    }
                );
        }
    }
}