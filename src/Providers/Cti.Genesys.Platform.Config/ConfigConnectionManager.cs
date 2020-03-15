// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Protocols.Contracts;
using Genesyslab.Platform.Configuration.Protocols;

namespace Cti.Platform.Config
{
    /// <summary>
    /// An object that can manage the state of a <see cref="ConfServerProtocol"/> object.
    /// </summary>
    public sealed class ConfigConnectionManager : ProtocolConnectionManager<ConfServerProtocol>
    {
        /// <summary>
        /// Creates an instance of this ConnectionManager using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The protocol this ConnectionManager delegates requests to.</param>
        /// <returns>An instance of this ConnectionManager, created using the provided <paramref name="protocol"/>.</returns>
        public override ICtiFeature Create(ConfServerProtocol protocol)
            => new ConfigConnectionManager
            {
                Protocol = protocol
            };
    }
}
