using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public GetAuthorDetailValidatorTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context,null);
            command.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeNotReturnErrors()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context,null);
            command.AuthorId = 1;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        
    }
}