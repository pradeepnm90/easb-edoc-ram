using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Enums;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories.Lookups
{
    public class RolePersonsLookupRepository : GenericRepository<ERMSDbContext, TbPerson, int>, IRolePersonsLookupRepository
    {
        public RolePersonsLookupRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public IList<LookupEntity> GetLookupDataByConfig(string configSetting)
        {
            throw new NotImplementedException();
        }

        public IList<LookupEntity> GetLookupData()
        {
            List<string> roles = new List<string>()
            {
                Role.Modeler.EnumDescription(), Role.ModelerManager.EnumDescription(),
                Role.Underwriter.EnumDescription(),Role.UnderwriterManager.EnumDescription(),
                Role.UATA.EnumDescription(),Role.Property_UATA.EnumDescription(),
                Role.Actuary.EnumDescription(), Role.ActuaryManager.EnumDescription()
                ,Role.CAT_Portfolio_Management.EnumDescription()
            };

            var data = from role in Context.CfgRoles
                       join rolePerson in Context.CfgRolePersons on role.RolePk equals rolePerson.RoleFk
                       join person in Context.TbPersons on rolePerson.PersonNameFk equals person.PersonId
                       orderby person.FirstName
                       where roles.Contains(role.Name)
                       select new { person.PersonId, person.FirstName, person.LastName, role.Name };
            var lookupData = data.ToList();

            return lookupData.Select(l => new LookupEntity(
                 id: l.PersonId,
                 code: l.PersonId.ToString(),
                 description: l.FirstName + " " + l.LastName,
                 group: l.Name,
                 isActive: true
             )).ToList();
        }
    }
}
