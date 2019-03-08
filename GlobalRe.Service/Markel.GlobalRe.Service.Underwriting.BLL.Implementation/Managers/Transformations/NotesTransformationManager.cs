using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class NotesTransformationManager : BaseManager, INotesTransformationManager
    {
        #region Constructor

        public NotesTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        public List<BLL_Notes> Transform(IList<grs_VGrsNote> notes)
        {
            //PS: Refactoring
            List<BLL_Notes> data = new List<BLL_Notes>();
            if (notes.Count > 0)
            {
                notes.GroupBy(a => a.Notenum).ToList().ForEach((sGroup) =>
                {
                    var statusGroupData = sGroup.ToList();
                    statusGroupData.ForEach((summary) =>
                    {
                        data.Add(new BLL_Notes() { Notenum = summary.Notenum, Dealnum = summary.Dealnum, Notedate = summary.Notedate, Notes = summary.Notes, Notetype = summary.Notetype, Whoentered = summary.Whoentered, Name = summary.Name, FirstName = summary.FirstName, MiddleName = summary.MiddleName, LastName = summary.LastName, CreatedBy = summary.CreatedBy });
                    });
                });
                return data.OrderByDescending(c => c.Notedate).ToList();
            }
            return null;
        }

        public BLL_Notes Transform(grs_VGrsNote dbModel)
        {
            return new BLL_Notes()
            {
                Dealnum = dbModel.Dealnum,
                Notenum = dbModel.Notenum,
                Notedate = dbModel.Notedate,
                Notes = dbModel.Notes,
                Notetype = dbModel.Notetype,
                Whoentered = dbModel.Whoentered,
                Name=dbModel.Name,
                FirstName=dbModel.FirstName,
                MiddleName=dbModel.MiddleName,
                LastName = dbModel.LastName,
                CreatedBy = dbModel.CreatedBy
            };
        }

    }
}
