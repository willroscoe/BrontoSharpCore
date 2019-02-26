using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bronto.API;

namespace Bronto.API.Tests
{
    public abstract class BrontoBaseTestWithLogin : BrontoBaseTestClass
    {
        LoginSession _login = null;

        public LoginSession Login
        {
            get { return _login ?? (_login = Bronto.API.LoginSession.CreateAsync(ApiToken).Result); }
            set { _login = value; }
        }
    }
}
