﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinde.Authorization.Models.User
{
    public interface IUserActionResolver
    {
        public event EventHandler<EventArgs> OnUserActionsCompleted;
        public event EventHandler<EventArgs> OnUserActionsNeeded;
        public Task<string> GetLoginUrl(string state);
        public Task<string> GetCode(string state);
        public Task SetCode(string code, string state);

        public Task SetLoginUrl(string url, string state);
    }
}