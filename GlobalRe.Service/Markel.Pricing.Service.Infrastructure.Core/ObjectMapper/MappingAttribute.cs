using System;

namespace Markel.Pricing.Service.Infrastructure.ObjectMapper
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MappingAttribute : Attribute
    {
        #region Properties

        public Type FromType { get; private set; }
        public Type ToType { get; private set; }
        public string FromPropertyName { get; private set; }
        public string ToPropertyName { get; private set; }
        public bool IsForReferenceOnly { get; set; }

        #endregion Properties

        #region Constructors

        public MappingAttribute() { }

        public MappingAttribute(Type fromType, Type toType) : this()
        {
            FromType = fromType;
            ToType = toType;
        }

        public MappingAttribute(Type fromType, Type toType, string from, string to, bool forReferenceOnly = false) : this(fromType, toType)
        {
            FromPropertyName = from;
            ToPropertyName = to;
            IsForReferenceOnly = forReferenceOnly;
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            return $"{FromType.Name}[{FromPropertyName}] > {ToType.Name}[{ToPropertyName}]{(IsForReferenceOnly ? " - ForReferenceOnly" : string.Empty)}";
        }

        #endregion Methods
    }
}
