import { Routes } from '@angular/router';
import { CadastroComponent } from './paginas/cadastro/cadastro';
import { ListagemTarefasComponent } from './paginas/listagem-tarefas/listagem-tarefas';
import { TarefaFormComponent } from './paginas/tarefa-form/tarefa-form';

export const routes: Routes = [
  { path: '', redirectTo: 'cadastro', pathMatch: 'full' },
  { path: 'cadastro', component: CadastroComponent },
  { path: 'tarefas/:usuarioId/nova', component: TarefaFormComponent },
  { path: 'tarefas/:usuarioId/editar/:id', component: TarefaFormComponent },
  { path: 'tarefas/:usuarioId', component: ListagemTarefasComponent },
  { path: '**', redirectTo: 'cadastro' }
];