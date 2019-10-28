using ImportLeague.Core.Entities;
using ImportLeague.Core.Interfaces.Repository;
using ImportLeague.Core.Interfaces.Services;
using ImportLeague.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ImportLeague.Core.Tests
{
    public class ImportLeagueTests
    {
        private Mock<ICompetitionExternalService> _competitionExternalServiceMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Competition>> _competitionRepository;
        private Mock<ILogger<CompetitionService>> _loggerMock;
        private Mock<IPlayerRepository> _playerRepository;

        private ICompetitionService _target;
        public ImportLeagueTests()
        {
            _competitionExternalServiceMock = new Mock<ICompetitionExternalService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<CompetitionService>>();
            _competitionRepository = new Mock<IRepository<Competition>>();
            _playerRepository = new Mock<IPlayerRepository>();
            _unitOfWorkMock.SetupGet(x => x.Competitions).Returns(_competitionRepository.Object);
            _unitOfWorkMock.SetupGet(x => x.Players).Returns(_playerRepository.Object);
            _target = new CompetitionService(_competitionExternalServiceMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Test_AlreadyImported_ThrowArgumentNullException()
        {
            //Arrange
            _competitionRepository.Setup(
                x => x.Count(It.IsAny<Expression<Func<Competition, bool>>>()))
                .ReturnsAsync(1);


            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _target.AlreadyImported(null));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _target.AlreadyImported(""));
        }

        [Fact]
        public async Task Test_AlreadyImported_ReturnTrue()
        {
            //Arrange

            Expression<Func<Competition, bool>> testExpression = binding => (binding.Code == "CL");
            _competitionRepository.Setup(
                x => x.Count(It.Is<Expression<Func<Competition, bool>>>(criteria => Neleus.LambdaCompare.Lambda.Eq(criteria, testExpression))))
                .ReturnsAsync(1);

            //Act
            var result = await _target.AlreadyImported("CL");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Test_AlreadyImported_ReturnFalse()
        {
            //Arrange

            Expression<Func<Competition, bool>> testExpression = binding => (binding.Code == "CL");
            _competitionRepository.Setup(
                x => x.Count(It.Is<Expression<Func<Competition, bool>>>(criteria => Neleus.LambdaCompare.Lambda.Eq(criteria, testExpression))))
                .ReturnsAsync(1);

            //Act
            var result = await _target.AlreadyImported("EL");

            //Assert
            Assert.False(result);
        }




    }
}
