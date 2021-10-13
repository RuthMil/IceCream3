using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream3
{
    public class TagNAme
    {
        public string en { get; set; }
    }

    public class Tag
    {
        public double confidence { get; set; }
        public TagNAme tag { get; set; }
    }

    public class Result
    {
        public List<Tag> tags { get; set; }
    }

    public class Status
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public Result result { get; set; }
        public Status status { get; set; }
    }
}
