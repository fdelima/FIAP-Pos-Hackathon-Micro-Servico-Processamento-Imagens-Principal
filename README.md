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

### Requisitos técnicos:
 - #### O sistema deve persistir os dados;
    > * Utilizamos NoSql em nosso microserviço de processamento de imagem principal.
>
 - #### O sistema deve estar em uma arquitetura que o permita ser escalado;
     > * Utilizamos kubernetes.
>
 - #### O projeto deve ser versionado no Github;
     > *  [Microserviço Principal](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal)
    > *  [Microserviço de produção](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao)
>
 - #### O projeto deve ter testes que garantam a sua qualidade;
    > * **Teste realizados**
    ![Teste realizados](/Documentacao/principal-tests.png)     
    > Realizado teste de componente em BDD.  
    > Realizado teste de integração.  
    > Realizado teste unitários.  
    >    
    > * **Code coverage**
    > * - Microserviço Principal
    ![Microserviço Principal code coverage 81%](/Documentacao/code-coverage-principal.png)  
    [Xunit Code Coverage :: Veja aqui mais detalhes](https://html-preview.github.io/?url=https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal/blob/develop/TestProject/CodeCoverage/Report/index.html)
    > * - Microserviço Producao
    ![Microserviço Produção code coverage 94%](/Documentacao/code-coverage-producao.png)  
    [Xunit Code Coverage :: Veja aqui mais detalhes](https://html-preview.github.io/?url=https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao/blob/develop/TestProject/CodeCoverage/Report/index.htm)    
    >    
    > * **DAST vulnerability test**  
    ![DAST Vulnerability](/Documentacao/ZAP-DAST-Principal.png)  
    > [DAST Vulnerability :: Veja aqui mais detalhes](https://html-preview.github.io/?url=https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal/blob/develop/TestProject/ZAP-DAST/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal-2025-02-09-ZAP-Report-.html)
 >
 - CI/CD da aplicacao
    - Microserviço processamento de imagem principal
        > * [Pipeline workflow](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal/actions/runs/13229399793/workflow)
        > * [Pipeline actions](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal/actions)
        > * [Pipeline execução](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal/actions/runs/13229399793/job/36924433052)
    
    - Microserviço processamento de imagem produção    
        > * [Pipeline workflow](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao/actions/runs/13215807809/workflow)
        > * [Pipeline actions](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao/actions)
        > * [Pipeline execução](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao/actions/runs/13232879274/job/36932786224)  

# Entregáveis:

 - Documentação da arquitetura proposta para o projeto;
    > * **Event Storming**
    > * - Braninstorming
    ![Braninstorming](/Documentacao/event-storming-brainstorming.png)     
    > * - Fluxo de fucionamento
    ![Fluxo de fucionamento](/Documentacao/event-storming-fluxo.png)     
    > * - Fluxo de fucionamento agregado
    ![Fluxo de fucionamento agregado](/Documentacao/event-storming-fluxo-agregado.png)     
    > * [Event storming :: Veja aqui mais detalhes](https://miro.com/app/board/uXjVLh9nMww=/)  
    
    > * **Diagrama Arquitetural**
   > *  ![Arquitetura proposta](/Documentacao/FIAP-Pos-Tech-Hackathon-Arquitetura.drawio.svg)
>
 - Script de criação do banco de dados ou de outros recursos utilizados;
    > *  ![Code First](/Documentacao/Banco_de_Dados_Code_First_Ef.png)
        - Não foi necessário geração de script de banco de dados, pois, foi utilizado "Code First" com tecnica de desenvolvimento. O banco de dados e criado ou alterado de acordo com as entidade mapedas no Entity Framework na camada de Infra. [Mais detalhes sobre code first](https://learn.microsoft.com/pt-br/ef/ef6/modeling/code-first/workflows/new-database)
 >
 - Link do Github dos projetos;
    > *  [Microserviço Principal](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Principal)
    > *  [Microserviço de produção](https://github.com/fdelima/FIAP-Pos-Hackathon-Micro-Servico-Processamento-Imagens-Producao)
>
 - Vídeo de nomáximo 10 minutos apresentando: Documentação, Arquitetura escolhida e o projeto funcionando.
    > *  [Video Yutube]()