﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Contracts.IRepositories
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        public Task<UserModel> GetUserByEmail(string email);

        public Task<UserModel> GetUserByPhone(string phone);

    }
}
