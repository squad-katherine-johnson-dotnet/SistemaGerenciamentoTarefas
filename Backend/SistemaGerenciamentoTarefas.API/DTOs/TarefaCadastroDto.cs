using System.ComponentModel.DataAnnotations;
using SistemaGerenciamentoTarefas.API.Enums;

namespace SistemaGerenciamentoTarefas.API.DTOs
{
    public class TarefaCadastroDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime? DataVencimento { get; set; }

        [EnumDataType(
            typeof(StatusTarefa),
            ErrorMessage = "O status deve ser 0 (pendente) ou 1 (concluída).")]
        public StatusTarefa StatusTarefa { get; set; }
    }
}