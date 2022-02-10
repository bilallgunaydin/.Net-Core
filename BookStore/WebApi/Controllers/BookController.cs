using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.Commands.CreateBook;
using AutoMapper;
using FluentValidation;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query =new GetBooksQuery(_context,_mapper);
            var result= query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            
            BookDetailViewModel result;
            GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
            query.BookId=id;
            GetBookDetailValidator validator =new GetBookDetailValidator();
            validator.ValidateAndThrow(query);
            result=query.Handle();


            return Ok(result);
        
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command =new CreateBookCommand(_context,_mapper);
            command.Model=newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
           
            UpdateBookCommand command= new UpdateBookCommand(_context);
            command.BookId=id;
            command.Model=updatedBook;
            UpdateBookCommandValidator validator =new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            
            DeleteBookCommand command =new DeleteBookCommand(_context);
            command.BookId=id;
            DeleteBookCommandValidator validator =new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}