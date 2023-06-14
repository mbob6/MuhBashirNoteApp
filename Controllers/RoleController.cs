using NoteApp.Models.Role;
using NoteApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();

            ViewData["Message"] = roles.Message;
            ViewData["Status"] = roles.Status;

            return View(roles.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRoleViewModel request)
        {
            var response = _roleService.CreateRole(request);

            if (response.Status is false)
            {
                return View(request);
            }

            return RedirectToAction("Index", "Role");
        }

        public IActionResult GetRoleDetail(string id)
        {
            var response = _roleService.GetRole(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Role");
            }

            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _roleService.GetRole(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Role");
            }

            var viewModel = new UpdateRoleViewModel
            {
                Id = response.Data.Id,
                RoleName = response.Data.RoleName,
                Description = response.Data.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateRoleViewModel request)
        {
            var response = _roleService.UpdateRole(id, request);

            if (response.Status is false)
            {
                return View(request);
            }

            return RedirectToAction("Index", "Role");
        }

        [HttpPost]
        public IActionResult DeleteRole([FromRoute] string id)
        {
            var response = _roleService.DeleteRole(id);

            if (response.Status is false)
            {
                return RedirectToAction("Index", "Role"); ;
            }

            return RedirectToAction("Index", "Role");
        }
    }
}
