using System.ComponentModel.DataAnnotations;

namespace SistemaGerenciamentoTarefas.API.Models {
    public class Usuario {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string SenhaHash { get; set; }

    }
}
