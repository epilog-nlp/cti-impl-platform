// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cti.Genesys.Platform.Config.Queries
{
    internal static class ReflectionResources
    {
        
        public static IEnumerable<MethodInfo> FindReturningMethods(this Type source, Type returnType)
            => source.GetMethods()
                     .Where(m => returnType.IsAssignableFrom(m.ReturnType));

        public static Type GetQueryReturnType(Type queryType)
            => queryType.FindReturningMethods(ConfigObjectContract)
                        .Single()
                        .ReturnType;

        public static IEnumerable<Type> FilterQueries
            => QueryContractSource.GetTypes()
                                  .Where(FilterQueryContract.IsAssignableFrom)
                                  .Except(ExcludedFilters);

        public static IEnumerable<Type> ExcludedFilters
            => new[]
            {
                FilterQueryContract,
                typeof(CfgFilterBasedQuery)
            };

        public static Type ConfigObjectContract => typeof(ICfgObject);

        public static Type FilterQueryContract => typeof(ICfgFilterBasedQuery);

        public static Type QueryBaseContract => typeof(ICfgQuery);

        public static Type QueryConstructorArgument => typeof(IConfService);

        public static Assembly QueryContractSource => FilterQueryContract.Assembly;

    }
}
