using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    [Serializable]
    [DataContract]
	[ExcludeFromCodeCoverage]
	public abstract class BaseGlobalReBusinessEntity : BaseChangeTrackingEntity
	{
		#region Constructors

		public BaseGlobalReBusinessEntity() : base () { }

        #endregion Constructors

        #region Public Properties

        public BaseGlobalReBusinessEntity Parent { get; protected set; }

        public virtual bool IsValid { get; set; }

        public virtual bool IsSaved { get; set; }

        #endregion Public Properties

        #region Internal Methods

        internal virtual void SetParent(BaseGlobalReBusinessEntity parent = null)
        {
            Parent = parent;
        }

		#endregion Internal Methods

		#region Public Methods

		public virtual T GetParent<T>() where T : BaseGlobalReBusinessEntity
		{
            return Parent as T;
        }

        #endregion Public Methods
    }
}
