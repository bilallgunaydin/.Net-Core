using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=> command.AuthorId).GreaterThan(0);
            RuleFor(command=> command.Model.FirstName).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.LastName).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.BookId).GreaterThan(0);

        }
    }
}