using System;
using System.Collections.Generic;
using System.Text;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.Configuration.Protocols.Types;

namespace Cti.Platform.Adapters
{
    public static class EnumAdapter
    {
        public static bool? IsEnabled(this CfgObjectState? state)
        {
            if (state is null)
                return null;
            return state.Value == CfgObjectState.CFGEnabled;
        }
    }
}
