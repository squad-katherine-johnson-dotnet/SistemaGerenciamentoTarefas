using Microsoft.EntityFrameworkCore;
using SistemaGerenciamentoTarefas.API.Data;
using SistemaGerenciamentoTarefas.API.Models;

namespace SistemaGerenciamentoTarefas.API.Repositories {
    public class UsuarioRepository : IUsuarioRepository {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<bool> EmailExisteAsync(string email) {

            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario> AdicionarUsuarioAsync(Usuario usuario) {

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}