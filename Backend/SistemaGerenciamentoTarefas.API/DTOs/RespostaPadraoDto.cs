namespace SistemaGerenciamentoTarefas.API.DTOs {
    public class RespostaPadraoDto {

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object? Dados { get; set; }
        public object? Erros { get; set; }
    }
}
