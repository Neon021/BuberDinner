using BuberDinner.Application.Common.Interfaces.Persistance;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
using BuberDinner.Application.UnitTests.TestUtils.Menus.Extensions;
using FluentAssertions;
using Moq;
using System.Runtime.CompilerServices;
using Xunit;

namespace BuberDinner.Application.UnitTests.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandlerTests
{
    private readonly CreateMenuCommandHandler _handler;
    private readonly Mock<IMenuRepository> _mockMenurepository;

    public CreateMenuCommandHandlerTests(CreateMenuCommandHandler handler, Mock<IMenuRepository> mockMenurepository)
    {
        _handler = handler;
        _mockMenurepository = mockMenurepository;
    }

    // T1: SUT - logical component we're testing 
    // T2: Scenario - what we're testing
    // T3: Expected outcome - what we expect the logical component to do
    //public void T1_T2_T3

    [Theory]
    [MemberData(nameof(ValidateCreateMenuCommands))]
    public async Task HandleCreateMenuCommand_WhenMenuIsValid_ShouldCreateAndReturnMenu(CreateMenuCommand createMenuCommand)
    {
        //Act
        var result = await _handler.Handle(createMenuCommand, default);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.ValidateCreatedFrom(createMenuCommand);
        _mockMenurepository.Verify(m => m.Add(result.Value), Times.Once);
    }

    public static IEnumerable<object[]> ValidateCreateMenuCommands()
    {
        yield return new[] { CreateMenuCommandUtils.CreateCommand() };

        yield return new[]
        {
            CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateSectionsCommand(sectionCount: 3)), 
        };

        yield return new[]
        {
            CreateMenuCommandUtils.CreateCommand(
                sections: CreateMenuCommandUtils.CreateSectionsCommand(
                    sectionCount: 3,
                    items: CreateMenuCommandUtils.CreateItemsCommand(itemCount: 3))),
        };
    }
}
