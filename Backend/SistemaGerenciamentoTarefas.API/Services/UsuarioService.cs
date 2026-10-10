using SistemaGerenciamentoTarefas.API.DTOs;
using SistemaGerenciamentoTarefas.API.Models;
using SistemaGerenciamentoTarefas.API.Repositories;
using Microsoft.AspNetCore.Identity;

namespace SistemaGerenciamentoTarefas.API.Services {
    public class UsuarioService : IUsuarioService {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(IUsuarioRepository usuarioRepository) {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<List<UsuarioRespostaDto>> BuscarTodosUsuariosAsync() {

            var usuarios = await _usuarioRepository.BuscarTodosUsuariosAsync();

            return usuarios.Select(usuario => new UsuarioRespostaDto {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            }).ToList();
        }

        public async Task<UsuarioRespostaDto?> BuscarUsuarioPorIdAsync(int id) {

            var usuario = await _usuarioRepository.BuscarUsuarioPorIdAsync(id);

            if (usuario == null) {
                return null;
            }

            return new UsuarioRespostaDto {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto) {

            if (await _usuarioRepository.EmailExisteAsync(usuarioDto.Email)) {
                throw new ArgumentException("E-mail já cadastrado.");
            }

            var usuario = new Usuario {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
            };

            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, usuarioDto.Senha);

            return await _usuarioRepository.CadastrarUsuarioAsync(usuario);
        }

        public async Task<UsuarioRespostaDto?> AtualizarUsuarioAsync(int id, UsuarioAtualizacaoDto usuarioAtualizado) {

            if (await _usuarioRepository.EmailExisteAsync(usuarioAtualizado.Email, id)) {
                throw new ArgumentException("E-mail já cadastrado.");
            }

            var usuario = new Usuario {
                Id = id,
                Nome = usuarioAtualizado.Nome,
                Email = usuarioAtualizado.Email
            };

            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, usuarioAtualizado.Senha);

            var usuarioAtualizadoResult = await _usuarioRepository.AtualizarUsuarioAsync(id, usuario);

            if (usuarioAtualizadoResult == null) {
                return null;
            }

            return new UsuarioRespostaDto {
                Id = usuarioAtualizadoResult.Id,
                Nome = usuarioAtualizadoResult.Nome,
                Email = usuarioAtualizadoResult.Email
            };
        }

        public async Task<bool> DeletarUsuarioAsync(int id) {

            return await _usuarioRepository.DeletarUsuarioAsync(id);
        }
    }
}
