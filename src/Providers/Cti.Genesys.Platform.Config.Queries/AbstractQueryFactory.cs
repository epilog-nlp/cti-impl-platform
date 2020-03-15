// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.Queries;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Cti.Genesys.Platform.Config.Queries
{
    using static ReflectionResources;

    /// <summary>
    /// Static factory exposing methods to construct Genesys Config Server queries.
    /// </summary>
    public static class AbstractQueryFactory
    {
        /// <summary>
        /// Constructs a delegate capable of retrieving all Configuration Objects of type <typeparamref name="TCfgObject"/> using the provided <paramref name="service"/>.
        /// </summary>
        /// <typeparam name="TCfgObject">The type of Configuration Object the delegate is specialized for.</typeparam>
        /// <param name="service">A valid Configuration Service object.</param>
        /// <returns>A delegate which can be invoked to retrieve all Configuration Objects of type <typeparamref name="TCfgObject"/>.</returns>
        public static Func<IEnumerable<TCfgObject>> BuildGetAllQuery<TCfgObject>(this IConfService service)
            where TCfgObject : CfgObject
            => () => RetrieveMultipleObjectsDelegate<TCfgObject>()(service, CreateQuery(service, ObjectToQueryMap[typeof(TCfgObject)]));

        internal static CfgFilterBasedQuery CreateQuery(IConfService service, Type queryType)
            => queryType.GetConstructor(new[] { QueryConstructorArgument })
                        .Invoke(new[] { service }) as CfgFilterBasedQuery;

        internal static Func<IConfService, CfgQuery, IEnumerable<TCfgObject>> RetrieveMultipleObjectsDelegate<TCfgObject>()
            where TCfgObject : CfgObject
            => (svc, query) => CloseGenericDelegate<TCfgObject>(nameof(IConfService.RetrieveMultipleObjects))(svc, query) as IEnumerable<TCfgObject>;

        internal static Func<IConfService, CfgQuery, TCfgObject> RetrieveObjectDelegate<TCfgObject>()
            where TCfgObject : CfgObject
            => (svc, query) => CloseGenericDelegate<TCfgObject>(nameof(IConfService.RetrieveObject))(svc, query) as TCfgObject;

        private static Func<IConfService, CfgQuery, object> CloseGenericDelegate<TCfgObject>(string methodName)
            => (svc, query) => typeof(IConfService).GetMethod(methodName, new[] { QueryBaseContract })
            .MakeGenericMethod(typeof(TCfgObject))
            .Invoke(svc, new[] { query });

        internal static IReadOnlyDictionary<Type, Type> ObjectToQueryMap => objectToQueryMap.Value;

        private static readonly Lazy<ImmutableDictionary<Type, Type>> objectToQueryMap
            = new Lazy<ImmutableDictionary<Type, Type>>(() => 
                FilterQueries.ToImmutableDictionary(
                    GetQueryReturnType, // Key Selector
                    val => val));
    }
}
