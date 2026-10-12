export interface Tarefa {
  id?: number;
  titulo: string;
  descricao: string;
  dataVencimento: string;
  statusTarefa: number;
  usuarioId?: number;
}