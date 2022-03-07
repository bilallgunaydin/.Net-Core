using System;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("***","***",0)]
        [InlineData("***","****",0)]
        [InlineData("****","****",0)]
        [InlineData("***","***",0)]
        [InlineData("**","***",0)]
        [InlineData("**","**",0)]
        [InlineData("*","**",0)]
        [InlineData("*","*",0)]
        [InlineData("","*",0)]
        [InlineData("","",0)]
        [InlineData("*","",0)]
        [InlineData("**","",0)]
        [InlineData("***","",0)]
        [InlineData("****","",0)]
        [InlineData("*","",0)]
        [InlineData("***","***",1)]
        [InlineData("***","****",1)]
        [InlineData("****","****",0)]
        [InlineData("***","***",1)]
        [InlineData("**","***",1)]
        [InlineData("**","**",1)]
        [InlineData("*","**",1)]
        [InlineData("*","*",1)]
        [InlineData("","*",1)]
        [InlineData("","",1)]
        [InlineData("*","",1)]
        [InlineData("**","",1)]
        [InlineData("***","",1)]
        [InlineData("****","",1)]
        [InlineData("*","",1)]
        [InlineData(" "," ",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName, int bookId)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
                command.Model = new UpdateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = DateTime.Now.AddYears(-1),
                BookId = bookId
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                FirstName="Ahmet",
                LastName="Şükrü",
                BirthDate=DateTime.Now.Date,
                BookId=1
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                FirstName="Ahmet",
                LastName="Şükrü",
                BirthDate=DateTime.Now.AddYears(-10),
                BookId=1
            };
            command.AuthorId=1;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}