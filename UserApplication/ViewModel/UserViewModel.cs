using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using UserApplication.Data.Data;

namespace UserApplication.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            if (user == null)
                return;

            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}