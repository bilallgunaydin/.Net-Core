using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public GetBookDetailValidatorTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context,null);
            command.BookId = id;

            GetBookDetailValidator validator = new GetBookDetailValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeNotReturnErrors()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context,null);
            command.BookId = 1;

            GetBookDetailValidator validator = new GetBookDetailValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
    }
}