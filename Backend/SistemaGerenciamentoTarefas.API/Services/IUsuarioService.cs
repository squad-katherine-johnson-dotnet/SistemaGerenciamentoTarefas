using SistemaGerenciamentoTarefas.API.DTOs;
using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Services {
    public interface IUsuarioService {
        Task<List<UsuarioRespostaDto>> BuscarTodosUsuariosAsync();
        Task<UsuarioRespostaDto?> BuscarUsuarioPorIdAsync(int id);
        Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto);
        Task<UsuarioRespostaDto?> AtualizarUsuarioAsync(int id, UsuarioAtualizacaoDto usuarioAtualizado);
        Task<bool> DeletarUsuarioAsync(int id);
    }
}