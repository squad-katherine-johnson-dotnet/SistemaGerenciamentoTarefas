using SistemaGerenciamentoTarefas.API.DTOs;

namespace SistemaGerenciamentoTarefas.API.Services
{
    public interface ITarefaService
    {
        Task<List<TarefaRespostaDto>> ListarAsync(int usuarioId);

        Task<TarefaRespostaDto> CriarAsync(
            int usuarioId, TarefaCadastroDto dados);

        Task<TarefaRespostaDto> AtualizarAsync(
            int usuarioId, int id, TarefaCadastroDto dados);

        Task ExcluirAsync(int usuarioId, int id);

        Task<TarefaRespostaDto> ConcluirAsync(int usuarioId, int id);
    }
}