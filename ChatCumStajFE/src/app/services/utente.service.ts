import { Injectable } from '@angular/core';
import { Utente } from '../models/utente';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../models/response';
import { Token } from '../models/token';

@Injectable({
  providedIn: 'root'
})
export class UtenteService {

  private base_url = "http://localhost:5041/Home/";
  private headerCustom : HttpHeaders = new HttpHeaders().set('Content-Type','Application/Json');
  //elencoUtenti:Utente[] = JSON.parse(localStorage.getItem('elencoUtenti')!) ? JSON.parse(localStorage.getItem('elencoUtenti')!) : new Array();

  constructor(private http: HttpClient) { 
  }

  inserisciUtenteNelDB(objUtente : Utente) : Observable<any>{
    return this.http.post<any>(this.base_url + "inserisciUtente", objUtente, {headers : this.headerCustom});
  }

  loggaUtente (objUtente : Utente) : Observable<Response> {
    return this.http.post<Response>(this.base_url + "loggaUtente", objUtente, {headers : this.headerCustom});
  }

  login (objUtente : Utente) : Observable<Token> {
    return this.http.post<Token>(this.base_url + "login", objUtente, {headers : this.headerCustom});
  }

  getPersonaLoggata(): string{
    let risposta: string = JSON.parse(localStorage.getItem('utenteLoggato')!);
    return risposta;
  }

  getPersoneInChat(){
    return JSON.parse(localStorage.getItem('utenti')!)? JSON.parse(localStorage.getItem('utenti')!) : "";
  }

  getAllPeople(): Observable<string[]>{
    return (this.http.get<string[]>(`${this.base_url}` + "utenti"));
  }
}
