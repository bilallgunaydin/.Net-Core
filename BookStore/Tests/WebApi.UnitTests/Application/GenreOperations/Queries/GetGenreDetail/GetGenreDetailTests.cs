using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public GetGenreDetailTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenANonExistentGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenValidİnputsAreGiven_Genre_ShouldBeListed()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreId=3;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var book =_context.Genres.SingleOrDefault(genre=> genre.Id==command.GenreId);
            book.Should().NotBeNull();
        } 
    }
}