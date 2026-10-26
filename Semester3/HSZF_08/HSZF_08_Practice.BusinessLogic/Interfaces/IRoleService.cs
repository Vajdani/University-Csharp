using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        void Create(Role role);
        void Delete(int id);
        List<Role> GetAll();
        Role? GetById(int id);
        void UpdateRole(Role role);

        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        List<RoleDisplay> GetRolesByActors();
        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        List<RoleDisplay> GetRolesByActorsWithEagerLoad();
    }
}