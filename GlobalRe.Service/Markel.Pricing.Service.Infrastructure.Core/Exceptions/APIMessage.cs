using Markel.Pricing.Service.Infrastructure.Extensions;
using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    /// <summary>
    /// API Message
    /// </summary>
    [Serializable]
    [DataContract]
    public class APIMessage
    {
        #region Properties

        [XmlAttribute]
        [DataMember]
        public string field { get; private set; }

        [XmlAttribute]
        [DataMember]
        public string detail { get; private set; }

        #endregion Properties

        #region Constructors

        public APIMessage(string field, string detail)
        {
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(detail))
                throw new NullReferenceException("Field and Detail are both required parameters for API Message!");

            this.field = field.LowerCaseFirstCharacter();
            this.detail = detail;
        }

        public APIMessage(Expression<Func<object>> field, string detail) : this(TypeExtension.PropertyName(field), detail) { }
        [Obsolete("Confusing")]
        public APIMessage(Expression<Func<object>> field, string detail, params object[] args) : this(TypeExtension.PropertyName(field), string.Format(detail, args)) { }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            return string.Format("{0}: {1}{2}", field, string.IsNullOrWhiteSpace(detail) ? "" : " - ", detail);
        }

        #endregion Methods
    }
}
