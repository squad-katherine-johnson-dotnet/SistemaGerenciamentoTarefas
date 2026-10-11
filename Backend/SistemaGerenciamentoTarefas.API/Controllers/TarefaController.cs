using Microsoft.AspNetCore.Mvc;
using SistemaGerenciamentoTarefas.API.DTOs;
using SistemaGerenciamentoTarefas.API.Services;

namespace SistemaGerenciamentoTarefas.API.Controllers
{
    [ApiController]
    [Route("api/usuarios/{usuarioId:int}/tarefas")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RespostaPadraoDto), 200)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 400)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 404)]
        public async Task<IActionResult> Listar(int usuarioId)
        {
            List<TarefaRespostaDto> tarefas =
                await _tarefaService.ListarAsync(usuarioId);

            return Ok(new RespostaPadraoDto
            {
                Sucesso = true,
                Mensagem = "Tarefas encontradas.",
                Dados = tarefas
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(RespostaPadraoDto), 201)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 400)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 404)]
        public async Task<IActionResult> Criar(
            int usuarioId, [FromBody] TarefaCadastroDto dados)
        {
            TarefaRespostaDto tarefa =
                await _tarefaService.CriarAsync(usuarioId, dados);

            return StatusCode(201, new RespostaPadraoDto
            {
                Sucesso = true,
                Mensagem = "Tarefa cadastrada com sucesso.",
                Dados = tarefa
            });
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(RespostaPadraoDto), 200)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 400)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 404)]
        public async Task<IActionResult> Atualizar(
            int usuarioId, int id, [FromBody] TarefaCadastroDto dados)
        {
            TarefaRespostaDto tarefa =
                await _tarefaService.AtualizarAsync(usuarioId, id, dados);

            return Ok(new RespostaPadraoDto
            {
                Sucesso = true,
                Mensagem = "Tarefa atualizada com sucesso.",
                Dados = tarefa
            });
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(RespostaPadraoDto), 200)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 400)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 404)]
        public async Task<IActionResult> Excluir(int usuarioId, int id)
        {
            await _tarefaService.ExcluirAsync(usuarioId, id);

            return Ok(new RespostaPadraoDto
            {
                Sucesso = true,
                Mensagem = "Tarefa excluída com sucesso."
            });
        }

        [HttpPatch("{id:int}/concluir")]
        [ProducesResponseType(typeof(RespostaPadraoDto), 200)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 400)]
        [ProducesResponseType(typeof(RespostaPadraoDto), 404)]
        public async Task<IActionResult> Concluir(int usuarioId, int id)
        {
            TarefaRespostaDto tarefa =
                await _tarefaService.ConcluirAsync(usuarioId, id);

            return Ok(new RespostaPadraoDto
            {
                Sucesso = true,
                Mensagem = "Tarefa concluída com sucesso.",
                Dados = tarefa
            });
        }
    }
}