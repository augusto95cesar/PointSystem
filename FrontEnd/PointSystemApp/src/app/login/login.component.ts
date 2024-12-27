import { Component } from '@angular/core';
 import { Router } from '@angular/router';  
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/service/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  username: string = '';  
  password: string = '';  
  errorMessage: string = '';  

  constructor(private authService: AuthService, private router: Router) { }  

  login() {  
     this.authService.login(this.username, this.password).subscribe({  
      next: () => {  
        this.router.navigate(['/pontos']); // Navegar para a página principal após o login  
      },  
      error: (err) => {  
        this.errorMessage = 'Erro ao fazer login. Verifique suas credenciais.';  
      }  
    });  
  }  
}
