using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Repositories {
    public interface IUsuarioRepository {

        Task<List<Usuario>> BuscarTodosUsuariosAsync();
        Task<Usuario?> BuscarUsuarioPorIdAsync(int id);
        Task<Usuario> CadastrarUsuarioAsync(Usuario usuario);
        Task<Usuario?> AtualizarUsuarioAsync(int id, Usuario usuarioAtualizado);
        Task<bool> DeletarUsuarioAsync(int id);
        Task<bool> EmailExisteAsync(string email, int? usuarioId = null);
    }
}
