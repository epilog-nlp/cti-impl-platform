using System;
using System.Collections.Generic;
using System.Text;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Platform.Models.Config;
using System.Linq;

namespace Cti.Platform.Adapters
{

    public static class ApplicationAdapter
    {

        public static Application ToApplication(this CfgApplication app)
            => new Application
            {
                CfgAppType = app.Type.Value,
                AppServers = app.AppServers?
                                .Select(conn => conn.ToConnectionInfo(app.DBID, app.GetTenantDBIDs()?.FirstOrDefault()))
                                .ToList(),
                Dbid = app.DBID,
                Enabled = app.State.IsEnabled(),
                Name = app.Name,
                TenantDbid = app.GetTenantDBIDs()?.FirstOrDefault(),    
            };

        public static ConnectionInfo ToConnectionInfo(this CfgConnInfo connInfo, int parentDbid, int? tenantDbid)
            => new ConnectionInfo
            {
                AppServerDbid = connInfo.AppServer.DBID,
                Dbid = parentDbid,
                TenantDbid = tenantDbid
            };

        //public static CfgApplication ToCfgApplication(this IApplication app, IConfService svc)
        //    => app.ToCfgApplication(new CfgApplication(svc));

        //public static CfgApplication ToCfgApplication(this IApplication app, CfgApplication itemToUpdate)
        //{
             
        //    return itemToUpdate;
        //}
    }
}
