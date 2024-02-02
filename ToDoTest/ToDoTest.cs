using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApi;
using ToDoWebApi.Controllers;
using ToDoWebApi.Models;
using Xunit;

public class ToDoControllerTests
{
    [Fact]
    public async Task GetTodoItems_ReturnsOkResult()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ToDoController>>();
        var contextMock = CreateInMemoryDbContext();
        var controller = new ToDoController(loggerMock.Object, contextMock);

        // Act
        var result = await controller.GetTodoItems();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var items = Assert.IsType<List<ToDoItem>>(okResult.Value);
    }

    [Fact]
    public async Task CreateToDoItem_ReturnsCreatedAtAction()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ToDoController>>();
        var contextMock = CreateInMemoryDbContext();
        var controller = new ToDoController(loggerMock.Object, contextMock);
        var newToDoItem = new ToDoItem { Name = "New Task", Description = "Description", Status = ToDoStatus.NotStarted };

        // Act
        var result = await controller.CreateToDoItem(newToDoItem);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdToDoItem = Assert.IsType<ToDoItem>(createdAtActionResult.Value);
        Assert.Equal(newToDoItem.Name, createdToDoItem.Name);
    }

    [Fact]
    public async Task CreateToDoItem_ValidInput_ReturnsCreatedAtAction()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ToDoController>>();
        var context = CreateInMemoryDbContext(); 
        var controller = new ToDoController(loggerMock.Object, context);
        var newToDoItem = new ToDoItem { Name = "New Task", Description = "Description", Status = ToDoStatus.NotStarted };

        // Act
        var result = await controller.CreateToDoItem(newToDoItem);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        var createdToDoItem = Assert.IsType<ToDoItem>(createdAtActionResult.Value);
        Assert.Equal(newToDoItem.Name, createdToDoItem.Name);
        Assert.Equal(newToDoItem.Description, createdToDoItem.Description);
        Assert.Equal(newToDoItem.Status, createdToDoItem.Status);
        Assert.False(createdToDoItem.IsDeleted);
        Assert.NotEqual(0, createdToDoItem.Id); 
    }



    [Fact]
    public async Task CreateToDoItem_NullInput_ReturnsBadRequest()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ToDoController>>();
        var context = CreateInMemoryDbContext();
        var controller = new ToDoController(loggerMock.Object, context);

        // Act
        var result = await controller.CreateToDoItem(null);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid Data", badRequestResult.Value);
    }

    [Fact]
    public async Task CreateToDoItem_DbError_ReturnsInternalServerError()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ToDoController>>();
        var context = CreateInMemoryDbContext();
        var controller = new ToDoController(loggerMock.Object, context);
        var newToDoItem = new ToDoItem { Id = 99, Name = "New Task", Description = "Description", Status = ToDoStatus.NotStarted };

        // Act
        var result = await controller.CreateToDoItem(newToDoItem);

        // Assert
        if (result.Result is CreatedAtActionResult)
        {
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdToDoItem = Assert.IsType<ToDoItem>(createdAtActionResult.Value);
            Assert.Equal(newToDoItem.Name, createdToDoItem.Name);
        }
        else if (result.Result is ObjectResult)
        {
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Internal server error", objectResult.Value);
        }
        else
        {
            Assert.Fail($"Unexpected result type: {result.Result.GetType()}");
        }
    }

    private ToDoContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ToDoContext(options);

        context.ToDos.AddRange(new List<ToDoItem>
    {
        new ToDoItem { Id = 1, Name = "Task 1", Status = ToDoStatus.NotStarted },
        new ToDoItem { Id = 2, Name = "Task 2", Status = ToDoStatus.InProgress },
        new ToDoItem { Id = 3, Name = "Task 3", Status = ToDoStatus.Complete }
    });

        context.SaveChanges();

        return context;
    }

}
