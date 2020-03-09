// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.Configuration.Protocols.Types;

namespace Platform.Models.Config
{
    /// <summary>
    /// A Genesys Config Server DN object.
    /// </summary>
    public class DirectoryNumber : ConfigObject<CfgDN>, IDirectoryNumber
    {
        /// <summary>
        /// Directory Number. Must be unique accross the parent Switch.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The type of DN this represents.
        /// </summary>
        public string DnType => CfgDNType.ToString();

        /// <summary>
        /// The PSDK DN Type. Source for <see cref="DnType"/>.
        /// </summary>
        public CfgDNType CfgDNType { get; set; }

        /// <summary>
        /// The DBID of the Switch where this DN resides.
        /// </summary>
        public int SwitchDbid { get; set; }

        /// <summary>
        /// The state of the Config Server object.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
