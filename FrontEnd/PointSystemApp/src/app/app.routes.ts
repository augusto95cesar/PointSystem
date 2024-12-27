import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PontosComponent } from './pontos/pontos.component';
import { AuthGuard } from '../core/guard/auth.guard';

export const routes: Routes = [  
    { path: '', component: LoginComponent },  
    { path: 'pontos', component: PontosComponent , canActivate: [AuthGuard]},  
    { path: '**', redirectTo: '' }  // Redireciona qualquer caminho desconhecido para a página inicial.  
  ]; 
