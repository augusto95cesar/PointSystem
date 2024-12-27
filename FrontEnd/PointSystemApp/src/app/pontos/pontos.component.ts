import { Component, ElementRef, ViewChild } from '@angular/core';
 import { Router } from '@angular/router';
import {Ponto} from '../../core/models/pontos.model'
import { PontosService } from '../../core/service/pontos.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Importe o CommonModule  
import { AuthService } from '../../core/service/auth.service';

@Component({
  selector: 'app-pontos',
  imports: [FormsModule, CommonModule],
  templateUrl: './pontos.component.html',
  styleUrl: './pontos.component.css'
})
export class PontosComponent {
  pontos : Ponto[] = []

  constructor(private pontoService: PontosService, private auth:AuthService, private router: Router) { }  
  
  ngOnInit(): void {   
    this.loadDePontos();
  }

  loadDePontos(){
    this.pontoService.getLista().subscribe(  
      (response) => {  
        this.pontos = response; // Armazena a lista recebida na propriedade `items`          
      },  
      (error) => {  
        console.error('Erro ao obter dados:', error);  
        this.auth.logout();
      }  
    );  
  }

  // Estados dos checkboxes
  isEntradaChecked: boolean = true;
  isSaidaChecked: boolean = false;

  // Alterna os valores das caixas
  onEntradaChange() {
    if (this.isEntradaChecked) {
      this.isSaidaChecked = false;
    }else{
      this.isSaidaChecked = true;
    }
  }

  onSaidaChange() {
    if (this.isSaidaChecked) {
      this.isEntradaChecked = false;
    }
    else{
      this.isEntradaChecked = true;
    }
  }

 

  RegistrarPonto(){ 
    this.pontoService.postPonto(this.isEntradaChecked).subscribe({  
      next: () => {  
        this.loadDePontos();
      },  
      error: (err) => {  
        //this.errorMessage = 'Erro ao fazer login. Verifique suas credenciais.';  
        console.log(err.error);
        alert(err.error)
      }  
    }); 
  }
}
