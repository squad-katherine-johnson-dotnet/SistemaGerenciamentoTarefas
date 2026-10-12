import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario.model';

// Contrato de resposta padronizado pela squad
export interface RespostaPadrao<T> {
  sucesso: boolean;
  mensagem: string;
  dados?: T;
  erros?: Record<string, string[]>;
}

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5189/api/usuarios'; // Ajuste a porta da API se necessário

  cadastrar(usuario: Omit<Usuario, 'id'>): Observable<RespostaPadrao<Usuario>> {
    return this.http.post<RespostaPadrao<Usuario>>(this.apiUrl, usuario);
  }
}