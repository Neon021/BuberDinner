using BuberDinner.Domain.Hosts.ValueObjects;
using BuberDinner.Domain.Menus;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public record CreateMenuCommand(
    HostId HostId,
    string Name,
    string Description,
    List<CreateMenuSectionCommand> Sections) : IRequest<ErrorOr<Menu>>;

public record CreateMenuSectionCommand(
    string Name,
    string Description,
    List<CreateMenuItemCommand> Items);

public record CreateMenuItemCommand(
    string Name,
    string Description);