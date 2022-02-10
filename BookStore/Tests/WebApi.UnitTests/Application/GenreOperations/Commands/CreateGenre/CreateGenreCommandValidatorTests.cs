using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.CreateGenre;
using Xunit;

namespace Tests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model= new CreateGenreModel()
            {
                Name=name
            };
            
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null,null);
            command.Model= new CreateGenreModel()
            {
                Name="Lord Of The Rings"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result= validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
        
    }
}











