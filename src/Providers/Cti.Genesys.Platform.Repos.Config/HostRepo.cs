// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Cti.Protocols.Contracts;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.Configuration.Protocols;

namespace Platform.Repos.Config
{
    using Adapters;
    using Models.Config;

    /// <summary>
    /// A repository exposing Read operations on Genesys Config Server Host objects.
    /// </summary>
    public sealed class HostRepo : NamedConfigObjectRepo<Host, IHost, CfgHost>, IHostRepo
    {
        /// <summary>
        /// Adapter to convert from <see cref="CfgHost"/> to <see cref="Host"/>.
        /// </summary>
        /// <param name="psdkObject">The Host object to convert.</param>
        /// <returns>A <see cref="Host"/> object.</returns>
        protected override IHost FromPsdk(CfgHost psdkObject)
            => psdkObject.ToHost();

        /// <summary>
        /// Creates an instance of <see cref="HostRepo"/> using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The Config Server Protocol object that will handle requests.</param>
        /// <returns>An instance of <see cref="HostRepo"/> using the provided <paramref name="protocol"/>.</returns>
        public override ICtiFeature Create(ConfServerProtocol protocol)
            => new HostRepo
            {
                Protocol = protocol
            };
    }
}
