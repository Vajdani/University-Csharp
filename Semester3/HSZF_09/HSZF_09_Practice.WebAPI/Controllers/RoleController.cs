using HSZF_09_Practice.BusinessLogic.Interfaces;
using HSZF_09_Practice.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HSZF_09_Practice.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        readonly IRoleService roleService;

        public RoleController(IRoleService roleService) // DI with IoC
        {
            this.roleService = roleService;
        }

        // GET: /<RoleController>/List
        [HttpGet]
        public IEnumerable<RoleListingDTO> List()
        {
            return roleService.GetAllForListing();
        }

        // GET /<RoleController>/Get/5
        [HttpGet("{id}")]
        public RoleCommonDataDTO? Get(int id)
        {
            return roleService.GetCommonDataById(id);
        }

        // POST /<RoleController>/Create
        [HttpPost]
        public void Create([FromBody] RoleCreateDTO role)
        {
            var model = RoleEditDTO.CreateFrom(role); // This conversion helps to keep clean the API
            roleService.Create(model);
        }

        // PUT /<RoleController>/Update
        [HttpPut]
        public void Update([FromBody] RoleEditDTO role)
        {
            roleService.UpdateRole(role);
        }

        // DELETE /<RoleController>/Delete/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            roleService.Delete(id);
        }

        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<RoleListingDTO> GetRolesWithActorsAndMovieTitles()
        {
            return roleService.GetRolesByActors();
        }
    }
}
