import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Stanza } from '../models/stanza';
import { Messaggi } from '../models/messaggi';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private base_url : string = "http://localhost:5041/Home/";
  private headerCustom : HttpHeaders = new HttpHeaders().set('Content-Type','Application/Json');
  private user: string = "";

  constructor( private http: HttpClient) { }

  stampaUser(){
    this.user = JSON.parse(localStorage.getItem('utenteLoggato')!) ? JSON.parse(localStorage.getItem('utenteLoggato')!) : "";
    console.log("hey questo è l'user loggato: "+ this.user);
  }

  CreaStanza(objStanza: Stanza): Observable<any>{
    objStanza.membri = new Array();
    objStanza.membri.push(this.user);
    objStanza.stanzaCode = this.user + objStanza.title;
    return this.http.post<any>(this.base_url + "creaStanza", objStanza, {headers : this.headerCustom});
  }  

  UpdateStanza(objStanza: Stanza): Observable<any>{
    return this.http.post<any>(this.base_url + "updateStanza", objStanza, {headers : this.headerCustom});
  }

  inviaMessage(objMessage: Messaggi): Observable<any>{
    return this.http.post<any>(this.base_url + "creaMessaggio", objMessage, {headers : this.headerCustom});
  }

  getMessages(): Observable<Messaggi[]>{
    return this.http.post<Messaggi[]>(this.base_url + this.getStanzaCode(), {headers : this.headerCustom});
  }

  getStanze(): Observable<Stanza[]>{
    return (this.http.get<Stanza[]>(`${this.base_url}` + this.user));
  }

  getStanzaCode(){
    return JSON.parse(localStorage.getItem('stanzaCode')!)? JSON.parse(localStorage.getItem('stanzaCode')!) : "";
  }
}
