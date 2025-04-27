using Moq;
using System.Text.Json;
using Produtos.Commons.Testes.Base;
using Produtos.Application.Requests;
using Produtos.Commons.Testes.Factory;
using Produtos.Commons.Testes.Extensions;
using Produtos.Domain.Interfaces.Services;
using Produtos.Application.UseCases.Produtos;

namespace Produtos.Unit.Tests.Application.UseCases.Produtos
{
    public class BuscarProdutoPorIdUseCaseTest : BaseTest
    {
        private readonly Mock<IProdutoService> _domainService;

        public BuscarProdutoPorIdUseCaseTest()
        {
            _domainService = new Mock<IProdutoService>();
        }

        private BuscarProdutoPorIdUseCase InitializeUseCase()
            => new(_domainService.Object, _notifications, _unitOfWorkMock.Object);


        [Fact]
        public async Task BuscarProdutoPorId()
        {
            var entity = DomainFactory.GetProduto;
            _domainService.SetupGet(x => x.GetAllQueryAsNoTracking)
                .Returns(EFCoreExtensions.GetDbSet(entity.ToQueryable()).Object);

            var useCsae = InitializeUseCase();

            var result = await useCsae.Handle(new BuscarProdutoPorIdRequest { Id = entity.Id }, default);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Nome, result.Nome);
            Assert.Equal(entity.Valor, entity.Valor);
            Assert.False(_notifications.HasNotifications());
            _domainService.Verify(x => x.GetAllQueryAsNoTracking, Times.Once);
        }
    }
}
