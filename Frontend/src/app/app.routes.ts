import { Routes } from '@angular/router';
import { CadastroComponent } from './paginas/cadastro/cadastro';

export const routes: Routes = [
  { path: '', redirectTo: 'cadastro', pathMatch: 'full' },
  { path: 'cadastro', component: CadastroComponent }
];