using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class CheckListTree : BaseApiModel<BLL_CheckListTree>
    {

        public CheckListTree() { }

        public CheckListTree(BLL_CheckListTree model) : base(model) { }

        public override BLL_CheckListTree ToBLLModel()
        {
            BLL_CheckListTree BLL_CheckListTree = new BLL_CheckListTree() { };
            return BLL_CheckListTree;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_CheckListTree model)
        {
            ChkListNum = model.ChkListNum;
            ChkListName = model.ChkListName;
            SortOrder = model.SortOrder;
            ReadOnly = model.ReadOnly;
            Checked = model.Checked;
            PersonId = model.PersonId;
            PersonName = model.PersonName;
            CheckedDateTime = model.CheckedDateTime;
            Note = model.Note;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
        }
    }
}
