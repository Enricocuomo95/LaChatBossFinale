export class Utente {
    username: string | undefined;
    passward: string | undefined;

    constructor(username?: string, passward?: string){
        this.username = username;
        this.passward = passward;
    }
}
