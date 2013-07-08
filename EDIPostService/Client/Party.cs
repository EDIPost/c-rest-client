﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client
{
    public class Party
    {
        public int id { get; set; }

        public String companyName { get; set; }

        public String customerNumber { get; set; }

        public String country { get; set; }

        public Address postAddress { get; set; }

        public Address streetAddress { get; set; }

        public Contact contact { get; set; }
    }
}
