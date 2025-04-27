using Moq;
using Bogus;
using Microsoft.Extensions.Logging;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Commons.Testes.Base
{
    public class BaseTest
    {
        protected Faker _faker;
        protected Mock<IUnitOfWork> _unitOfWorkMock;
        protected IHandler<DomainNotification> _notifications;
        protected Mock<ILogger<DomainNotificationHandler>> _logger;

        public BaseTest()
        {
            _faker = new Faker();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<DomainNotificationHandler>>();
            _notifications = new DomainNotificationHandler(_logger.Object);
        }
    }
}