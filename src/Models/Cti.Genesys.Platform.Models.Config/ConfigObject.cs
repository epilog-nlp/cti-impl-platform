// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models;
using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using System;

namespace Platform.Models.Config
{
    /// <summary>
    /// A Genesys Config Server object exposed by the PSDK.
    /// </summary>
    public abstract class ConfigObject : GenesysObject, IConfigObject
    {
        /// <summary>
        /// The DBID (Database Id) of the Config Server object. New objects will have a <c>null</c> Dbid.
        /// </summary>
        public int? Dbid { get; set; }

        /// <summary>
        /// The parent Tenant DBID of the Config Server object.
        /// </summary>
        public int TenantDbid { get; set; }
    }

    /// <summary>
    /// A Genesys Config Server object exposed by the PSDK with a direct correlation to a PSDK Type.
    /// </summary>
    /// <typeparam name="TPsdk"></typeparam>
    public abstract class ConfigObject<TPsdk> : ConfigObject, IPsdkObject<TPsdk>
        where TPsdk : ICfgBase
    {
        /// <summary>
        /// The Genesys PSDK Config Server object Type this item corresponds to.
        /// </summary>
        public virtual Type PsdkType => typeof(TPsdk);
    }
}
