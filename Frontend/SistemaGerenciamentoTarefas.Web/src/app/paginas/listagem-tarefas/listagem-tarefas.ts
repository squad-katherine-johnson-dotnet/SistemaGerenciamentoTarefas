import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Tarefa } from '../../models/tarefa.model';
import { TarefaService } from '../../services/tarefa';

@Component({
  selector: 'app-listagem-tarefas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './listagem-tarefas.html',
  styleUrl: './listagem-tarefas.css'
})
export class ListagemTarefasComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private tarefaService = inject(TarefaService);
  private cd = inject(ChangeDetectorRef);

  usuarioId!: number;
  tarefas: Tarefa[] = [];

  mensagemFeedback: string = '';
  isErro: boolean = false;
  loading: boolean = false;
  mostrarConfirmacaoExclusao: boolean = false;
  tarefaParaExcluir: Tarefa | null = null;


  ngOnInit(): void {
    this.usuarioId = Number(this.route.snapshot.paramMap.get('usuarioId'));

    if (!Number.isInteger(this.usuarioId) || this.usuarioId <= 0) {
      this.isErro = true;
      this.mensagemFeedback = 'Não foi possível identificar o usuário.';
      return;
    }

    this.carregarTarefas();
  }

  carregarTarefas(): void {
    this.loading = true;
    this.mensagemFeedback = '';

    this.tarefaService.obterTarefasPorUsuario(this.usuarioId).subscribe({
      next: (tarefas: Tarefa[]) => {
        this.loading = false;
        this.tarefas = Array.isArray(tarefas) ? tarefas : [];
        this.cd.detectChanges();
      },
      error: (err) => {
        this.loading = false;
        this.tarefas = [];
        this.isErro = true;

        if (err.name === 'TimeoutError') {
          this.mensagemFeedback = 'Erro ao carregar tarefas: a API não respondeu no tempo esperado.';
        } else if (err.status === 0) {
          this.mensagemFeedback = 'Erro ao carregar tarefas. Verifique se o backend está funcionando.';
        } else {
          this.mensagemFeedback =
            err.error?.mensagem ||
            err.error?.Mensagem ||
            'Erro ao carregar tarefas.';
        }

        this.cd.detectChanges();
      }
    });
  }

  novaTarefa(): void {
    this.router.navigate(['/tarefas', this.usuarioId, 'nova']);
  }

  editarTarefa(tarefa: Tarefa): void {
    if (tarefa.id == null) {
      this.isErro = true;
      this.mensagemFeedback = 'Não foi possível identificar a tarefa.';
      return;
    }

    this.router.navigate([
      '/tarefas',
      this.usuarioId,
      'editar',
      tarefa.id
    ]);
  }

  concluirTarefa(tarefa: Tarefa): void {
    if (tarefa.id == null || this.estaConcluida(tarefa)) {
      return;
    }

    this.mensagemFeedback = '';

    this.tarefaService.concluir(this.usuarioId, tarefa.id).subscribe({
      next: (tarefaAtualizada) => {
        this.isErro = false;
        this.mensagemFeedback = 'Tarefa concluída com sucesso!';

        if (tarefaAtualizada) {
          this.tarefas = this.tarefas.map(item =>
            item.id === tarefa.id ? tarefaAtualizada : item
          );
        } else {
          this.carregarTarefas();
        }

        this.cd.detectChanges();
      },
      error: (err) => {
        this.isErro = true;
        this.mensagemFeedback =
          err.error?.mensagem ||
          err.error?.Mensagem ||
          'Erro ao concluir tarefa.';

        this.cd.detectChanges();
      }
    });
  }


  reabrirTarefa(tarefa: Tarefa): void {
    if (tarefa.id == null || !this.estaConcluida(tarefa)) {
      return;
    }

    this.mensagemFeedback = '';

    const tarefaAtualizada: Tarefa = {
      ...tarefa,
      statusTarefa: 0
    };

    this.tarefaService
      .atualizar(this.usuarioId, tarefa.id, tarefaAtualizada)
      .subscribe({
        next: (resposta) => {
          this.tarefas = this.tarefas.map(item =>
            item.id === tarefa.id
              ? (resposta ?? tarefaAtualizada)
              : item
          );

          this.isErro = false;
          this.mensagemFeedback = 'Tarefa reaberta com sucesso!';
          this.cd.detectChanges();
        },
        error: (err) => {
          this.isErro = true;
          this.mensagemFeedback =
            err.error?.mensagem ||
            err.error?.Mensagem ||
            'Erro ao reabrir tarefa.';

          this.cd.detectChanges();
        }
      });
  }


  excluirTarefa(tarefa: Tarefa): void {
    if (tarefa.id == null) {
      this.isErro = true;
      this.mensagemFeedback = 'Não foi possível identificar a tarefa.';
      return;
    }

    this.tarefaParaExcluir = tarefa;
    this.mostrarConfirmacaoExclusao = true;
    return;
  }

  cancelarExclusao(): void {
    this.mostrarConfirmacaoExclusao = false;
    this.tarefaParaExcluir = null;
  }

  confirmarExclusao(): void {
    const tarefa = this.tarefaParaExcluir;
    const tarefaId = tarefa?.id;

    if (tarefa == null || tarefaId == null) {
      return;
    }

    this.mostrarConfirmacaoExclusao = false;
    this.tarefaParaExcluir = null;
    this.mensagemFeedback = '';

    this.tarefaService.deletar(this.usuarioId, tarefaId).subscribe({
      next: () => {
        this.tarefas = this.tarefas.filter(
          item => item.id !== tarefaId
        );

        this.isErro = false;
        this.mensagemFeedback = 'Tarefa excluída com sucesso!';
        this.cd.detectChanges();
      },
      error: (err) => {
        this.isErro = true;
        this.mensagemFeedback =
          err.error?.mensagem ||
          err.error?.Mensagem ||
          'Erro ao excluir tarefa.';

        this.cd.detectChanges();
      }
    });
  }

  estaConcluida(tarefa: Tarefa): boolean {
    return Number(tarefa.statusTarefa) === 1;
  }

  textoStatus(tarefa: Tarefa): string {
    return this.estaConcluida(tarefa) ? 'Concluída' : 'Pendente';
  }
}
