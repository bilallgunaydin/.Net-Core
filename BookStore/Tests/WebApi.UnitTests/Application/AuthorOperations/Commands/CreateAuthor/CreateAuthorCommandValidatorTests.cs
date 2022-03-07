using System;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.CreateAuthor
{
 public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
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
       CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            FirstName = firstName,
            LastName = lastName,
            BirthDate = DateTime.Now.AddYears(-1),
            BookId = bookId
        };

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    
    }
    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null,null);
        command.Model = new CreateAuthorModel()
        {
            FirstName="Ahmet",
            LastName="Şükrü",
            BirthDate=DateTime.Now.Date,
            BookId=1
        };

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsGiven_Validator_ShouldNotBeReturnError()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null,null);
        command.Model= new CreateAuthorModel()
        {
            FirstName="Ahmet",
            LastName="Şükrü",
            BirthDate=DateTime.Now.AddYears(-10),
            BookId=1
        };
     CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);
     result.Errors.Count.Should().Be(0);
    }
  }
}