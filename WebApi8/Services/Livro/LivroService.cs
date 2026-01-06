using Microsoft.EntityFrameworkCore;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<LivroModel>> BuscarLivroPorID(int idLivro)
        {
            ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();

            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);
                //itera pelas linhas do banco ate achar

                if (livro == null)
                {
                    response.Mensagem = "Nenhum livro foi localizado";
                    
                    return response;
                }

                response.Dados = livro;
                response.Mensagem = "livro Localizado com sucesso";

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .Where(livroBanco => livroBanco.Autor.Id == idAutor)
                    .ToListAsync();
                //Incluse entra dentro das proriedades so autor que esta dentro das propriedades de livro

                if (livro == null)
                {
                    response.Mensagem = "Nenhum registro localizado!";
                    return response;
                }
                response.Dados = livro;
                response.Mensagem = "Livros Localizados";
                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;

            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            try
            {
               var autor =  await _context.Autores.
                    FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

                if(autor == null)
                {
                    response.Mensagem = "Autor não encontrado para associar ao livro.";
                    return response;
                }

                var livro =  new LivroModel
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Livros.Include(a=>a.Autor).ToListAsync();

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros.Include(a=> a.Autor).FirstOrDefaultAsync(livroBanco=> livroBanco.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);


                if (autor == null)
                {
                    response.Mensagem = "Autor não encontrado.";
                    return response;
                }

                if (livro == null)
                {
                    response.Mensagem = "Livro não encontrado.";
                    return response;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);

                await _context.SaveChangesAsync();

                response.Dados = await _context.Livros.ToListAsync();

                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    response.Mensagem = "Nenhum livro foi localizado";

                    return response;
                }

                _context.Remove(livro);

                await _context.SaveChangesAsync();

                response.Dados = await _context.Livros.ToListAsync();
                response.Mensagem = "Livro excluído com sucesso.";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            try
            {
                var livros = await _context.Livros.Include(a=>a.Autor).ToListAsync();

                response.Dados = livros;
                response.Mensagem = "Lista de livros retornada com sucesso.";

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;

            }
        }
    }
}
