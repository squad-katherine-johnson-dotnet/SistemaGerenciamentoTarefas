import { Component, inject, OnInit } from '@angular/core';
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

  tarefaForm!: FormGroup;
  isEdicao: boolean = false;
  tarefaId?: number;
  usuarioId!: number;
  
  mensagemFeedback: string = '';
  isErro: boolean = false;
  loading: boolean = false;

  ngOnInit(): void {
    // 1. Captura o usuarioId e o id da tarefa (se existir) na URL
    this.usuarioId = Number(this.route.snapshot.paramMap.get('usuarioId'));
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.isEdicao = true;
      this.tarefaId = Number(idParam);
    }

    // 2. Inicializa o formulário com os validadores
    this.tarefaForm = this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      descricao: ['', [Validators.required, Validators.maxLength(500)]],
      dataVencimento: ['', [Validators.required]],
      status: ['Pendente', [Validators.required]]
    });

    // 3. Se for modo edição, carrega os dados existentes via GET
    if (this.isEdicao && this.tarefaId) {
      this.carregarTarefa(this.tarefaId);
    }
  }

  carregarTarefa(id: number): void {
    this.loading = true;
    this.tarefaService.obterPorId(id).subscribe({
      next: (tarefa: any) => {
        this.loading = false;
        // Preenche o formulário com os dados vindos do backend
        this.tarefaForm.patchValue({
          titulo: tarefa.titulo,
          descricao: tarefa.descricao,
          dataVencimento: tarefa.dataVencimento ? tarefa.dataVencimento.split('T')[0] : '',
          status: tarefa.status
        });
      },
      error: (err) => {
        this.loading = false;
        this.isErro = true;
        this.mensagemFeedback = 'Erro ao carregar dados da tarefa.';
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

    const dadosTarefa = {
      ...this.tarefaForm.value,
      usuarioId: this.usuarioId
    };

    if (this.isEdicao && this.tarefaId) {
      // Atualizar Tarefa Existente
      this.tarefaService.atualizar(this.tarefaId, dadosTarefa).subscribe({
        next: () => {
          this.loading = false;
          this.mensagemFeedback = 'Tarefa atualizada com sucesso!';
          this.isErro = false;
        },
        error: (err) => {
          this.loading = false;
          this.isErro = true;
          this.mensagemFeedback = 'Erro ao atualizar tarefa.';
        }
      });
    } else {
      // Criar Nova Tarefa
      this.tarefaService.criar(dadosTarefa).subscribe({
        next: () => {
          this.loading = false;
          this.mensagemFeedback = 'Tarefa criada com sucesso!';
          this.isErro = false;
          this.tarefaForm.reset({ status: 'Pendente' });
        },
        error: (err) => {
          this.loading = false;
          this.isErro = true;
          this.mensagemFeedback = 'Erro ao criar tarefa.';
        }
      });
    }
  }
}