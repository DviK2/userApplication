using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserApplication.Data.Data;
using UserApplication.Data.Repository;
using UserApplication.Data.UnitOfWork;
using UserApplication.ViewModel;

namespace UserApplication.Models
{
    public interface IUserService
    {
        User GetUserById(int id);
        User EditOrCreate(UserViewModel user);
        List<User> GetUsers(TableProperty property);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public User GetUserById(int id)
        {
            var user = _userRepository.Get(id, true);
            return user;
        }

        public User EditOrCreate(UserViewModel user)
        {
            if (user.Id == default(int))
                return InsertUser(user);

            return UpdateUser(user);
        }

        public List<User> GetUsers(TableProperty property)
        {
            var set = _userRepository.GetSet();

            var query = string.IsNullOrWhiteSpace(property.Search)
                ? set
                : set.Where(a => a.Name.Contains(property.Search));

            var count = query.Count();

            SetPaging(property, count);

            var users = query
                .OrderBy(a => a.Id)
                .Skip((property.Page - 1) * property.PageSize)
                .Take(property.PageSize)
                .ToList();

            return users;
        }

        public void Delete(int id)
        {
            _userRepository.Remove(id);
            _unitOfWork.SaveChanges();
        }

        private static void SetPaging(TableProperty property, int count)
        {
            if (property.PageSize < 1)
                property.PageSize = 10;

            if (property.Page < 1)
                property.Page = 1;

            property.Total = count;

            var pageCount = property.Total / property.PageSize + 1;

            property.Page = pageCount < property.Page
                ? pageCount
                : property.Page;
        }

        private User InsertUser(UserViewModel user)
        {
            var newUser = new User();
            UpdateUser(user, newUser);

            _userRepository.Insert(newUser);
            _unitOfWork.SaveChanges();

            return newUser;
        }

        private User UpdateUser(UserViewModel user)
        {
            var editUser = _userRepository.Get(user.Id);
            UpdateUser(user, editUser);

            _unitOfWork.SaveChanges();

            return editUser;
        }

        private static void UpdateUser(UserViewModel user, User editUser)
        {
            editUser.Name = user.Name;
            editUser.Password = user.Password;
            editUser.Email = user.Email;
        }
    }
}