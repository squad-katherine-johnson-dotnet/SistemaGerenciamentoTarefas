using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Repositories
{
    public interface ITarefaRepository
    {
        Task<List<Tarefa>> ListarAsync(int usuarioId);

        Task<Tarefa?> BuscarPorIdAsync(int usuarioId, int id);

        Task AdicionarAsync(Tarefa tarefa);

        Task SalvarAlteracoesAsync();

        Task ExcluirAsync(Tarefa tarefa);
    }
}