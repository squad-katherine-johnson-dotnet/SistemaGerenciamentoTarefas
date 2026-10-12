using Microsoft.AspNetCore.Mvc;
using SistemaGerenciamentoTarefas.API.Services;
using SistemaGerenciamentoTarefas.API.DTOs;

namespace SistemaGerenciamentoTarefas.API.Controllers
{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosUsuarios()
        {

            var usuarios = await _usuarioService.BuscarTodosUsuariosAsync();

            var resposta = new RespostaPadraoDto
            {
                Sucesso = true,
                Mensagem = "Usuários encontrados.",
                Dados = usuarios
            };

            return Ok(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDto usuarioDto)
        {

            try
            {

                var usuarioCriado = await _usuarioService.CadastrarUsuarioAsync(usuarioDto);

                var resposta = new UsuarioRespostaDto
                {
                    Id = usuarioCriado.Id,
                    Nome = usuarioCriado.Nome,
                    Email = usuarioCriado.Email
                };

                var respostaPadrao = new RespostaPadraoDto
                {
                    Sucesso = true,
                    Mensagem = "Usuário cadastrado com sucesso.",
                    Dados = resposta
                };

                return StatusCode(201, respostaPadrao);
            }
            catch (ArgumentException ex) {

                if (ex.Message == "E-mail já cadastrado.") {

                    var resposta = new RespostaPadraoDto {
                        Sucesso = false,
                        Mensagem = ex.Message
                    };

                    return Conflict(resposta);
                }
                throw;
            }

        }
    }
}