﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Model.Entities
{
    public class Category : TimeStamp
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}
