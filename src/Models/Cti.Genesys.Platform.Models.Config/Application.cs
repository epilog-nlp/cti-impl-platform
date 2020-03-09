// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.Configuration.Protocols.Types;
using System.Collections.Generic;

namespace Platform.Models.Config
{
    /// <summary>
    /// A Config Server Application object.
    /// </summary>
    public class Application : ConfigObject<CfgApplication>, IApplication
    {
        /// <summary>
        /// Identifier for the type of the Application object.
        /// </summary>
        public int AppType 
        { 
            get => (int)CfgAppType; 
            set => CfgAppType = (CfgAppType)value; 
        }

        /// <summary>
        /// The PSDK Application Type. Source for <see cref="AppType"/>.
        /// </summary>
        public CfgAppType CfgAppType { get; set; }

        /// <summary>
        /// Connections to other Applications.
        /// </summary>
        public IEnumerable<IConnectionInfo> AppServers { get; set; }

        /// <summary>
        /// Name of the Config Server object.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The state of the Config Server object.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
