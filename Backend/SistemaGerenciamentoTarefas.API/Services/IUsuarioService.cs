using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Services {
    public interface IUsuarioService {
        Task<List<Usuario>> BuscarTodosUsuariosAsync();
        Task<Usuario?> BuscarUsuarioPorIdAsync(int id);
        Task<Usuario> CadastrarUsuarioAsync(Usuario usuario);
        Task<Usuario?> AtualizarUsuarioAsync(int id, Usuario usuarioAtualizado);
        Task<bool> DeletarUsuarioAsync(int id);
    }
}