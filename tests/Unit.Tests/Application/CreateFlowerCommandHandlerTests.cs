using System.Linq.Expressions;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Application.Features.Flowers.CreateFlower;
using Domain.FlowerAggregate;
using Moq;

namespace Unit.Tests.Application;

public class CreateFlowerCommandHandlerTests
{
    private const string _flowerName = "Red Rose";
    private const string _flowerType = "Rose";
    private readonly Flower _expectedResult;
    private readonly Mock<IFlowersRepository> _repositoryMock;

    public CreateFlowerCommandHandlerTests()
    {
        _expectedResult = Flower.Create(_flowerName, _flowerType);
        _repositoryMock = new();
    }

    [Fact]
    public async Task Handle_Should_CreateFlower_When_DataIsValid()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Flower>(), CancellationToken.None))
            .ReturnsAsync(_expectedResult);

        var handler = new CreateFlowerCommandHandler(_repositoryMock.Object);

        // Act
        Guid result = await handler.Handle(new CreateFlowerCommand(_flowerName, _flowerType), CancellationToken.None);

        // Assert
        Assert.Equal(_expectedResult.Id, result);
    }

    [Fact]
    public async Task Handle_Should_ThrowDuplicateException_When_FlowerAlreadyExists()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetFlowerBy(It.IsAny<Expression<Func<Flower, bool>>>(), CancellationToken.None))
            .ReturnsAsync(_expectedResult);

        var handler = new CreateFlowerCommandHandler(_repositoryMock.Object);

        // Act
        async Task act() => await handler.Handle(
            new CreateFlowerCommand(_flowerName, _flowerType),
            CancellationToken.None);

        // Assert
        DuplicateException exception = await Assert.ThrowsAsync<DuplicateException>(act);
        Assert.NotNull(exception);
        Assert.IsType<DuplicateException>(exception);
    }
}