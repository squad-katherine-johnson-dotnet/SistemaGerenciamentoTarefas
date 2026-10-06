using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Services {
    public interface IUsuarioService {
        Task<Usuario> AdicionarUsuarioAsync(Usuario usuario);
    }
}