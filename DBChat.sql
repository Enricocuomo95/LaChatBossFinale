use ChatChattiamo;

drop table if exists Utente;
create table Utente(
	username varchar(255) primary key,
	passward varchar(255) not null
);

select * from Utente;