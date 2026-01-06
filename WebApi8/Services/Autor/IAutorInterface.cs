using WebApi8.Models;
using WebApi8.Dto.Autor;

namespace WebApi8.Services.Autor
{
    public interface IAutorInterface 
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();

        // task pois é um metodo assincrono

        Task<ResponseModel<AutorModel>> BuscarAutorPorID(int idAutor);

        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);

        Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto);

        Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
        Task<ResponseModel<List<AutorModel>>> ExcluirAutor( int idAutor);


    }
}
