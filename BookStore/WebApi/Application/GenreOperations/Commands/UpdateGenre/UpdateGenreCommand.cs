using System;
using System.Linq;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public int GenreId {get; set;}
        public UpdateGenreModel Model {get; set;}

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null)
            throw new InvalidOperationException("Kitap Türü Bulunamadı!");

            if(_context.Genres.Any(x=> x.Name.ToLower()==Model.Name.ToLower() && x.Id!=GenreId))
            throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut!");

            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive=Model.IsActive;
            _context.SaveChanges();


        }
        
    }

    public class UpdateGenreModel
    {
        public string Name {get; set;}
        public bool IsActive {get; set;}=true;
    }
}
