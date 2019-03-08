using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;


namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_UserView : BaseGlobalReBusinessEntity
    {
        public int ViewId { get; set; }
        public int UserId { get; set; }
        public string ScreenName { get; set; }
        public string ViewName { get; set; }
        public bool ? Default { get; set; }
        public string Layout { get; set; }
        public DateTime UserViewCreationDate { get; set; }
        public bool? CustomView { get; set; }
        public int? SortOrder { get; set; }
    }
}

