export interface Tarefa {
  id?: number;
  titulo: string;
  descricao: string;
  dataVencimento: string;
  status: '0' | '1';
}