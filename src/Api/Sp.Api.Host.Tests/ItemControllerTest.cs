using System.Security.Authentication;
using System.Security.Claims;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sp.Api.Business.Feature.Item.DeleteItem;
using Sp.Api.Business.Feature.Item.GetItems;
using Sp.Api.Business.Feature.Item.UpdateItem;
using Sp.Api.Host.Controllers;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Host.Tests;

[TestClass]
public class ItemControllerTest
{
    private readonly Mock<IMediator> _mediator;

    public ItemControllerTest()
    {
        _mediator = new Mock<IMediator>();
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationException))]
    public async Task ShouldGetAuthError()
    {
        var target = GetTarget();
        var result = await target.Get();
        result.Value.Should().NotBeNull();
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ShouldGetError()
    {
        var target = GetTarget();
        var user = GetUser(true);
        target.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = GetUser(true) }
        };

        var result = await target.GetById("123");
    }

    [TestMethod]
    public async Task ShouldGet()
    {
        var target = GetTarget();
        target.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = GetUser(false) }
        };
        var result = await target.Get();
        var okResult = (OkObjectResult)result.Result!;
        okResult.Value!.GetType().Should().Be(typeof(List<ItemDto>));
    }

    [TestMethod]
    public async Task ShouldDelete()
    {
        var target = GetTarget();
        target.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = GetUser(false) }
        };
        var result = await target.Delete("123");
        var okResult = (OkObjectResult)result.Result!;
        okResult.Value.Should().BeEquivalentTo(string.Empty);
    }

    [TestMethod]
    public async Task ShouldCreate()
    {
        var target = GetTarget();
        target.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = GetUser(false) }
        };
        var result = await target.Create();
        var okResult = (OkObjectResult)result.Result!;
        okResult.Value!.GetType().Should().Be(typeof(ItemDto));
    }

    [TestMethod]
    public async Task ShouldUpdate()
    {
        var target = GetTarget();
        target.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = GetUser(false) }
        };
        var result = await target.Update("123", new ItemEditInputDto());
        var okResult = (OkObjectResult)result.Result!;
        okResult.Value.Should().BeEquivalentTo(string.Empty);
    }

    [TestMethod]
    public async Task ShouldGetById()
    {
        var target = GetTarget();
        target.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = GetUser(false) }
        };
        var result = await target.GetById("123");
        var okResult = (OkObjectResult)result.Result!;
        okResult.Value!.GetType().Should().Be(typeof(ItemDto));
    }

    private ClaimsPrincipal GetUser(bool isBrokenuser)
    {
        string userId;
        if (isBrokenuser)
        {
            userId = "error";
        }
        else
        {
            userId = Guid.NewGuid().ToString();
        }

        return new ClaimsPrincipal(new ClaimsIdentity(
            new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("UserId", userId),
            }));
    }

    private ItemController GetTarget()
    {
        _mediator.Setup(x => x.Send(It.IsAny<GetItemsCommand>(), CancellationToken.None)).ReturnsAsync(new List<ItemDto>());
        _mediator.Setup(x => x.Send(It.IsAny<DeleteItemCommand>(), CancellationToken.None)).ReturnsAsync(string.Empty);
        return new ItemController(_mediator.Object);
    }
}