// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using System.Linq;

namespace Platform.Adapters
{
    using Models.Config;

    /// <summary>
    /// Adapters for conversions between PSDK/CTI Application objects.
    /// </summary>
    public static class ApplicationAdapter
    {
        /// <summary>
        /// Converts a PSDK Application object to its CTI equivalent.
        /// </summary>
        /// <param name="app">The Application object to convert.</param>
        /// <returns>An <see cref="Application"/> object equivalent to the provided PSDK object.</returns>
        public static Application ToApplication(this CfgApplication app)
            => new Application
            {
                CfgAppType = app.Type.Value,
                AppServers = app.AppServers?
                    .Select(conn => conn.ToConnectionInfo(app.DBID, app.GetTenantDBIDs()?.FirstOrDefault()))
                    .ToList(),
                Dbid = app.DBID,
                Enabled = app.State.IsEnabled(),
                Name = app.Name,
                TenantDbid = app.GetTenantDBIDs()?.FirstOrDefault()
            };

        /// <summary>
        /// Converts a PSDK Connection Info object to its CTI equivalent.
        /// </summary>
        /// <param name="connInfo">The Connection Info object to convert.</param>
        /// <param name="parentDbid">The DBID of the parent Application.</param>
        /// <param name="tenantDbid">The DBID of the parent Application's Tenant.</param>
        /// <returns>A <see cref="ConnectionInfo"/> object equivalent to the provided PSDK object.</returns>
        internal static ConnectionInfo ToConnectionInfo(this CfgConnInfo connInfo, int parentDbid, int? tenantDbid)
            => new ConnectionInfo
            {
                AppServerDbid = connInfo.AppServer.DBID,
                Dbid = parentDbid,
                TenantDbid = tenantDbid
            };
    }
}
