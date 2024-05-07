import { Component } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { UtenteService } from '../../services/utente.service';
import { Stanza } from '../../models/stanza';
import { Messaggi } from '../../models/messaggi';

@Component({
  selector: 'app-chat2.0',
  templateUrl: './chat2.0.component.html',
  styleUrl: './chat2.0.component.css'
})
export class Chat20Component {
  listaMembri: string[] = [];
  listaPeople: string[] = [];
  listaMessaggi: Messaggi[] = [];
  utenteLoggato: string = "";
  stanzaCode: string|undefined;
  testo: string|undefined;
  file: File|undefined;
  imageData: string|undefined;

  constructor(private serviceChat: ChatService,private serviceUtente: UtenteService){}

  onFileSelect(event: Event) {
    let file: any|undefined = event.target as HTMLInputElement;
    file = file.files[0];
    console.log(file);
    const allowedMimeTypes = ["image/png", "image/jpeg", "image/jpg"];
    if (file && allowedMimeTypes.includes(file.type)) {
      const reader = new FileReader();
      reader.onload = () => {
        this.imageData = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  ngOnInit(): void {
    this.listaMembri = this.serviceUtente.getPersoneInChat();
    this.stanzaCode = this.serviceChat.getStanzaCode();
    this.utenteLoggato = this.serviceUtente.getPersonaLoggata();
    this.serviceChat.getMessages().subscribe(response =>{
      this.listaMessaggi = response;
    });

    this.serviceUtente.getAllPeople().subscribe(response =>{
      this.listaPeople = response;
    });
  }

  inviaMessage(){
    console.log("sono qui nell'inviamess");
    let sms: Messaggi = new Messaggi();
    this.listaMessaggi.push(sms); 

    sms.StanzaCode = this.stanzaCode;
    sms.dati = this.testo;
    sms.date = new Date();
    sms.usernameMittente = this.utenteLoggato;
    this.testo = "";

    this.serviceChat.inviaMessage(sms).subscribe(response => {
      console.log(response);
    });
  }

  inviaImg(){
    if(this.imageData){
      let sms: Messaggi = new Messaggi();
      sms.StanzaCode = this.stanzaCode;
      sms.dati = this.imageData;
      sms.date = new Date();
      sms.usernameMittente = this.utenteLoggato;
      this.listaMessaggi.push(sms);   

      this.serviceChat.inviaMessage(sms).subscribe(response => {
        console.log(response);
      });
    }
  }

  settaArray(){
    let array: string[] = new Array();
    this.listaPeople.forEach(element => {
      if(!this.listaMembri.includes(element))
        array.push(element);
    });

    this.listaPeople = array;
  }

  addFriend(i: any){
    let index: number = i;
    this.listaMembri?.push(this.listaPeople[index]);
    
    localStorage.setItem("utenti", JSON.stringify(this.listaMembri)); 
    let room: Stanza = new Stanza();
    room.membri = this.listaMembri;
    room.stanzaCode = this.stanzaCode;
    this.serviceChat.UpdateStanza(room).subscribe(response => {
      console.log(response);
      setTimeout(()=>{
        window.location.reload();
      }, 300);
    });
  }

}
