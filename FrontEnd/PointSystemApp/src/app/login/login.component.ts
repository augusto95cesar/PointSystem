import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroupDirective, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../core/service/auth.service';
import { ChangeDetectionStrategy } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  imports: [FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,
    MatButtonModule, MatDividerModule, MatIconModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class LoginComponent {

  username: string = '';
  password: string = '';
  senha: string = '';

  emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  matcher = new MyErrorStateMatcher();

  constructor(private authService: AuthService, private router: Router) {}

  login() { 
    if (this.username != "" && this.password != "") {
      this.authService.login(this.username, this.password).subscribe({
        next: () => {
          this.router.navigate(['/pontos']); // Navegar para a página principal após o login  
        },
        error: (err) => {
          console.log(err)
          //this.errorMessage = 'Erro ao fazer login. Verifique suas credenciais.';  
        }
      });
    }
  }

  createUser(){
    if(this.username === '' || this.password ===''){
      alert('preencha os campos de login e senha!');
    }else{
      const observer = {
        next: (response: any) => {
          alert('Login cadastrado com sucesso!'); 
        },
        error: (err: any) => {
          const errorMessage = err?.error?.[0]?.code || 'Erro ao fazer login. Verifique suas credenciais.';
          alert(errorMessage);
        },
        complete: () => {
          console.log('Requisição concluída');
        },
      };
  
      this.authService.createLogin(this.username, this.password).subscribe(observer); 
    } 
  }
  
}
