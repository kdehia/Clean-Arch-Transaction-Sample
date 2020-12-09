﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Core.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }
}
