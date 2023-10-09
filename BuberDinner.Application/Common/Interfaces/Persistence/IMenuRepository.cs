using BuberDinner.Domain.Menus;

namespace BuberDinner.Application.Common.Interfaces.Persistence
{
    public interface IMenuRepository
    {
        void Add(Menu menu);
    }
}
