using SistemaGerenciamentoTarefas.API.Models;
using SistemaGerenciamentoTarefas.API.Repositories;

namespace SistemaGerenciamentoTarefas.API.Services {
    public class UsuarioService : IUsuarioService {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> BuscarTodosUsuariosAsync() {

            return await _usuarioRepository.BuscarTodosUsuariosAsync();
        }

        public async Task<Usuario?> BuscarUsuarioPorIdAsync(int id) {

            return await _usuarioRepository.BuscarUsuarioPorIdAsync(id);
        }

        public async Task<Usuario> CadastrarUsuarioAsync(Usuario usuario) {

            if (await _usuarioRepository.EmailExisteAsync(usuario.Email)) {
                throw new ArgumentException("E-mail já cadastrado.");
            }

            return await _usuarioRepository.CadastrarUsuarioAsync(usuario);
        }

        public async Task<Usuario?> AtualizarUsuarioAsync(int id, Usuario usuarioAtualizado) {

            if (await _usuarioRepository.EmailExisteAsync(usuarioAtualizado.Email, id)) {
                throw new ArgumentException("E-mail já cadastrado.");
            }

            return await _usuarioRepository.AtualizarUsuarioAsync(id, usuarioAtualizado);
        }

        public async Task<bool> DeletarUsuarioAsync(int id) {

            return await _usuarioRepository.DeletarUsuarioAsync(id);
        }
    }
}
