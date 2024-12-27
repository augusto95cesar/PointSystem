// src/app/auth.guard.ts  
import { Injectable } from '@angular/core';  
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';  
import { AuthService } from '../service/auth.service'; // ou o caminho correto para o seu serviço de autenticação  

@Injectable({  
  providedIn: 'root'  
})  
export class AuthGuard implements CanActivate {  
  
  constructor(private authService: AuthService, private router: Router) {}  

  canActivate(  
    route: ActivatedRouteSnapshot,  
    state: RouterStateSnapshot): boolean {  
    const isAuthenticated = this.authService.isAuthenticated(); // Método que verifica autenticação  
    if (!isAuthenticated) {  
      this.router.navigate(['/login']); // Redireciona para a página de login  
    }  
    return isAuthenticated;  
  }  
}