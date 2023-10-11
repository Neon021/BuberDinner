using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.TestUtils.Constants;

namespace BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;

public class CreateMenuCommandUtils
{
    public static CreateMenuCommand CreateCommand(
        List<CreateMenuSectionCommand>? sections = null) =>
        new(
            Constants.Host.hostId,
            Constants.Menu.Name,
            Constants.Menu.Description,
            sections ?? CreateSectionsCommand());

    public static List<CreateMenuSectionCommand> CreateSectionsCommand(int sectionCount = 1,
        List<CreateMenuItemCommand>? items = null) => 
        Enumerable.Range( 0, sectionCount )
            .Select(i => new CreateMenuSectionCommand(
                Constants.Menu.SectionNameFromIndex(i),
                Constants.Menu.SectionDescriptionFromIndex(i),
                items ?? CreateItemsCommand()))
            .ToList();

    public static List<CreateMenuItemCommand> CreateItemsCommand(int itemCount = 1) =>
       Enumerable.Range(0, itemCount)
           .Select(i => new CreateMenuItemCommand(
               Constants.Menu.ItemNameFromIndex(i),
               Constants.Menu.ItemDescriptionFromIndex(i)
               ))
       .ToList();

}
