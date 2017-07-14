CREATE PROCEDURE P_UPDATE_DATAS
AS

UPDATE Anuncio  
	SET status = 'f'
	WHERE EXISTS(SELECT COUNT (Id) 
					FROM Evento e
					WHERE DataRealizacao > getdate()
					)

UPDATE Anuncio  
	SET status = 'f'
	WHERE EXISTS(SELECT COUNT (Id) 
					FROM Vaquinha v
					WHERE DateTermino > getdate()
					)









/*------------------ PARA TESTAR ----------------------*/

insert into usuario (nome, email, senha, 
	notificacaocomentarioanuncioemail,
	notificacaocomentarioanuncioslack,
	notificacaocomentarioanunciobrowser,
	notificacaopresencaemail,
	notificacaopresencaslack,
	notificacaopresencabrowser,
	notificacaointeresseemail,
	notificacaointeresseslack,
	notificacaointeressebrowser,
	notificacaodoacaovaquinhaemail,
	notificacaodoacaovaquinhaslack,
	notificacaodoacaovaquinhabrowser)
values('admin', 'admin@admin', 'sem_hash',
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1,
		1);







					insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio,status)

values ('Churras dos Cresceres', 'Churrasco dos cresceres com várias opções de carne e jogos', 
'https://upload.wikimedia.org/wikipedia/commons/5/5c/Churrasco_carioca.jpg', 
'https://www.auxiliadorapredial.com.br/images/vendas/imoveis/210563/i5uU689O93v97_2105635847081e19135.jpg',
null,'20171009',1,'Evento', 'a')


/*evento*/
insert into evento (id,datarealizacao, local, datamaximaconfirmacao, valorporpessoa)
values (1,'20171013', 'CWI Software, 6� andar', '20171011', 10.0)

insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio,status)

values ('master', 'Churrasco dos cresceres com várias opções de carne e jogos', 
'https://upload.wikimedia.org/wikipedia/commons/5/5c/Churrasco_carioca.jpg', 
'https://www.auxiliadorapredial.com.br/images/vendas/imoveis/210563/i5uU689O93v97_2105635847081e19135.jpg',
null,'20171009',1,'Evento', 'a')


/*evento*/
insert into evento (id,datarealizacao, local, datamaximaconfirmacao, valorporpessoa)
values (1,'20171013', 'CWI Software, 6� andar', '20171011', 10.0)

insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio,status)

values ('evento', 'Churrasco dos cresceres com várias opções de carne e jogos', 
'https://upload.wikimedia.org/wikipedia/commons/5/5c/Churrasco_carioca.jpg', 
'https://www.auxiliadorapredial.com.br/images/vendas/imoveis/210563/i5uU689O93v97_2105635847081e19135.jpg',
null,'20161011',1,'Evento', 'a')


/*evento*/
insert into evento (id,datarealizacao, local, datamaximaconfirmacao, valorporpessoa)
values (1,'20171013', 'CWI Software, 6� andar', '20161011', 10.0)

insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio,status)

values ('alho', 'Churrasco dos cresceres com várias opções de carne e jogos', 
'https://upload.wikimedia.org/wikipedia/commons/5/5c/Churrasco_carioca.jpg', 
'https://www.auxiliadorapredial.com.br/images/vendas/imoveis/210563/i5uU689O93v97_2105635847081e19135.jpg',
null,'20161011',1,'Evento', 'a')


/*evento*/
insert into evento (id,datarealizacao, local, datamaximaconfirmacao, valorporpessoa)
values (1,'20171013', 'CWI Software, 6� andar', '20161011', 10.0)

select * from anuncio

