using System.ComponentModel.DataAnnotations;

namespace SistemaGerenciamentoTarefas.API.DTOs {
    public class UsuarioAtualizacaoDto {

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}