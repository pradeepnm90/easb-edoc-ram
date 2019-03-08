using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class Person : BaseApiModel<BLL_Person>
    {
        public Person() { }
        public Person(BLL_Person model) : base(model) { }

        public override BLL_Person ToBLLModel()
        {
            BLL_Person bLL_Person = new BLL_Person() { };
            return bLL_Person;
        }

        protected override bool HasValue()
        {
            //ToDo:
            return true;
        }

        protected override void Initialize(BLL_Person model)
        {
            PersonId = model.PersonId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PopulateDisplayName(model);
        }

        private void PopulateDisplayName(BLL_Person model)
        {
            if (!string.IsNullOrWhiteSpace(model.CommonName))
                DisplayName = model.CommonName;
            else if (string.IsNullOrWhiteSpace(model.CommonName) && !string.IsNullOrWhiteSpace(model.PersonName))
                DisplayName = model.PersonName;
            else if (string.IsNullOrWhiteSpace(model.CommonName) && string.IsNullOrWhiteSpace(model.PersonName))
                DisplayName = model.FirstName + " " + model.LastName;
        }
    }
}