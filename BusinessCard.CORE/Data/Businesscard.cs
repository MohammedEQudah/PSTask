﻿using System;
using System.Collections.Generic;

namespace BusinessCard.CORE.Data
{
    public partial class Businesscard
    {
        public decimal Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Phone { get; set; }
        public string? Imagepath { get; set; }
        public string? Address { get; set; }
    }
}
