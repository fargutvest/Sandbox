using System;

namespace Application.Models
{
    public class BusinessFlowItemModel
    {
        public string Title { get; set; }
        public Action Invocation { get; set; }
    }
}
