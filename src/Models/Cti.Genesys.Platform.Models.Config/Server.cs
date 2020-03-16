// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;

namespace Platform.Models.Config
{
    /// <summary>
    /// Specialized Config Server Application object with hosting details.
    /// </summary>
    public class Server : Application, IServer
    {
        /// <summary>
        /// DBID of the Host hosting this <see cref="Server"/>.
        /// </summary>
        public int? HostDbid { get; set; }

        /// <summary>
        /// Port exposing this <see cref="Server"/>.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// DBID of the <see cref="Application"/> defining the backup <see cref="Server"/>.
        /// </summary>
        public int? BackupServerDbid { get; set; }
    }
}
