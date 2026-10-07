using Microsoft.AspNetCore.Mvc;
using SistemaGerenciamentoTarefas.API.Models;
using SistemaGerenciamentoTarefas.API.Services;
using SistemaGerenciamentoTarefas.API.DTOs;

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

            var resposta = new RespostaPadraoDto {
                Sucesso = true,
                Mensagem = "Usuários encontrados.",
                Dados = usuarios
            };

            return Ok(resposta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id) {

            if (id <= 0) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = "O id deve ser maior que zero."
                };

                return BadRequest(resposta);
            }

            var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);

            if (usuario == null) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = $"Usuário com ID {id} não encontrado."
                };

                return NotFound(resposta);
            }

            var respostaSucesso = new RespostaPadraoDto {
                Sucesso = true,
                Mensagem = "Usuário encontrado.",
                Dados = usuario
            };

            return Ok(respostaSucesso);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDto usuarioDto) {

            try {

                var usuarioCriado = await _usuarioService.CadastrarUsuarioAsync(usuarioDto);

                var resposta = new UsuarioRespostaDto {
                    Id = usuarioCriado.Id,
                    Nome = usuarioCriado.Nome,
                    Email = usuarioCriado.Email
                };

                var respostaPadrao = new RespostaPadraoDto {
                    Sucesso = true,
                    Mensagem = "Usuário cadastrado com sucesso.",
                    Dados = resposta
                };

                return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = usuarioCriado.Id }, respostaPadrao);
            }
            catch (ArgumentException ex) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = ex.Message
                };

                return Conflict(resposta);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] UsuarioAtualizacaoDto usuarioAtualizado) {

            if (id <= 0) {
                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = "O id deve ser maior que zero."
                };

                return BadRequest(resposta);
            }

            try {
                var usuario = await _usuarioService.AtualizarUsuarioAsync(id, usuarioAtualizado);

                if (usuario == null) {
                    var resposta = new RespostaPadraoDto {
                        Sucesso = false,
                        Mensagem = $"Usuário com ID {id} não encontrado."
                    };

                    return NotFound(resposta);
                }

                var respostaSucesso = new RespostaPadraoDto {
                    Sucesso = true,
                    Mensagem = "Usuário atualizado com sucesso.",
                    Dados = usuario
                };

                return Ok(respostaSucesso);
            }
            catch (ArgumentException ex) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = ex.Message
                };

                return Conflict(resposta);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletarUsuario(int id) {

            if (id <= 0) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = "O id deve ser maior que zero."
                };

                return BadRequest(resposta);
            }

            var removido = await _usuarioService.DeletarUsuarioAsync(id);

            if (!removido) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = $"Usuário com ID {id} não encontrado."
                };

                return NotFound(resposta);
            }

            var respostaSucesso = new RespostaPadraoDto {
                Sucesso = true,
                Mensagem = "Usuário excluído com sucesso."
            };

            return Ok(respostaSucesso);
        }
    }
}