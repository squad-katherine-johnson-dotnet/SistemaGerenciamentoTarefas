using Microsoft.EntityFrameworkCore;
using SistemaGerenciamentoTarefas.API.Data;
using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tarefa>> ListarAsync(int usuarioId)
        {
            return await _context.Tarefas
                .Where(tarefa => tarefa.UsuarioId == usuarioId)
                .OrderBy(tarefa => tarefa.DataVencimento)
                .ToListAsync();
        }

        public async Task<Tarefa?> BuscarPorIdAsync(int usuarioId, int id)
        {
            return await _context.Tarefas.FirstOrDefaultAsync(
                tarefa => tarefa.Id == id &&
                          tarefa.UsuarioId == usuarioId);
        }

        public async Task AdicionarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}