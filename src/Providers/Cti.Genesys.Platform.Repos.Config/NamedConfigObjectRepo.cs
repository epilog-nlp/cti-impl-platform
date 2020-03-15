using System;
using System.Collections.Generic;
using System.Text;
using Platform.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;

namespace Cti.Platform.Repos.Config
{
    
    using Models.Config;

    public abstract class NamedConfigObjectRepo<TModel,TPsdk> : ConfigObjectRepo<TModel,TPsdk>
        where TModel : ConfigObject<TPsdk>, IQueryableConfigObject, INamedConfigObject
        where TPsdk : ICfgBase
    {
    }
}
