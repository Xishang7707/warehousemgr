using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.In;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace warehousemgr.Api
{
    public class BaseController : Controller
    {
        protected In Package()
        {
            return new In { };
        }

        protected In<T> Package<T>(T data)
        {
            return new In<T>
            {
                data = data
            };
        }
    }
}
