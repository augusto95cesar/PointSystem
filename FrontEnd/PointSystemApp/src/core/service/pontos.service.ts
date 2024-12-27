import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AppJsonConfig } from '../config/appjson';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { Ponto } from '../models/pontos.model';


@Injectable({
  providedIn: 'root'
})
export class PontosService {
 
  private apiUrl =  AppJsonConfig.apiBaseUrl;

  constructor(private http: HttpClient, private router: Router, private auth:AuthService) { }  

  getLista(): Observable<Ponto[]> {  
    var token = this.auth.getToken();

    const headers = new HttpHeaders({  
      'Authorization': `${token}` // Adiciona o token de autenticação  
    });  

    return  this.http.get<Ponto[]>(`${this.apiUrl}/ControleDePonto`, { headers }); 
  }
  
  postPonto(isEntradaChecked: any): Observable<any> { 
    var url = `${this.apiUrl}`;
    if(isEntradaChecked){
      url = url + '/ControleDePonto/RegistrarEntrada'
    }else{
      url = url + '/ControleDePonto/RegistrarSaida'
    }
    
    var token = this.auth.getToken();
    console.log(url)
    console.log(token)


   
    const headers = new HttpHeaders({
      Authorization: `${token}`,
      'Content-Type': 'application/json',
    });

    return this.http.post(url, {}, { headers }); // Envia um corpo 
  }
}
