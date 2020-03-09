// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;

namespace Platform.Models.Config
{
    /// <summary>
    /// A Genesys Config Server Host object.
    /// </summary>
    public class Host : ConfigObject<CfgHost>, IHost
    {
        /// <summary>
        /// IP Address of the Host.
        /// </summary>
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// Name of the Config Server Host object.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The state of the Config Server object.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
