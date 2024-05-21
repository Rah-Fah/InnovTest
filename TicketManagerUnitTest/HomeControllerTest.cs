using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManager.Controllers;
using TicketManager.Models;
using TicketManager.Services;

namespace TicketManagerUnitTest
{
    [TestFixture]
    public class HomeControllerTest
    {

        private HomeController _controller;
        private IHttpContextAccessor _httpContextAccessor;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessor = new HttpContextAccessor();
            _controller = new HomeController(_httpContextAccessor);
        }
        [Test]
        public void TestLogin() {
            var user = new User() { Email = "test@gmail.com", UserName = "testName", IsAdmin = true, IdUser = 1 };
            var result = _controller.Login(user);
            Assert.IsInstanceOf<ViewResult>(result);
        }


        [Test]
        public void Login_ReturnsView()
        {
            var result = _controller.Login();
            Assert.IsInstanceOf<ViewResult>(result);
        }

       

    }
}
