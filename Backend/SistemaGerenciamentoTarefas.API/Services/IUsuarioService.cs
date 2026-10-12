using SistemaGerenciamentoTarefas.API.DTOs;
using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Services {

    public interface IUsuarioService {

        Task<List<UsuarioRespostaDto>> BuscarTodosUsuariosAsync();

        Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto);
    }
}