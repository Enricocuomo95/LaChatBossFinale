import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UtenteService } from '../../services/utente.service';
import { ChatService } from '../../services/chat.service';
import { Stanza } from '../../models/stanza';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {
  TitoloStanza: string|undefined;
  DescrizioneStanza: string = "";
  listaStanze: Stanza[] = new Array();
  Indice: number = -1;

  
  constructor(private router: Router,private serviceUtente: UtenteService,private serviceChat: ChatService){}

  settaVisibilita(i: any){
    let index: number = i;
    this.Indice = index;
    console.log("dopo " + this.Indice);
  }

  ngOnInit(): void {
    //inizializzo la lista delle stanze
    this.serviceChat.stampaUser();
    this.serviceChat.getStanze().subscribe(risultato =>{
      this.listaStanze = risultato;
      console.log(this.listaStanze[0].title);
    });    
  }

  CreaStanza(){
    if(this.TitoloStanza != undefined && this.TitoloStanza != ""){
        let objStanza: Stanza = new Stanza();
        objStanza.title = this.TitoloStanza;
        objStanza.descrizione = this.DescrizioneStanza;
        this.serviceChat.CreaStanza(objStanza).subscribe(res =>{
          console.log(res);
          setTimeout(()=>{
            window.location.reload();
          }, 100);
        });
    } else {
      Swal.fire({
        icon: "error",
        title: "Titolo non valido",
        showConfirmButton: false,
        timer: 1500
      });
    }
  }

  VaiAllaChat(i: any){
    let index: number = i;
    localStorage.setItem("utenti", JSON.stringify(this.listaStanze[index].membri));
    localStorage.setItem("stanzaCode", JSON.stringify(this.listaStanze[index].stanzaCode));
    this.router.navigate(["chat2"]);
  }
    
  
}
