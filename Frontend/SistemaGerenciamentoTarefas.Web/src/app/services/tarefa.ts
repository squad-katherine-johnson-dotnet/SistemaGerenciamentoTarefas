
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, timeout } from 'rxjs';
import { Tarefa } from '../models/tarefa.model';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {
  private readonly baseUrl = 'http://localhost:5189/api/usuarios';
  private readonly tempoLimite = 10000;

  constructor(private readonly http: HttpClient) {}

  private tratarResposta<T>(resposta: any): T {
    return (resposta?.dados ?? resposta?.Dados ?? resposta) as T;
  }

  obterTarefasPorUsuario(usuarioId: number): Observable<Tarefa[]> {
    return this.http
      .get<any>(`${this.baseUrl}/${usuarioId}/tarefas`)
      .pipe(
        timeout({ first: this.tempoLimite }),
        map(resposta => {
          const dados = this.tratarResposta<any>(resposta);
          return Array.isArray(dados) ? dados : [];
        })
      );
  }

  criar(usuarioId: number, tarefa: Tarefa): Observable<Tarefa> {
    return this.http
      .post<any>(`${this.baseUrl}/${usuarioId}/tarefas`, tarefa)
      .pipe(
        timeout({ first: this.tempoLimite }),
        map(resposta => this.tratarResposta<Tarefa>(resposta))
      );
  }

  atualizar(usuarioId: number, tarefaId: number, tarefa: Tarefa): 
  Observable<Tarefa> {
    return this.http
      .put<any>(
        `${this.baseUrl}/${usuarioId}/tarefas/${tarefaId}`,
        tarefa
      )
      .pipe(
        timeout({ first: this.tempoLimite }),
        map(resposta => this.tratarResposta<Tarefa>(resposta))
      );
  }

  concluir(usuarioId: number, tarefaId: number): 
  Observable<Tarefa> {
    return this.http
      .patch<any>(
        `${this.baseUrl}/${usuarioId}/tarefas/${tarefaId}/concluir`,
        {}
      )
      .pipe(
        timeout({ first: this.tempoLimite }),
        map(resposta => this.tratarResposta<Tarefa>(resposta))
      );
  }

  deletar(usuarioId: number, tarefaId: number): Observable<any> {
    return this.http
      .delete<any>(
        `${this.baseUrl}/${usuarioId}/tarefas/${tarefaId}`
      )
      .pipe(timeout({ first: this.tempoLimite }));
  }
}