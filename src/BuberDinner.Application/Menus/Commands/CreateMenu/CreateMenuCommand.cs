using BuberDinner.Domain.Hosts.ValueObjects;
using BuberDinner.Domain.Menus;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public record CreateMenuCommand(
    HostId HostId,
    string Name,
    string Description,
    List<MenuSectionCommand> Sections) : IRequest<ErrorOr<Menu>>;

public record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items);

public record MenuItemCommand(
    string Name,
    string Description);