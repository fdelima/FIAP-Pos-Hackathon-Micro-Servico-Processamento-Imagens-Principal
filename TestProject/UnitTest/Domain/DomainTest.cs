using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Messages;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;

namespace TestProject.UnitTest.Domain
{
    public partial class DomainTest
    {

        [Fact]
        public void StringExtensionTest()
        {
            //Arrange
            const string valor = "ToSnakeCaseTest";
            const string expectedResult = "to_snake_case_test";
            //Act
            var result = StringExtension.ToSnakeCase(valor);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void UtiTest()
        {
            //Arrange
            const string valor = "FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities";

            //Act
            var result = Util.GetTypesInNamespace(valor);

            //Assert
            Assert.Contains(typeof(Notificacao), result);
            Assert.Contains(typeof(Pedido), result);
        }

        [Fact]
        public void DuplicatedResultTest()
        {
            //Arrange
            //Act
            var resut = ModelResultFactory.DuplicatedResult<Pedido>();

            //Assert
            Assert.Contains(BusinessMessages.DuplicatedError<Pedido>(), resut.ListErrors());
        }

        [Fact]
        public void NoneResultTest()
        {
            //Arrange
            //Act
            var resut = ModelResultFactory.None();

            //Assert
            Assert.True(resut.ListErrors().Count() == 0);
            Assert.True(resut.ListMessages().Count() == 0);
        }

        [Fact]
        public void MessageResultTest()
        {
            //Arrange
            const string msg = "mensagem";

            //Act
            var resut = ModelResultFactory.Message(msg);

            //Assert
            Assert.Contains(msg, resut.ListMessages());
        }

        [Fact]
        public void ErrorResultTest()
        {
            //Arrange
            const string msg = "erro";

            //Act
            var resut = ModelResultFactory.Error(msg);

            //Assert
            Assert.Contains(msg, resut.ListErrors());
        }
    }
}
