using System;

namespace LaXiS.ImageHash.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NavigationAttribute : Attribute
    {
        public string NameofIdProperty { get; }

        /// <summary>
        /// Specifies that this is a navigation property for the given ID property
        /// </summary>
        public NavigationAttribute(string nameofIdProperty)
        {
            NameofIdProperty = nameofIdProperty;
        }
    }
}
