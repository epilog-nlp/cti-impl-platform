// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Repos;
using Genesyslab.Platform.Commons.Protocols;

namespace Cti.Platform.Repos
{
    using Protocols.Contracts;
    using Models;

    /// <summary>
    /// Base type for all PSDK Repository Features exposing Genesys objects.
    /// </summary>
    /// <typeparam name="TModel">The type of model the Repo exposes.</typeparam>
    /// <typeparam name="TProtocol">The PSDK protocol exposing access to Genesys.</typeparam>
    public abstract class PsdkRepo<TModel, TProtocol> : IGenesysRepo, ICtiFeature<TProtocol>
        where TModel : IGenesysObject
        where TProtocol : IProtocol
    {
        /// <summary>
        /// Name used to distinguish implementations sharing the same contract signature.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The <see cref="IProtocol"/> implementation capable of handling requests to Genesys.
        /// </summary>
        protected TProtocol Protocol { get; set; }

        /// <summary>
        /// Creates an instance of this Repo using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The PSDK protocol this Repo delegates requests to.</param>
        /// <returns>An instance of this Repo, created using the provided <paramref name="protocol"/>.</returns>
        public abstract ICtiFeature Create(TProtocol protocol);
    }
}
