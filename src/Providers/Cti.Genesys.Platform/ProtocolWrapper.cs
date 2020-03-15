// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.Commons.Protocols;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Cti.Platform
{
    using Protocols.Contracts;

    /// <summary>
    /// Base type for all PSDK Feature Providers.
    /// </summary>
    /// <typeparam name="TProtocol">The PSDK Protocol exposing the CTI Feature.</typeparam>
    public abstract class ProtocolWrapper<TProtocol> : ICtiFeatureProvider
        where TProtocol : IProtocol
    {
        /// <summary>
        /// Discovers and instantiates an implementation of the provided <typeparamref name="TFeature"/> contract with an optional <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TFeature">The CTI Feature contract to discover and instantiate.</typeparam>
        /// <param name="name">An optional contract name.</param>
        /// <returns>An implementation of the provided <typeparamref name="TFeature"/> contract, if it exists.</returns>
        public TFeature Resolve<TFeature>(string name = "")
            where TFeature : ICtiFeature
        {
            return (TFeature)FeatureImplementations.Value
                .Where(impl => impl.Name.Equals(name))
                .Where(feature => typeof(TFeature).IsAssignableFrom(feature.GetType()))
                .Single().Create(Protocol);
        }

        /// <summary>
        /// The Protocol object to be provided to Features.
        /// </summary>
        protected TProtocol Protocol { get; }

        /// <summary>
        /// Discovered
        /// </summary>
        protected static Lazy<IEnumerable<ICtiFeature<TProtocol>>> FeatureImplementations { get; }
            = new Lazy<IEnumerable<ICtiFeature<TProtocol>>>(() => DiscoverAll<ICtiFeature<TProtocol>>());

        /// <summary>
        /// Discovers all compatible Feature contract implementations.
        /// </summary>
        /// <typeparam name="TFeature"></typeparam>
        /// <returns></returns>
        protected static IEnumerable<TFeature> DiscoverAll<TFeature>() => throw new NotImplementedException();
    }
}
