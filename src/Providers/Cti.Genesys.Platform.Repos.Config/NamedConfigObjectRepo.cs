// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using System.Collections.Generic;

namespace Platform.Repos.Config
{
    using Models.Config;

    /// <summary>
    /// A repository exposing retrieval methods for Genesys Config Server objects by name.
    /// </summary>
    /// <typeparam name="TModel">The type of Config Server object.</typeparam>
    /// <typeparam name="TContract">The contract for the Model exposed to consumers.</typeparam>
    /// <typeparam name="TPsdk">The corresponding PSDK type.</typeparam>
    public abstract class NamedConfigObjectRepo<TModel, TContract, TPsdk> : ConfigObjectRepo<TModel, TContract, TPsdk>, INamedConfigObjectRepo<TContract>
        where TModel : ConfigObject<TPsdk>, TContract
        where TContract : IQueryableConfigObject, INamedConfigObject
        where TPsdk : CfgObject
    {
        /// <summary>
        /// Retrieves a Config Server object by its <paramref name="name"/>.
        /// Throws an exception if multiple matches are found.
        /// </summary>
        /// <param name="name">The <see cref="INamedConfigObject.Name"/> of the Config Server object.</param>
        /// <returns>A Config Server object with the provided <paramref name="name"/>, if it exists.</returns>
        public virtual TContract GetByName(string name) => throw new System.NotImplementedException();

        /// <summary>
        /// Retrieves all Config Server objects matching the provided <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="INamedConfigObject.Name"/> of the Config Server objects.</param>
        /// <returns>A collection of all Config Server objects with the provided <paramref name="name"/>.</returns>
        public virtual IEnumerable<TContract> GetAllByName(string name) => throw new System.NotImplementedException();
    }
}
