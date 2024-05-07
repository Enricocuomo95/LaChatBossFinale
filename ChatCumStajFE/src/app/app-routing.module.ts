import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrazioneComponent } from './components/registrazione/registrazione.component';
import { Chat20Component } from './components/chat2.0/chat2.0.component';
import { ChatComponent } from './components/chat/chat.component';

const routes: Routes = [
  {path: "", redirectTo: "inserisci", pathMatch: "full"},
  {path: "registrazione", component: RegistrazioneComponent},
  {path: "chat", component: ChatComponent},
  {path: "chat2", component: Chat20Component},
  {path: "inserisci", component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
