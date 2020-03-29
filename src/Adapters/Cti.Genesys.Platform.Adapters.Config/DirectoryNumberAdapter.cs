// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Platform.Models.Config;

namespace Cti.Platform.Adapters
{
    /// <summary>
    /// Adapters for conversions between PSDK/CTI Directory Number (DN) objects.
    /// </summary>
    public static class DirectoryNumberAdapter
    {
        /// <summary>
        /// Converts a PSDK Directory Number (DN) object to its CTI equivalent.
        /// </summary>
        /// <param name="dn">The DN object to convert.</param>
        /// <returns>A <see cref="DirectoryNumber"/> (DN) object equivalent to the provided PSDK object.</returns>
        public static DirectoryNumber ToDirectoryNumber(this CfgDN dn)
            => new DirectoryNumber
            {
                SwitchDbid = dn.GetSwitchDBID(),
                CfgDNType = dn.Type.Value,
                Dbid = dn.DBID,
                Enabled = dn.State.IsEnabled(),
                Number = dn.Number,
                TenantDbid = dn.GetTenantDBID()
            };
    }
}
