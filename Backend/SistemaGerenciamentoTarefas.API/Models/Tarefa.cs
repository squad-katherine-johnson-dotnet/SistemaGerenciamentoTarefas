using SistemaGerenciamentoTarefas.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaGerenciamentoTarefas.API.Models {
    public class Tarefa {

        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }
        public StatusTarefa StatusTarefa { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
