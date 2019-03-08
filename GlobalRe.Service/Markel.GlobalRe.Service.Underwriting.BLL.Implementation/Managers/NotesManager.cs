using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class NotesManager : BaseGlobalReManager<BLL_Notes>, INotesManager
    {
        #region Private Variable 


            private INotesRepository _notesRepository;
            private INotesTransformationManager _notesTransformationManager;
            private ITbDealNotesRepository _tbDealNotesRepository;

      

        private enum FilterParameters
            {
                dealnumber
            }
        #endregion

        #region Constructors

                  public NotesManager(IUserManager userManager,
                                ICacheStoreManager cacheStoreManager,
                                ILogManager logManager,
                                INotesRepository dealNotesRepository,
                                INotesTransformationManager notesTransformationManager,
                                ITbDealNotesRepository tbDealNotesRepository)
                : base(userManager, cacheStoreManager, logManager)
            {
                _notesRepository = ValidateRepository(dealNotesRepository);
                _notesTransformationManager = ValidateManager(notesTransformationManager);
            _tbDealNotesRepository = ValidateRepository(tbDealNotesRepository);
        }

        #endregion

        #region Entity Actions
        public EntityAction GetEntityActions(BLL_Notes entity)
            {
                List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
              //  entityActionTypes.Add(EntityActionType.Action_Update);

                return new EntityAction(
                   entityType: EntityType.Notes,
                   entityId: entity.Notenum,
                   entityActionTypes: entityActionTypes
               );
            }


        //    public IList<BLL_DealNotes> GetNotes(int dealNumber)
        //    {
        //    if(dealNumber<1)
        //    {
        //        throw new NotFoundAPIException("DealNotes not found");
        //    }
        //    var data = _notesRepository.GetMany(FilterDealNumber(dealNumber)); /// method need to change
        //        if (data == null) throw new NotFoundAPIException("DealNotes not found");
        //    //return new EntityResult<BLL_DealNotes>(_notesTransformationManager.Transform(data));
        //    return _notesTransformationManager.Transform(data);
        //}


        public EntityResult<IEnumerable<BLL_Notes>> GetNotes(int dealNumber)
        {
            if (dealNumber < 1)
            {
                throw new NotFoundAPIException("Records not found");
            }
            var dealNotesdata = _notesRepository.GetNotes(dealNumber);
            if (dealNotesdata == null)
            {
                throw new NotFoundAPIException("Records not found");
            }
            else if (dealNotesdata.Count == 0)
            {
                throw new NotFoundAPIException("Records not found");
            }
            else
            {
                return new EntityResult<IEnumerable<BLL_Notes>>(_notesTransformationManager.Transform(dealNotesdata));
            }
          //  return new EntityResult<IEnumerable<BLL_DealNotes>>(_notesTransformationManager.Transform(_notesRepository.GetNotes(dealNumber)));
        }


        public EntityResult<IEnumerable<BLL_Notes>> GetNotebyNoteNumber(int noteNumber)
        {
            if(noteNumber<1)
            {
                throw new NotFoundAPIException("Records not found");
            }
            var notesdata = _notesRepository.GetNotebyNoteNumber(noteNumber);
            if (notesdata == null)
            {
                throw new NotFoundAPIException("Records not found");
            }
            else if (notesdata.Count == 0)
            {
                throw new NotFoundAPIException("Records not found");
            }
            else
            {
                return new EntityResult<IEnumerable<BLL_Notes>>(_notesTransformationManager.Transform(notesdata));
            }
        }

        #endregion

        #region Private Methods

        //private EntityResult<IPaginatedList<BLL_DealNotes>> Transform<T>(IPaginatedList<T> dbResults) where T : class
        //{
        //    var results = new PaginatedList<BLL_DealNotes>()
        //    {
        //        PageCount = dbResults.PageCount,
        //        PageIndex = dbResults.PageIndex,
        //        PageSize = dbResults.PageSize,
        //        TotalRecordCount = dbResults.TotalRecordCount,
        //        Items = dbResults.Items.Select(s => s is grs_VGrsNote ? _notesTransformationManager.Transform(s as grs_VGrsNote) : _notesTransformationManager.Transform(s as grs_VGrsNote)).ToList()
        //    };

        //    return new EntityResult<IPaginatedList<BLL_DealNotes>>(results);
        //}

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VGrsNote, bool>> FilterDealNumber(int dealNumber)
            {
                return s => s.Dealnum == dealNumber;
            }
       
            public IPaginatedList<BLL_Notes> Search(SearchCriteria criteria)
            {
            throw new NotImplementedException();
        }

        private void AssignDefaults(TbDealnote currentNote, BLL_Notes changedEntity, int notenum = -1)
        {
            currentNote.Notenum = notenum;
            currentNote.Dealnum = changedEntity.Dealnum;
            currentNote.Notedate = changedEntity.Notedate ?? DateTime.Now;
            currentNote.Dateentered = currentNote.Notedate;
            currentNote.Notetype = changedEntity.Notetype;
            currentNote.Notes = changedEntity.Notes;
            currentNote.Whoentered = changedEntity.Whoentered ?? UserIdentity.NameId;
        }

        private void ValidateSearchCriteria(bool globalReData, SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            if (globalReData)
            {
                validFilterParameters = _notesRepository.GetFilterParameters();
                validSortParameters = _notesRepository.GetSortParameters();
            }
            //else
            //{
            //    validFilterParameters = _notesRepository.GetFilterParameters();
            //    validSortParameters = _notesRepository.GetSortParameters();
            //}
            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }

        public EntityResult<BLL_Notes> AddDealNotes(BLL_Notes bLL_DealNotes)
        {
            try
            {
                TbDealnote tbDealNotes = new TbDealnote();
                var maxNoteNum = _tbDealNotesRepository.GetNextNoteNumber();
                AssignDefaults(tbDealNotes, bLL_DealNotes, maxNoteNum);
                _tbDealNotesRepository.Add(tbDealNotes);
                _tbDealNotesRepository.Save(tbDealNotes);
                return new EntityResult<BLL_Notes>(_notesTransformationManager.Transform(_notesRepository.GetNotebyNoteNumber(maxNoteNum).SingleOrDefault()));
                //return new EntityResult<BLL_Notes>(_notesTransformationManager.Transform(_tbDealNotesRepository.Get(d => d.Dealnum == tbDealNotes.Dealnum && d.Notenum == tbDealNotes.Notenum)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityResult<BLL_Notes> UpdateDealNotes(BLL_Notes bLL_DealNotes)
        {
            try
            {
                var tbDealNotes = _tbDealNotesRepository.Get(d => d.Notenum == bLL_DealNotes.Notenum);

                if (tbDealNotes == null) { throw new NotFoundAPIException($"Deal Note '{bLL_DealNotes.Notenum}' is not available in database."); }

                OnApplyChanges(tbDealNotes, bLL_DealNotes);
                _tbDealNotesRepository.Save(tbDealNotes);

                return new EntityResult<BLL_Notes>(_notesTransformationManager.Transform(_notesRepository.GetNotebyNoteNumber(bLL_DealNotes.Notenum).SingleOrDefault()));

                // return new EntityResult<BLL_Notes>(_notesTransformationManager.Transform(_tbDealNotesRepository.Get(d => d.Notenum == bLL_DealNotes.Notenum)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void OnApplyChanges(TbDealnote tbDealNotes, BLL_Notes bLL_DealNotes)
        {
            if (bLL_DealNotes.Notetype != null)
                tbDealNotes.Notetype = bLL_DealNotes.Notetype;
            tbDealNotes.Notes = bLL_DealNotes.Notes;
            if (bLL_DealNotes.Whoentered != null)
                tbDealNotes.Whoentered = bLL_DealNotes.Whoentered;
        }

        #endregion
    }
}