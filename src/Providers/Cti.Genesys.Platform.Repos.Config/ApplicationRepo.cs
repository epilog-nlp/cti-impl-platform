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
    /// A repository exposing Read operations on Genesys Config Server Application objects.
    /// </summary>
    public sealed class ApplicationRepo : NamedConfigObjectRepo<Application, IApplication, CfgApplication>, IApplicationRepo
    {
        /// <summary>
        /// Adapter to convert from <see cref="CfgApplication"/> to <see cref="Application"/>.
        /// </summary>
        /// <param name="psdkObject">The Application object to convert.</param>
        /// <returns>An <see cref="Application"/> object.</returns>
        protected override IApplication FromPsdk(CfgApplication psdkObject)
            => psdkObject.ToApplication();

        /// <summary>
        /// Creates an instance of <see cref="ApplicationRepo"/> using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The Config Server Protocol object that will handle requests.</param>
        /// <returns>An instance of <see cref="ApplicationRepo"/> using the provided <paramref name="protocol"/>.</returns>
        public override ICtiFeature Create(ConfServerProtocol protocol)
            => new ApplicationRepo
            {
                Protocol = protocol
            };
    }
}
