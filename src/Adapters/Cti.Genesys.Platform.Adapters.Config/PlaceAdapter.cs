// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using System.Linq;

namespace Platform.Adapters
{
    using Models.Config;

    /// <summary>
    /// Adapters for conversions between PSDK/CTI Place objects.
    /// </summary>
    public static class PlaceAdapter
    {
        /// <summary>
        /// Converts a PSDK Place object to its CTI equivalent.
        /// </summary>
        /// <param name="place">The Place object to convert.</param>
        /// <returns>A <see cref="Place"/> object equivalent to the provided PSDK object.</returns>
        public static Place ToPlace(this CfgPlace place)
            => new Place
            {
                Dbid = place.DBID,
                DnDbids = place.GetDNDBIDs().ToList() ?? Enumerable.Empty<int>(),
                Enabled = place.State.IsEnabled(),
                Name = place.Name,
                TenantDbid = place.GetTenantDBID()
            };
    }
}
