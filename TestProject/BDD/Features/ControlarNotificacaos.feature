Feature: ControlarNotificacaos
	Para controlar os notificacoes do processamento de imagem
	Eu preciso das seguindes funcionalidades
	Adicionar um notificacao
	Alterar um notificacao
	Consultar um notificacao
	Deletar um notificacao

Scenario: Controlar notificacaos
	Given Recebendo uma notificacao do processamento de imagem vamos preparar o notificacao
	And Adicionar o notificacao
	And Encontrar o notificacao
	And Alterar o notificacao
	When Consultar o notificacao
	Then posso deletar o notificacao