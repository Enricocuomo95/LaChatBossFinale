import { Component } from '@angular/core';
import { UtenteService } from '../../services/utente.service';
import { Utente } from '../../models/utente';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  Username: string|undefined;
  Password: string|undefined;

  constructor(private utenteService: UtenteService, private router: Router){ }

  resetValue(){
    this.Password = "";
    this.Username = "";
  }

  btnLogin(){
    if((this.Username != "")&&(this.Password != "")){
      localStorage.setItem("utenteLoggato", JSON.stringify(this.Username));
      this.utenteService.loggaUtente(new Utente(this.Username,this.Password)).subscribe(res =>{
        if(res == null){
          Swal.fire({
            icon: "success",
            title: "Bentornato nella nostra chat!",
            showConfirmButton: false,
            timer: 1500
          });

          this.utenteService.login(new Utente(this.Username,this.Password)).subscribe(res =>{
            console.log("qui ci vuole un bel tokennn ahhhh belloooo");
            localStorage.setItem("token", JSON.stringify(res.token));
          });
          this.router.navigate(['chat']);
        }        
      });
      
      Swal.fire({
        title: "Auto close alert!",
        html: "I will close in <b></b> milliseconds.",
        timer: 3000,
        timerProgressBar: true,
        didOpen: () => {
          Swal.showLoading();
        }
      });

      Swal.fire({
        icon: "warning",
        title: "Sto elabborando la richiesta",
        showConfirmButton: false,
        timer: 1500
      });
      this.resetValue(); 
    }
  }


  Registrazione(){
    this.router.navigate(['registrazione']);
  }
}
