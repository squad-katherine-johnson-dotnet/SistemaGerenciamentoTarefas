import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../services/usuario';
import { Usuario } from '../../models/usuario.model';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cadastro.html',
  styleUrl: './cadastro.css'
})
export class CadastroComponent {
  usuario: Usuario = { nome: '', email: '', senha: '' };
  mensagem: string = '';

  constructor(private usuarioService: UsuarioService) {}

  onSubmit() {
    this.usuarioService.cadastrar(this.usuario).subscribe({
      next: () => this.mensagem = 'Usuária cadastrada com sucesso!',
      error: () => this.mensagem = 'Erro ao realizar o cadastro.'
    });
  }
}