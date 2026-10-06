using Microsoft.AspNetCore.Mvc;
using SistemaGerenciamentoTarefas.API.Models;
using SistemaGerenciamentoTarefas.API.Services;

namespace SistemaGerenciamentoTarefas.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosUsuarios() {

            var usuarios = await _usuarioService.BuscarTodosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id) {

            if (id <= 0) {
                return BadRequest("O id deve ser maior que zero.");
            }

            var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);

            if (usuario == null) {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {

                var usuarioCriado = await _usuarioService.CadastrarUsuarioAsync(usuario);

                return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = usuarioCriado.Id }, usuarioCriado);
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] Usuario usuarioAtualizado) {

            if (id <= 0) {
                return BadRequest("O id deve ser maior que zero.");
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var usuario = await _usuarioService.AtualizarUsuarioAsync(id, usuarioAtualizado);

                if (usuario == null) {
                    return NotFound($"Usuário com ID {id} não encontrado.");
                }

                return Ok($"Usuário '{usuario.Nome}' atualizado com sucesso!");
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletarUsuario(int id) {

            if (id <= 0) {
                return BadRequest("O id deve ser maior que zero.");
            }

            var removido = await _usuarioService.DeletarUsuarioAsync(id);

            if (!removido) {
                return NotFound($"Usuário com ID {id} não encontrado.");
            }

            return NoContent();
        }
    }
}