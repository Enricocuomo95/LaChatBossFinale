import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UtenteService } from '../../services/utente.service';
import { Utente } from '../../models/utente';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-registrazione',
  templateUrl: './registrazione.component.html',
  styleUrl: './registrazione.component.css'
})
export class RegistrazioneComponent {

  Username: string = "";
  Passward: string = "";
  Passward1: string = "";

  constructor(private router :Router, private utenteService: UtenteService){}

  Insert(){
    console.log(this.Username + " " + this.Passward + "siamo nel controller registrazione");
    if((this.Username != "")&&(this.Passward != "")&&(this.Passward == this.Passward1)){
      this.utenteService.inserisciUtenteNelDB(new Utente(this.Username,this.Passward)).subscribe(res =>{
        console.log(res);
      });

      Swal.fire({
        icon: "success",
        title: "Benvenuto nella nostra chat!",
        showConfirmButton: false,
        timer: 1500
      });

      this.router.navigate(['inserisci']);
    }
  }

}
