using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.CreateGenre

{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

         public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre(){
                Name="Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model=new CreateGenreModel(){Name=genre.Name};

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut.");
        }

        [Fact]
        public void WhenValidİnputisGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            CreateGenreModel model = new CreateGenreModel()
            {Name="Bilim Kurgu"};
            command.Model=model;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genre =_context.Genres.SingleOrDefault(genre=> genre.Name==model.Name);
            genre.Should().NotBeNull();
        }
    }
    
}