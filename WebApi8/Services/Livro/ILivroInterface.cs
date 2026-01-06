using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();

        // task pois é um metodo assincrono

        Task<ResponseModel<LivroModel>> BuscarLivroPorID(int idLivro);

        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);

        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);

        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);

    }
}
