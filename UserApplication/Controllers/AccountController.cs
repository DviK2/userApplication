using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserApplication.Models;
using UserApplication.ViewModel;

namespace UserApplication.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IHttpActionResult User(int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(new UserViewModel(user));
        }

        [HttpPost]
        public IHttpActionResult User(UserViewModel user)
        {
            var editedUser = _userService.EditOrCreate(user);
            return Ok(new UserViewModel(editedUser));
        }

        [HttpPost]
        public IHttpActionResult DeleteUser(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetUsers(string search = null, int page = 1 , int pageSize = 10)
        {
            var property = new TableProperty {Page = page, Search = search, PageSize = pageSize};
            var users = _userService.GetUsers(property);
            var viewUsers = users.Select(a => new UserViewModel(a));
            return Ok(new {paging = property, users = viewUsers});
        }
    }
}
