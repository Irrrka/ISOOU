﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISOOU.Services.Data.Contracts;
using ISOOU.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace ISOOU.Web.Controllers
{
    public class DistrictsController : BaseController
    {
        private readonly IDistrictsService districtsServices;

        public DistrictsController(IDistrictsService districtsServices)
        {
            this.districtsServices = districtsServices;
        }

        public IActionResult All()
        {
           var districts = this.districtsServices.GetAllDistricts();

           return this.View(districts);
        }
    }
}