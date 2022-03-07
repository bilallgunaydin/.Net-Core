using System;
using System.Linq;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenANonExistentGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeDeleted()
        {
            var genre = _context.Genres.SingleOrDefault(genre=> genre.Id==3);
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genre.Id;

            FluentActions.Invoking(()=> command.Handle()).Invoke();
            var deletedGenre = _context.Genres.SingleOrDefault(deletedGenre=> deletedGenre.Id==genre.Id);
            deletedGenre.Should().BeNull();

        }
    }
}