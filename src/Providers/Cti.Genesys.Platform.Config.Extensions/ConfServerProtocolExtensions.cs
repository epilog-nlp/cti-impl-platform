// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.Configuration.Protocols;

namespace Cti.Platform.Config.Extensions
{
    /// <summary>
    /// Contains methods extending normal <see cref="ConfServerProtocol"/> behaviour.
    /// </summary>
    public static class ConfServerProtocolExtensions
    {
        /// <summary>
        /// Gets or creates an <see cref="IConfService"/> from the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The protocol object to retrieve a Service from.</param>
        /// <returns>An <see cref="IConfService"/> instance.</returns>
        public static IConfService GetConfService(this ConfServerProtocol protocol)
            => ConfServiceFactory.RetrieveConfService(protocol) ?? ConfServiceFactory.CreateConfService(protocol);
    }
}
