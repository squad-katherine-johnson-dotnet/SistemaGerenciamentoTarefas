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
        public async Task<IActionResult> BuscarTodosAssync() {

            var produtos = await _usuarioService.BuscarTodosAsync();
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuarioAsync([FromBody] Usuario usuario) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {

                var usuarioCriado = await _usuarioService.AdicionarUsuarioAsync(usuario);

                return StatusCode(201, usuarioCriado);
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}