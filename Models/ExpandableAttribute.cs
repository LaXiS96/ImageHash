using System;

namespace LaXiS.ImageHash.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    sealed class ExpandableAttribute : Attribute
    {
        public Type ExpandedType { get; }

        /// <summary>
        /// Specifies that this property is an entity ID that can be expanded to the given type
        /// </summary>
        public ExpandableAttribute(Type expandedType)
        {
            ExpandedType = expandedType;
        }
    }
}
