// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using System.Collections.Generic;

namespace Platform.Models.Config
{
    /// <summary>
    /// A Genesys Config Server Place object.
    /// </summary>
    public class Place : ConfigObject<CfgPlace>, IPlace
    {
        /// <summary>
        /// DBIDs of <see cref="DirectoryNumber"/> items operated by this <see cref="Place"/> object.
        /// </summary>
        public IEnumerable<int> DnDbids { get; set; }

        /// <summary>
        /// Name of Config Server object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// State of the Config Server object.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
