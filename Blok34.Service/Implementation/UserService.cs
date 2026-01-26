using Blok34.Domain.Identity;
using Blok34.Repository.Interface;
using Blok34.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blok34.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApplicationUser? GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return null;

            return user;
        }
    }
}
