﻿using Domain.Ecommerce.Enum;

namespace Domain.Ecommerce.Model
{
    public class Base
    {
        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; }
        public Status Status { get; set; }
    }
}
