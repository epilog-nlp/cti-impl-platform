// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;

namespace Platform.Models.Config
{
    /// <summary>
    /// A link between two <see cref="Application"/> objects.
    /// </summary>
    public class ConnectionInfo : ConfigObject, IConnectionInfo
    {
        /// <summary>
        /// DBID of the connected <see cref="IApplication"/>.
        /// </summary>
        public int AppServerDbid { get; set; }
    }
}
