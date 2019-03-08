using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class UserViewManager : BaseGlobalReManager<BLL_UserView>, IUserViewManager
    {
        #region Private Variable 

        private IUserViewRepository _UserViewRepository;
        private IUserViewTransformationManager _UserViewTransformationManager;
        private IUserViewScreenRepository _UserViewScreenRepository;

        #endregion

        #region Constructors

        public UserViewManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager
          , IUserViewRepository userViewRepository, IUserViewTransformationManager userViewTransformationManager)
          : base(userManager, cacheStoreManager, logManager)
        {
            _UserViewRepository = ValidateRepository(userViewRepository);
            _UserViewTransformationManager = ValidateManager(userViewTransformationManager);
        }

        public UserViewManager(IUserManager userManager, ICacheStoreManager cacheStoreManager, ILogManager logManager
          , IUserViewRepository userViewRepository, IUserViewTransformationManager userViewTransformationManager, IUserViewScreenRepository userViewScreenRepository)
          : base(userManager, cacheStoreManager, logManager)
        {
            _UserViewRepository = ValidateRepository(userViewRepository);
            _UserViewTransformationManager = ValidateManager(userViewTransformationManager);
            _UserViewScreenRepository = userViewScreenRepository;
        }



        #endregion

        #region Entity Actions

        public Result DeleteUserViewByID(int viewID, BLL_UserViewDelete userviewdelete)
        {
            try
            {
                if (validationForDelete(viewID, userviewdelete) < 0)
                {
                    throw new NotFoundAPIException("Records has not found");
                }
                var userview = _UserViewRepository.Get(d => d.ViewId == viewID);
                if (userview == null)
                { throw new NotFoundAPIException(String.Format("User view does not exists with view Id '{0}'", viewID)); }

                _UserViewRepository.Delete(userview);
                _UserViewRepository.Save(userview);

                if (userviewdelete.DefaultStatus == true)
                {
                    if (userviewdelete.KeyMember == true)
                    {
                        var userview1 = _UserViewRepository.Get(d => d.Screenname == userviewdelete.ScreenName && d.Viewname == viewname1 && d.Userid == UserIdentity.UserId);
                        if (userview1 == null)
                        {
                            throw new NotFoundAPIException("Record not found for the screen name");
                        }
                        OnApplyChangesForStaus(userview1, userviewdelete);
                        _UserViewRepository.Save(userview1);
                    }
                    else
                    {
                        var userview1 = _UserViewRepository.Get(d => d.Screenname == userviewdelete.ScreenName && d.Viewname == viewname2 && d.Userid == UserIdentity.UserId);
                        if (userview1 == null)
                        {
                            throw new NotFoundAPIException("Record not found for the screen name");
                        }
                        OnApplyChangesForStaus(userview1, userviewdelete);
                        _UserViewRepository.Save(userview1);
                    }
                }
                return new Result(new Information("User View", "Successfully Deleted"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EntityAction GetEntityActions(BLL_UserView entity)
        {
            List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };

            return new EntityAction(
               entityType: EntityType.UserViews,
               entityId: entity.ViewId,
               entityActionTypes: entityActionTypes
           );
        }

        public EntityResult<IEnumerable<BLL_UserView>> GetByUserViewSreenName(string screenName)
        {
            if (string.IsNullOrWhiteSpace(screenName))
            {
                throw new NotFoundAPIException("Records not found");
            }
            screenName = screenName.Trim();
            var userview = _UserViewScreenRepository.GetMany(d => d.Screenname == screenName && d.Userid == UserIdentity.UserId);
            if ((userview == null) || (userview.Count == 0))
			{
                throw new NotFoundAPIException("Records not found");
            }
            else
            {
                return new EntityResult<IEnumerable<BLL_UserView>>(_UserViewTransformationManager.Transform(userview));
            }
        }

        public EntityResult<BLL_UserView> GetUserViewByID(int viewID)
        {
            try
            {
                var userview = _UserViewRepository.Get(d => d.ViewId == viewID);

                if (userview == null)
                { throw new NotFoundAPIException(string.Format("User view does not exists with view Id '{0}'", viewID)); }

                return new EntityResult<BLL_UserView>(_UserViewTransformationManager.Transform(userview));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        const string viewname1 = "My Submissions";
        const string viewname2 = "All Submissions";

        /*
        public Result DeleteUserViewByID(int viewID, BLL_UserViewDelete userviewdelete)
        {
            try
            {
               if(validationForDelete(viewID, userviewdelete)<0)
                {
                    throw new NotFoundAPIException("Records has not found");
                }
                var userview = _UserViewRepository.Get(d => d.ViewId == viewID);
                if (userview == null)
                { throw new NotFoundAPIException(string.Format("User view does not exists with view Id '{0}'", viewID)); }

                _UserViewRepository.Delete(userview);
                _UserViewRepository.Save(userview);

                return new Result(new Information("User View", "Successfully Deleted"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
        private int validationForDelete(int viewid,BLL_UserViewDelete blluserview)
        {
            int workflow = -1;

            try
            {
                var userview = _UserViewRepository.Get(d => d.ViewId == viewid);
                if (userview == null)
                { throw new NotFoundAPIException(String.Format("User view does not exists with view Id '{0}'", viewid)); }
                workflow = 1;
                if (blluserview.DefaultStatus == true)
                {
                    if (blluserview.KeyMember == true)
                    {
                        var userview1 = _UserViewRepository.Get(d => d.Screenname == blluserview.ScreenName && d.Viewname == viewname1 && d.Userid == UserIdentity.UserId);
                        if (userview1 == null)
                        {
                            throw new NotFoundAPIException("Record not found for the screen name");
                        }
                        workflow = 2;
                    }
                    else
                    {
                        var userview1 = _UserViewRepository.Get(d => d.Screenname == blluserview.ScreenName && d.Viewname == viewname2 && d.Userid == UserIdentity.UserId);
                        if (userview1 == null)
                        {
                            throw new NotFoundAPIException("Record not found for the screen name");
                        }
                        workflow = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return workflow;
        }

        public EntityResult<IEnumerable<BLL_UserView>> GetUserViews(int userId, string screenName)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        public IPaginatedList<BLL_UserView> Search(SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityResult<BLL_UserView> UpdateUserView(BLL_UserView bLL_UserView)
        {
            try
            {
                var userview = _UserViewRepository.Get(d => d.ViewId == bLL_UserView.ViewId);

                if (userview == null)
                { throw new NotFoundAPIException(string.Format("User view does not exists with view Id '{0}'", bLL_UserView.ViewId)); }

                if (bLL_UserView.Default ?? false) //reset existing then update default column
                {
                    //_UserViewRepository.ResetDefaultOnUserViews(userview.Userid, userview.Screenname, userview.ViewId);
                    _UserViewRepository.FindBy(d => d.Userid == UserIdentity.UserId && d.ViewId != userview.ViewId
                    && d.Screenname == userview.Screenname && d.Default).ToList().ForEach(v => v.Default = false);
                    userview.Default = true;
                }
                else if (!(bLL_UserView.Default ?? true))
                    userview.Default = false;

                OnApplyChanges(userview, bLL_UserView);

                _UserViewRepository.Save(userview);

                return new EntityResult<BLL_UserView>(_UserViewTransformationManager.Transform(_UserViewRepository.Get(d => d.ViewId == bLL_UserView.ViewId)));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityResult<BLL_UserView> AddUserView(BLL_UserView bLL_UserView)
        {
            try
            {
                validateParams(bLL_UserView);

                if (bLL_UserView.CustomView ?? true)
                    bLL_UserView.SortOrder = _UserViewRepository.GetNextSortOrder(UserIdentity.UserId, bLL_UserView.ScreenName);

                grs_TblUserview tbUserView = new grs_TblUserview();
                AssignDefaults(tbUserView, bLL_UserView);

                var userview = _UserViewRepository.Get(d => d.Userid == tbUserView.Userid
                && d.Screenname == tbUserView.Screenname
                && d.Viewname == tbUserView.Viewname);

                if (!(userview == null))
                { throw new IllegalArgumentAPIException(string.Format("User view already exists with combination of screen '{0}' and view '{1}'", tbUserView.Screenname, tbUserView.Viewname)); }

                if (bLL_UserView.Default ?? false) //reset existing then update default column
                {
                    _UserViewRepository.FindBy(d => d.Userid == UserIdentity.UserId
                    && d.Screenname == bLL_UserView.ScreenName && d.Default).ToList().ForEach(v => v.Default = false);
                    tbUserView.Default = true;
                }

                _UserViewRepository.Add(tbUserView);
                _UserViewRepository.Save(tbUserView);

                return new EntityResult<BLL_UserView>(_UserViewTransformationManager.Transform(_UserViewRepository.Get(d => d.ViewId == tbUserView.ViewId)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Private Methods

        private void validateParams(BLL_UserView bLL_UserView)
        {
            if (!(bLL_UserView.CustomView ?? true) && bLL_UserView.SortOrder.IsNullOrEmpty())
                throw new IllegalArgumentAPIException("Sort order is required for system view");
            else if ((bLL_UserView.CustomView ?? true) && !bLL_UserView.SortOrder.IsNullOrEmpty())
                throw new IllegalArgumentAPIException("Sort order is not required for custom view");
            if (bLL_UserView.ScreenName.Length > 100)
                throw new IllegalArgumentAPIException("Screenname length cannot be more than 100 characters");
            if (bLL_UserView.ViewName.Length > 50)
                throw new IllegalArgumentAPIException("Viewname length cannot be more than 50 characters");
        }
        private void AssignDefaults(grs_TblUserview currentView, BLL_UserView newEntity)
        {
            currentView.Userid = UserIdentity.UserId;
            currentView.Screenname = newEntity.ScreenName;
            currentView.Viewname = newEntity.ViewName;
            currentView.Layout = newEntity.Layout;
            currentView.Customview = newEntity.CustomView ?? true;
            currentView.Sortorder = newEntity.SortOrder ?? 1;
        }

        private void OnApplyChanges(grs_TblUserview currentView, BLL_UserView newEntity)
        {
            if (!newEntity.Layout.IsNullOrEmpty())
                currentView.Layout = newEntity.Layout;
        }

        private void OnApplyChangesForStaus(grs_TblUserview currentView, BLL_UserViewDelete newEntity)
        {
                currentView.Default = true;
        }

        #endregion
    }
}

