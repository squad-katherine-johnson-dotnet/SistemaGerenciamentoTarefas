using SistemaGerenciamentoTarefas.API.DTOs;
using SistemaGerenciamentoTarefas.API.Enums;
using SistemaGerenciamentoTarefas.API.Models;
using SistemaGerenciamentoTarefas.API.Repositories;

namespace SistemaGerenciamentoTarefas.API.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarefaService(
            ITarefaRepository tarefaRepository,
            IUsuarioRepository usuarioRepository)
        {
            _tarefaRepository = tarefaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<TarefaRespostaDto>> ListarAsync(int usuarioId)
        {
            await ValidarUsuarioAsync(usuarioId);

            List<Tarefa> tarefas =
                await _tarefaRepository.ListarAsync(usuarioId);

            List<TarefaRespostaDto> respostas =
                new List<TarefaRespostaDto>();

            foreach (Tarefa tarefa in tarefas)
            {
                respostas.Add(MontarResposta(tarefa));
            }

            return respostas;
        }

        public async Task<TarefaRespostaDto> CriarAsync(
            int usuarioId, TarefaCadastroDto dados)
        {
            await ValidarUsuarioAsync(usuarioId);

            Tarefa tarefa = new Tarefa
            {
                Titulo = dados.Titulo,
                Descricao = dados.Descricao,
                DataVencimento = dados.DataVencimento.GetValueOrDefault(),
                StatusTarefa = dados.StatusTarefa,
                UsuarioId = usuarioId
            };

            await _tarefaRepository.AdicionarAsync(tarefa);

            return MontarResposta(tarefa);
        }

        public async Task<TarefaRespostaDto> AtualizarAsync(
            int usuarioId, int id, TarefaCadastroDto dados)
        {
            Tarefa tarefa = await BuscarTarefaAsync(usuarioId, id);

            tarefa.Titulo = dados.Titulo;
            tarefa.Descricao = dados.Descricao;
            tarefa.DataVencimento = dados.DataVencimento.GetValueOrDefault();
            tarefa.StatusTarefa = dados.StatusTarefa;

            await _tarefaRepository.SalvarAlteracoesAsync();

            return MontarResposta(tarefa);
        }

        public async Task ExcluirAsync(int usuarioId, int id)
        {
            Tarefa tarefa = await BuscarTarefaAsync(usuarioId, id);

            await _tarefaRepository.ExcluirAsync(tarefa);
        }

        public async Task<TarefaRespostaDto> ConcluirAsync(
            int usuarioId, int id)
        {
            Tarefa tarefa = await BuscarTarefaAsync(usuarioId, id);

            tarefa.StatusTarefa = StatusTarefa.Concluida;

            await _tarefaRepository.SalvarAlteracoesAsync();

            return MontarResposta(tarefa);
        }

        private async Task ValidarUsuarioAsync(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentException(
                    "O ID da usuária deve ser maior que zero.");
            }

            Usuario? usuario =
                await _usuarioRepository.BuscarUsuarioPorIdAsync(usuarioId);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuária não encontrada.");
            }
        }

        private async Task<Tarefa> BuscarTarefaAsync(int usuarioId, int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "O ID da tarefa deve ser maior que zero.");
            }

            await ValidarUsuarioAsync(usuarioId);

            Tarefa? tarefa =
                await _tarefaRepository.BuscarPorIdAsync(usuarioId, id);

            if (tarefa == null)
            {
                throw new KeyNotFoundException(
                    "Tarefa não encontrada para esta usuária.");
            }

            return tarefa;
        }

        private TarefaRespostaDto MontarResposta(Tarefa tarefa)
        {
            return new TarefaRespostaDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataVencimento = tarefa.DataVencimento,
                StatusTarefa = tarefa.StatusTarefa,
                UsuarioId = tarefa.UsuarioId
            };
        }
    }
}