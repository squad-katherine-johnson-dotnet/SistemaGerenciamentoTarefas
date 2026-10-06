using SistemaGerenciamentoTarefas.API.Enums;

namespace SistemaGerenciamentoTarefas.API.Models {
    public class Tarefa {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public StatusTarefa StatusTarefa { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
