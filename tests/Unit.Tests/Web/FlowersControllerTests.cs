using Application.Features.Flowers.CreateFlower;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Unit.Tests.Web;

public class FlowersControllerTests
{
    private const string _flowerName = "Red Rose";
    private const string _flowerType = "Rose";

    [Fact]
    public async Task Post_Should_ReturnCreatedResponse_When_FlowerIsCreatedSuccessfully()
    {
        // Arrange
        CreateFlowerCommand command = new(_flowerName, _flowerType);
        var mediatorMock = new Mock<ISender>();
        var flowerGuid = Guid.NewGuid();

        mediatorMock.Setup(x => 
            x.Send(It.IsAny<CreateFlowerCommand>(), CancellationToken.None))
            .ReturnsAsync(flowerGuid);

        var controller = new FlowersController(mediatorMock.Object);

        // Act
        var response = await controller.Post(command);

        //Assert
        Assert.NotNull(response);
        Assert.IsType<CreatedResult>(response);
        Assert.Equal(flowerGuid, ((CreatedResult)response).Value);
    }
}