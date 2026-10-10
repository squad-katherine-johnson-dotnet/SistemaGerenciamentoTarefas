import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './cadastro.html',
  styleUrl: './cadastro.css'
})
export class CadastroComponent {
  private fb = inject(FormBuilder);
  private usuarioService = inject(UsuarioService);
  private router = inject(Router);

  mensagemFeedback: string = '';
  isErro: boolean = false;
  loading: boolean = false;

  cadastroForm: FormGroup = this.fb.group({
    nome: ['', [Validators.required,
     Validators.minLength(3),
     Validators.maxLength(100),
     Validators.pattern(/^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$/)]], // Validação para permitir apenas letras (com acentos) e espaços
    email: ['', [Validators.required, Validators.email]],
    senha: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit(): void {
    if (this.cadastroForm.invalid) {
      this.cadastroForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.mensagemFeedback = '';

    this.usuarioService.cadastrar(this.cadastroForm.value).subscribe({
      next: (res: any) => {
        this.loading = false;
        this.mensagemFeedback = 'Cadastro realizado com sucesso!';
        this.isErro = false;

        // Redireciona de forma segura após cadastrar
        setTimeout(() => {
          this.router.navigate(['/cadastro']);
        }, 1000);
      },
      error: (err) => {
        this.loading = false;
        this.isErro = true;

        if (err.status === 409) {
          this.mensagemFeedback = 'E-mail já cadastrado no sistema.';
        } else {
          this.mensagemFeedback = err.error?.mensagem || 'Erro ao realizar cadastro.';
        }
      }
    });
  }
}