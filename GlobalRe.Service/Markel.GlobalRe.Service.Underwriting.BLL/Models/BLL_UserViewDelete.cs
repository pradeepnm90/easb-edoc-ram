using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;


namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_UserViewDelete : BaseGlobalReBusinessEntity
    {
        public string ScreenName { get; set; }
        public bool DefaultStatus { get; set; }
        public bool KeyMember { get; set; }
    }
}

