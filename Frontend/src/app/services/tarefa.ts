import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Tarefa } from '../models/tarefa.model';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {
  private baseUrl = 'http://localhost:5189/api/usuarios';

  constructor(private http: HttpClient) {}

  obterTarefasPorUsuario(usuarioId: number): Observable<Tarefa[]> {
    return this.http.get<any>(`${this.baseUrl}/${usuarioId}/tarefas`).pipe(
      map(res => res.dados)
    );
  }

  criar(usuarioId: number, tarefa: Tarefa): Observable<Tarefa> {
    return this.http.post<any>(`${this.baseUrl}/${usuarioId}/tarefas`, tarefa).pipe(
      map(res => res.dados)
    );
  }

  atualizar(usuarioId: number, tarefaId: number, tarefa: Tarefa): Observable<Tarefa> {
    return this.http.put<any>(`${this.baseUrl}/${usuarioId}/tarefas/${tarefaId}`, tarefa).pipe(
      map(res => res.dados)
    );
  }

  deletar(usuarioId: number, tarefaId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${usuarioId}/tarefas/${tarefaId}`);
  }
}