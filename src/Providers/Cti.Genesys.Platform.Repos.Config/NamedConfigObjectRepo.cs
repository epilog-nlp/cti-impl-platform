// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;

namespace Platform.Repos.Config
{
    using Models.Config;

    /// <summary>
    /// A repository exposing retrieval methods for Genesys Config Server objects by name.
    /// </summary>
    /// <typeparam name="TModel">The type of Config Server object.</typeparam>
    /// <typeparam name="TPsdk">The corresponding PSDK type.</typeparam>
    public abstract class NamedConfigObjectRepo<TModel, TPsdk> : ConfigObjectRepo<TModel, TPsdk>
        where TModel : ConfigObject<TPsdk>, IQueryableConfigObject, INamedConfigObject
        where TPsdk : CfgObject
    {
    }
}
