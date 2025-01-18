Feature: ControlarPagamentos
	Para controlar os pagamentos dos pedidos da lanchonete
	Eu preciso das seguindes funcionalidades
	Receber um pedido pendente de pagamento	
	Consultar o status do pagamento de um pedido
	Receber informacoes do pagamento do pedido via webhook

Scenario: Controlar pagamentos
	Given Preparando o processo de pagamento de um pedido pendente
	And Recebendo um pedido pendente de pagamento
	And Consultar status de pagamento do pedido pendente
	When Receber informacoes do pagamento do pedido via webhook
	Then Consultar status de pagamento do pedido