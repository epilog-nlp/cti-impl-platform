// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using System;
using System.Linq;

namespace Platform.Adapters
{
    using Models.Config;

    /// <summary>
    /// Adapters for conversions between PSDK/CTI Server objects.
    /// </summary>
    public static class ServerAdapter
    {
        /// <summary>
        /// Converts a PSDK Server Application to its CTI equivalent.
        /// </summary>
        /// <remarks>
        /// Duplicates similar code in <see cref="ApplicationAdapter"/> to avoid clutter.
        /// </remarks>
        /// <param name="server">The Server Application object to convert.</param>
        /// <returns>A <see cref="Server"/> object equivalent to the provided PSDK object.</returns>
        /// <exception cref="ArgumentException">
        /// Throws if the provided <paramref name="server"/> Application is not really a server.
        /// </exception>
        public static Server ToServer(this CfgApplication server)
            => !(server.IsServer.IsTrue() ?? false)
            ? throw InvalidServer(server, nameof(server))
            : new Server
            {
                AppServers = server.AppServers?
                    .Select(conn => conn.ToConnectionInfo(server.DBID, server.GetTenantDBIDs()?.FirstOrDefault()))
                    .ToList(),
                BackupServerDbid = server.ServerInfo.BackupServer?.DBID,
                CfgAppType = server.Type.Value,
                Dbid = server.DBID,
                Enabled = server.State.IsEnabled(),
                HostDbid = server.ServerInfo.Host?.DBID,
                Name = server.Name,
                Port = server.ServerInfo.Port,
                TenantDbid = server.GetTenantDBIDs()?.FirstOrDefault()
            };

        private static ArgumentException InvalidServer(CfgApplication application, string paramName)
            => new ArgumentException($"The provided Application object ({application.Name}:{application.DBID}) is not a Server, and is invalid for this operation.", paramName);
    }
}
