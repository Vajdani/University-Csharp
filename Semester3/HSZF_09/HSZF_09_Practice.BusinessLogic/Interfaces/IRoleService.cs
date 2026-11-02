using HSZF_09_Practice.BusinessLogic.Models;
using HSZF_09_Practice.Shared.DTOs;
using HSZF_09_Practice.Shared.Entities;

namespace HSZF_09_Practice.BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        void Create(RoleEditDTO role);
        void Delete(int id);
        List<Role> GetAll();
        Role? GetById(int id);
        void UpdateRole(RoleEditDTO role);

        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        List<RoleListingDTO> GetRolesByActors();
        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        List<RoleListingDTO> GetRolesByActorsWithEagerLoad();
        List<RoleListingDTO> GetAllForListing();
        RoleCommonDataDTO? GetCommonDataById(int id);
    }
}