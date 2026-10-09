using SistemaGerenciamentoTarefas.API.Enums;

namespace SistemaGerenciamentoTarefas.API.DTOs
{
    public class TarefaRespostaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public DateTime DataVencimento { get; set; }

        public StatusTarefa StatusTarefa { get; set; }

        public int UsuarioId { get; set; }
    }
}