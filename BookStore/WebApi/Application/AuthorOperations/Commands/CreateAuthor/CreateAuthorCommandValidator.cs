using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=> command.Model.FirstName).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.LastName).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command=> command.Model.BookId).GreaterThan(0);
        }

    }
}