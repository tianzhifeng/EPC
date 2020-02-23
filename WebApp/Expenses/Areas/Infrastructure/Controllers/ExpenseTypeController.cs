﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class ExpenseTypeController : InstrasController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_DefineExpenseType";
            }
        }
    }
}