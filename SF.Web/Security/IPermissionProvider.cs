﻿using System.Collections.Generic;

namespace SF.Web.Security
{
    /// <summary>
    /// Implemented by modules to enumerate the types of permissions
    /// the which may be granted
    /// </summary>
    public interface IPermissionProvider
    {
        IEnumerable<Permission> GetPermissions();
        IEnumerable<PermissionStereotype> GetDefaultStereotypes();
    }

    public class PermissionStereotype
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
