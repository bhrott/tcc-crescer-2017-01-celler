select * from produto


/*usuario*/
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
values('teste', 'teste@teste', 'af51a138526519a7ae40c41e7cae5d5c',
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


/*anuncio*/
insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio,status)

values ('Churras dos Cresceres', 'Churrasco dos cresceres com várias opções de carne e jogos', 
'https://upload.wikimedia.org/wikipedia/commons/5/5c/Churrasco_carioca.jpg', 
'https://www.auxiliadorapredial.com.br/images/vendas/imoveis/210563/i5uU689O93v97_2105635847081e19135.jpg',
null,'20171001',4,'Evento', 'a')


/*evento*/
insert into evento (id,datarealizacao, local, datamaximaconfirmacao, valorporpessoa)
values (5,'20171003', 'CWI Software, 6° andar', '20171001', 10.0)

/*confirmado*/
insert into confirmadoevento(idevento, idusuario)
values (5,4)

/*comentario*/
insert into comentario (texto, datacomentario, idusuario, idanuncio)
values ('Uhul!', '20171002', 4, 5)

insert into comentario (texto, datacomentario, idusuario, idanuncio)
values ('Massa!', '20171002', 2, 5)


/*anuncio*/
insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio,status)

values ('Palestra motivacional', 'Palestra motivacional com um tema diferenciado, que o fará se sentir muito melhor e muito mais entediado', 
'http://3.bp.blogspot.com/-X14NwlGRyLk/T2QfWLbOvkI/AAAAAAAAHV0/Y79Ooj2Gweg/s1600/SUCESSO.jpg', 
null,
null,'20171001',2,'Evento', 'a')

/*evento*/
insert into evento (id,datarealizacao, local, datamaximaconfirmacao, valorporpessoa)
values (6,'20171003', 'Auditório do Tecnosinos', '20171001', 0.0)

/*comentario*/
insert into comentario (texto, datacomentario, idusuario, idanuncio)
values ('Que chato!', '20171002', 4, 6)







/*anuncio*/
insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio, status)

values ('Comic do Batman', 'Comic raro do batman da minha coleção pessoal, pechincha!', 
'https://images-na.ssl-images-amazon.com/images/I/51L4f5ztm0L._SY344_BO1,204,203,200_.jpg', 
null,
null,'20171001',2,'Produto', 'a')

/*produto*/
insert into produto (id, idcomprador, valor)
values (7,null, 35.0)

/*comentario*/
insert into comentario (texto, datacomentario, idusuario, idanuncio)
values ('Why so serious, São Leopoldo?', '20171002', 4, 7)






/*anuncio*/
insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio, status)

values ('Xícara personalizada', 'Faço xícaras personalizadas', 
null, 
null,
null,'20171001',2,'Produto', 'a')

/*produto*/
insert into produto (id, idcomprador, valor)
values (9,null, 35.0)

/*interessado*/
insert into interessadoproduto(idproduto, idusuario)
values (9,4)








/*anuncio*/
insert into anuncio (titulo, descricao, foto1, foto2, foto3, dataanuncio, idcriador, tipoanuncio, status)

values ('Vaquinha para comparar miçangas', 'Ajude-nos a fazer um estoque de miçangas para o inverno', 
'http://www.fishingtur.com.br/imagens/artigos/100/f2.jpg', 
null,
null,'20171001',4,'Vaquinha', 'a')

/*vaquinha*/
insert into vaquinha (id, arrecadamentoprevisto, totalarrecadado, datetermino)
values (8,1500.0, 35.0, '20171225')

/*interessado*/
insert into doador(valordoado, idusuario, status)
values (35.0,4,'n')

insert into doadorvaquinha (idvaquinha, iddoador)
values (8,1)