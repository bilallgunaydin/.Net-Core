using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace Tests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}

        public CommonTestFixture()
        {
            var options= new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDB")
                .Options;
            
            Context= new BookStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfq=> { cfq.AddProfile<MappingProfile>(); }).CreateMapper();

        }
    }
}