using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YoYo.Core.ViewModel
{
    public class HttpResponseErrorModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Path { get; set; }
        public string StackTrace { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
