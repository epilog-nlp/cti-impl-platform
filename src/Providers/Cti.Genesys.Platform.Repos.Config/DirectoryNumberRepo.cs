// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Cti.Protocols.Contracts;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.Configuration.Protocols;
using System;
using System.Collections.Generic;

namespace Platform.Repos.Config
{
    using Adapters;
    using Models.Config;

    /// <summary>
    /// A repository exposing Read operations on Genesys Config Server Director Number (DN) objects.
    /// </summary>
    public sealed class DirectoryNumberRepo : ConfigObjectRepo<DirectoryNumber, IDirectoryNumber, CfgDN>, IDirectoryNumberRepo
    {
        /// <summary>
        /// Adapter to convert from <see cref="CfgDN"/> to <see cref="DirectoryNumber"/>.
        /// </summary>
        /// <param name="psdkObject">The DN object to convert.</param>
        /// <returns>A <see cref="DirectoryNumber"/> object.</returns>
        protected override IDirectoryNumber FromPsdk(CfgDN psdkObject)
            => psdkObject.ToDirectoryNumber();

        /// <summary>
        /// Creates an instance of <see cref="DirectoryNumberRepo"/> using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The Config Server Protocol object that will handle requests.</param>
        /// <returns>An instance of <see cref="DirectoryNumberRepo"/> using the provided <paramref name="protocol"/>.</returns>
        public override ICtiFeature Create(ConfServerProtocol protocol)
            => new DirectoryNumberRepo
            {
                Protocol = protocol
            };

        /// <summary>
        /// Retrieves all Directory Number objects with the provided <paramref name="directoryNumber"/>.
        /// </summary>
        /// <param name="directoryNumber">The <see cref="CfgDN.Number"/> to match on.</param>
        /// <returns>A collection of <see cref="DirectoryNumber"/> objects matching the provided <paramref name="directoryNumber"/>.</returns>
        public IEnumerable<IDirectoryNumber> GetByNumber(string directoryNumber) => throw new NotImplementedException();
    }
}
