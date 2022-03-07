using AutoMapper;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.GenreOperations.CreateGenre;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, 
            opt=>opt.MapFrom(src=> src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, 
            opt=>opt.MapFrom(src=> src.Genre.Name));
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<Author,AuthorsViewModel>().ForMember(dest=> dest.Book, opt=> 
            opt.MapFrom(src=> src.Book.Title));
            CreateMap<Author,AuthorDetailViewModel>().ForMember(dest=> dest.Book, opt=> 
            opt.MapFrom(src=> src.Book.Title));
            CreateMap<CreateAuthorModel,Author>();
            CreateMap<UpdateAuthorModel,Author>();
            CreateMap<CreateUserModel,User>();
        }
    }
}