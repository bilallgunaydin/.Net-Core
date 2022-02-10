using System;
using System.Linq;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.UpdateDete
{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenANonExistentGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı!");
        }


        [Fact]

        public void WhenGivenTheSameValues_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            var genre = _context.Genres.SingleOrDefault(genre=> genre.Id==3);
            command.GenreId=genre.Id;
            UpdateGenreModel Model=new UpdateGenreModel
            {
                Name="Romance",
            };
            command.Model=Model;
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap türü zaten mevcut!");

            genre.Should().NotBeNull();
            genre.Name.Should().Be(Model.Name);

        }

        [Fact]
        public void WhenAnExistingGenreIsGiven_ShouldBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            var genre = _context.Genres.SingleOrDefault(genre=> genre.Id==3);
            command.GenreId=genre.Id;
            UpdateGenreModel Model=new UpdateGenreModel
            {
                Name="Test",
                IsActive=false
            };
            command.Model=Model;
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            genre.Should().NotBeNull();
            genre.Name.Should().Be(Model.Name);
            genre.IsActive.Should().Be(Model.IsActive);
        }
    }
}
         