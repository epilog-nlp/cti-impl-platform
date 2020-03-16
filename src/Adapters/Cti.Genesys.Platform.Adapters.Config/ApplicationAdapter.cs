using System;
using System.Collections.Generic;
using System.Text;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;

namespace Cti.Platform.Adapters
{    
    using Models.Config;

    public static class ApplicationAdapter
    {
        public static CfgApplication ToCfgApplication(this IApplication app, IConfService svc)
            => app.ToCfgApplication(new CfgApplication(svc));

        public static CfgApplication ToCfgApplication(this IApplication app, CfgApplication itemToUpdate)
        {
             
            return itemToUpdate;
        }
    }
}
