using System;

namespace SF.Data
{
    /// <summary>
    /// Used with OptIn AnnotationMode to include the entity on the Audit logs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class AuditIncludeAttribute : Attribute
    {

    }
}