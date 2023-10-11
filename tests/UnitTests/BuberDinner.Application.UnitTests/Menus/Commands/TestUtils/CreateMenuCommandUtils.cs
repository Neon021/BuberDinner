using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.TestUtils.Constants;

namespace BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;

public class CreateMenuCommandUtils
{
    public static CreateMenuCommand CreateCommand() =>
        new CreateMenuCommand(
            Guid.Parse(Constants.Host.hostId),
            Constants.Menu.Name,
            Constants.Menu.Description,

        );

    public static List<CreateMenuSectionCommand> CreateSectionCommand(int sectionCount) =>
    new CreateMenuCommand(
        Constants.Host.hostId.ToString(),
        Constants.Menu.Name,
        Constants.Menu.Description,


    );

}
