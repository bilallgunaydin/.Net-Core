using Xunit;
using Tests.TestSetup;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;


namespace Tests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
      

        [Theory]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings",0,1)]
        [InlineData("Lord Of The Rings",1,0)]
        [InlineData("",0,0)]
        [InlineData("",0,1)]
        [InlineData("",1,0)]
        [InlineData("",1,1)]
        [InlineData("Lor",1,1)]
        [InlineData("Lor",0,1)]
        [InlineData("Lor",1,0)]
        [InlineData(" ",1,1)]
        [InlineData(" ",0,1)]
        [InlineData(" ",1,0)]
        [InlineData(" ",0,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int id)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            UpdateBookModel model = new UpdateBookModel
            {
                Title = title,
                GenreId = genreId
            };
            command.BookId=id;
            command.Model = model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Martin Eden",3)]
        [InlineData("Hayatın Kaynağı",2)]

        public void WhenGivenTheSameValues_Validator_ShouldBeReturn(string title, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            UpdateBookModel model = new UpdateBookModel
            {
                Title = title,
                GenreId = genreId
            };
            command.BookId = 3;
            command.Model=model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        

        [Fact]
        public void WhenValidInputsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            UpdateBookModel model = new UpdateBookModel
            {
                Title= "Lord Of The Rings", 
                GenreId=1,
            };
            command.BookId=1;
            command.Model = model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}