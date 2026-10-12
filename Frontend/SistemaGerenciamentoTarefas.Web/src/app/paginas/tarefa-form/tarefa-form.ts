import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TarefaService } from '../../services/tarefa';

@Component({
  selector: 'app-tarefa-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './tarefa-form.html',
  styleUrl: './tarefa-form.css'
})
export class TarefaFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private tarefaService = inject(TarefaService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private cd = inject(ChangeDetectorRef);

  tarefaForm!: FormGroup;
  isEdicao: boolean = false;
  tarefaId?: number;
  usuarioId!: number;

  mensagemFeedback: string = '';
  isErro: boolean = false;
  loading: boolean = false;

  ngOnInit(): void {
    const usuarioIdParam = this.route.snapshot.paramMap.get('usuarioId');
    const idParam = this.route.snapshot.paramMap.get('id');

    this.usuarioId = Number(usuarioIdParam);

    if (
      usuarioIdParam === null ||
      !Number.isInteger(this.usuarioId) ||
      this.usuarioId <= 0
    ) {
      this.isErro = true;
      this.mensagemFeedback = 'ID de usuário inválido.';
      return;
    }

    if (idParam !== null) {
      this.isEdicao = true;
      this.tarefaId = Number(idParam);

      if (!Number.isInteger(this.tarefaId) || this.tarefaId <= 0) {
        this.isErro = true;
        this.mensagemFeedback = 'ID de tarefa inválido.';
        return;
      }
    }

    this.tarefaForm = this.fb.group({
      titulo: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
        Validators.pattern(/.*\S.*/)
      ]],
      descricao: ['', [
        Validators.required,
        Validators.maxLength(500),
        Validators.pattern(/.*\S.*/)
      ]],
      dataVencimento: ['', [Validators.required]],
      statusTarefa: [0, [Validators.required]]
    });


    if (this.isEdicao && this.tarefaId) {
      this.carregarTarefa(this.tarefaId);
    }
  }

  carregarTarefa(id: number): void {
    this.loading = true;
    this.tarefaService.obterTarefasPorUsuario(this.usuarioId).subscribe({
      next: (tarefas: any[]) => {
        this.loading = false;
        const tarefa = tarefas.find(t => t.id === id);
        if (tarefa) {
          this.tarefaForm.patchValue({
            titulo: tarefa.titulo,
            descricao: tarefa.descricao,
            dataVencimento: tarefa.dataVencimento ? tarefa.dataVencimento.split('T')[0] : '',
            statusTarefa: tarefa.statusTarefa !== undefined ? Number(tarefa.statusTarefa) : (tarefa.status === 'Concluida' || tarefa.status === 'Concluída' ? 1 : 0)
          });
        }
        else {
          this.isErro = true;
          this.mensagemFeedback = 'Tarefa não encontrada.';
        }
        this.cd.detectChanges();
      },
      error: (err) => {
        this.loading = false;
        this.isErro = true;
        this.mensagemFeedback = 'Erro ao carregar dados da tarefa.';
        this.cd.detectChanges();
      }
    });
  }

  onSubmit(): void {
    if (this.tarefaForm.invalid) {
      this.tarefaForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.mensagemFeedback = '';

    const formValues = this.tarefaForm.value;
    const dadosTarefa = {
      ...formValues,
      statusTarefa: Number(formValues.statusTarefa)
    };

    if (this.isEdicao && this.tarefaId) {
      this.tarefaService.atualizar(this.usuarioId, this.tarefaId, dadosTarefa).subscribe({
        next: () => {
          this.loading = false;
          this.mensagemFeedback = 'Tarefa atualizada com sucesso!';
          this.isErro = false;
          this.cd.detectChanges();

          setTimeout(() => {
            this.router.navigate(['/tarefas', this.usuarioId]);
          }, 1500);
        },
        error: (err) => {
          this.loading = false;
          this.isErro = true;
          this.mensagemFeedback = 'Erro ao atualizar tarefa.';
          this.cd.detectChanges();
        }
      });
    } else {
      this.tarefaService.criar(this.usuarioId, dadosTarefa).subscribe({
        next: () => {
          this.loading = false;
          this.mensagemFeedback = 'Tarefa criada com sucesso!';
          this.isErro = false;
          this.cd.detectChanges();

          setTimeout(() => {
            this.router.navigate(['/tarefas', this.usuarioId]);
          }, 1500);
        }
        ,
        error: (err) => {
          this.loading = false;
          this.isErro = true;
          this.mensagemFeedback = 'Erro ao criar tarefa.';
          this.cd.detectChanges();
        }
      });
    }
  }
}
