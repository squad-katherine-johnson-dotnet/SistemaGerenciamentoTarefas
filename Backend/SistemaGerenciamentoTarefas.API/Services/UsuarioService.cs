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

        public async Task<List<Usuario>> BuscarTodosUsuariosAsync() {

            return await _usuarioRepository.BuscarTodosUsuariosAsync();
        }

        public async Task<Usuario?> BuscarUsuarioPorIdAsync(int id) {

            return await _usuarioRepository.BuscarUsuarioPorIdAsync(id);
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

        public async Task<Usuario?> AtualizarUsuarioAsync(int id, Usuario usuarioAtualizado) {

            if (await _usuarioRepository.EmailExisteAsync(usuarioAtualizado.Email)) {
                throw new ArgumentException("E-mail já cadastrado.");
            }

            return await _usuarioRepository.AtualizarUsuarioAsync(id, usuarioAtualizado);
        }

        public async Task<bool> DeletarUsuarioAsync(int id) {

            return await _usuarioRepository.DeletarUsuarioAsync(id);
        }
    }
}
