using SistemaGerenciamentoTarefas.API.Models;
using SistemaGerenciamentoTarefas.API.Repositories;

namespace SistemaGerenciamentoTarefas.API.Services {
    public class UsuarioService : IUsuarioService {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository) {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AdicionarUsuarioAsync(Usuario usuario) {

            if (await _usuarioRepository.EmailExisteAsync(usuario.Email)) {
                throw new ArgumentException("E-mail já cadastrado.");
            }

            return await _usuarioRepository.AdicionarUsuarioAsync(usuario);
        }
    }
}
