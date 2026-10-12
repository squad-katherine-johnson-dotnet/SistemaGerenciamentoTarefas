using Microsoft.EntityFrameworkCore;
using SistemaGerenciamentoTarefas.API.Data;
using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Repositories {

    public class UsuarioRepository : IUsuarioRepository {

        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<List<Usuario>> BuscarTodosUsuariosAsync() {

            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> BuscarUsuarioPorIdAsync(int id) {

            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CadastrarUsuarioAsync(Usuario usuario) {

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> EmailExisteAsync(string email, int? usuarioId = null) {

            return await _context.Usuarios.AnyAsync(u => u.Email == email && u.Id != usuarioId);
        }
    }
}