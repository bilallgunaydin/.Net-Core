using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.UpdateGenre;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.UpdateDete
{
    public class UpdateGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" ",1)]
        [InlineData("a",0)]
        [InlineData("aa",1)]
        [InlineData("aaa",1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name,int genreid)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model= new UpdateGenreModel()
            {
                Name=name,
            };
            command.GenreId=genreid;
            
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Theory]
        [InlineData("Personal Growth",1)]
        [InlineData("Science Fiction",2)]

        public void WhenGivenTheSameValues_Validator_ShouldBeReturn(string name, int genreId)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model= new UpdateGenreModel()
            {
                Name=name,
            };
            command.GenreId=genreId;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        } 

        [Fact]
        public void WhenValidInputsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model= new UpdateGenreModel()
            {
                Name="Bilim Kurgu",
            };
            command.GenreId=1;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}