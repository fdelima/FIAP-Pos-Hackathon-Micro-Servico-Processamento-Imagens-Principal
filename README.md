# Fase 5 da Fiap Pos Tech - Hackathon - Processamento de Imagens
## Microserviço : FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal

### Demanda
Vocês foram contratados pela empresa FIAP X que precisa avançar no desenvolvimento de um projeto de processamento de imagens. Em uma rodada de investimentos, a empresa apresentou um
projeto simples que processa um vídeo e retorna as imagens dele em um arquivo .zip.
Os investidores gostaram tanto do projeto, que querem investir em uma versão onde eles possam enviar um vídeo e fazer download deste zip.

### Premissas adotas pelo grupo

* Upload de vídeos de até 500MB.
* Geração de arquivo .zip com até 10 imagens extraídas do vídeo.
* Exclusão do vídeo do servidor após a geração do arquivo .zip, garantindo a privacidade dos dados.
* Preservação dos dados do usuário, o arquivo .zip será criado com informação anonimizada.
* O arquivo .zip poderá conter no máximo 10 imagens.
* Escalável

### Objetivo

Atender à demanda dos investidores por uma solução mais completa, que permita o envio de vídeos e o download de imagens processadas, expandindo as funcionalidades do projeto existente.

# Entregáveis:
 - Documentação da arquitetura proposta para o projeto;
   > *  ![Arquitetura proposta](/Documentacao/FIAP-Pos-Tech-Hackathon-Arquitetura.drawio.svg)
>
 - Script de criação do banco de dados ou de outros recursos utilizados;
    > *  ![Code First](/Documentacao/Banco_de_Dados_Code_First_Ef.png)
        - Não foi necessário geração de script de banco de dados, pois, foi utilizado "Code First" com tecnica de desenvolvimento. O banco de dados e criado ou alterado de acordo com as entidade mapedas no Entity Framework na camada de Infra. [Mais detalhes sobre code first](https://learn.microsoft.com/pt-br/ef/ef6/modeling/code-first/workflows/new-database)
 >
 - Link do Github do(s) projeto(s);
    > *  [Microserviço Principal](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal)
    > *  [Microserviço de produção](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao)
>
 - Vídeo de nomáximo 10 minutos apresentando: Documentação, Arquitetura escolhida e o projeto funcionando.
    > *  [Video Yutube]()