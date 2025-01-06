import { Injectable } from '@angular/core';
import { AppJsonConfig } from '../config/appjson';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = AppJsonConfig.apiBaseUrl;
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient, private router: Router) { }

  createLogin(username: string, password: string): Observable<any> { 
    var url = '/Auth/register?email='+username+'&password='+password 
    return this.http.post(`${this.apiUrl + url}`, {}) 
  }


  login(username: string, password: string): Observable<any> {

    return this.http.post<any>(`${this.apiUrl + '/Auth/login'}`, { email: username, password: password })
      .pipe(
        tap(response => {
          localStorage.setItem(this.tokenKey, response.token); // Salvar o token  
          localStorage.setItem('username', username); // Salvar o usuario  
        })
      );
  }

  logout() {
    localStorage.removeItem(this.tokenKey); // Remover o token  
    localStorage.removeItem('username');  
    this.router.navigate(['/login']); // Navegar para a página de login  
  }

  isAuthenticated(): boolean {
    var existe = !!localStorage.getItem(this.tokenKey);

    if (existe === false) {
      this.router.navigate(['/login']);
    }

    return true // Retorna true se o token existir  
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey); 
  }

  getUser(): string | null {
    return localStorage.getItem('username'); 
  }
}
