using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Repositories {
    public interface IUsuarioRepository {

        Task<bool> EmailExisteAsync(string email);
        Task<Usuario> AdicionarUsuarioAsync(Usuario usuario);
    }
}
