// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Platform.Models.Config;

namespace Cti.Platform.Adapters
{
    /// <summary>
    /// Adapters for conversions between PSDK/CTI Host objects.
    /// </summary>
    public static class HostAdapter
    {
        /// <summary>
        /// Converts a PSDK Host object to its CTI equivalent.
        /// </summary>
        /// <param name="host">The Host object to convert.</param>
        /// <returns>A <see cref="Host"/> object equivalent to the provided PSDK object.</returns>
        public static Host ToHost(this CfgHost host)
            => new Host
            {
                Dbid = host.DBID,
                Enabled = host.State.IsEnabled(),
                IpAddress = host.IPaddress,
                Name = host.Name,
                TenantDbid = WellKnownDbids.EnvironmentDbid
            };
    }
}
