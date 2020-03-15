// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.Queries;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cti.Platform.Config.Queries
{
    using static AbstractQueryFactory;
    using static ReflectionResources;

    /// <summary>
    /// Static factory exposing methods to create Genesys Config Server queries with DBID filters.
    /// </summary>
    public static class DbidQueryFactory
    {

        /// <summary>
        /// Constructs a delegate capable of retrieving a Configuration Object of type <typeparamref name="TCfgObject"/> by its DBID.
        /// </summary>
        /// <typeparam name="TCfgObject">The type of Configuration Object to retrieve.</typeparam>
        /// <returns>A delegate which can be invoked to retrieve a Config Object with the provided DBID.</returns>
        public static Func<IConfService, int, TCfgObject> BuildDbidQuery<TCfgObject>()
            where TCfgObject : CfgObject
            => (svc, dbid) => RetrieveObjectDelegate<TCfgObject>()(svc, CreateDbidQuery(svc, ObjectToQueryMap[typeof(TCfgObject)], dbid));

        /// <summary>
        /// Constructs a delegate capable of retrieving a Configuration Object of type <typeparamref name="TCfgObject"/> by its DBID.
        /// </summary>
        /// <typeparam name="TCfgObject">The type of Configuration Object to retrieve.</typeparam>
        /// <param name="service">A valid Configuration Service object.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Object with the provided DBID.</returns>
        public static Func<int, TCfgObject> BuildDbidQuery<TCfgObject>(this IConfService service)
            where TCfgObject : CfgObject
            => dbid => BuildDbidQuery<TCfgObject>()(service, dbid);

        private static CfgFilterBasedQuery CreateDbidQuery(IConfService service, Type queryType, int dbid)
        {
            if(!dbidQueries.Value.Contains(queryType))
                throw new InvalidOperationException($"The provided query type is not valid for filtering by DBID: {queryType.FullName}");
            var query = CreateQuery(service, queryType);
            queryType.GetProperty(DbidProperty).SetValue(query, dbid);
            return query;
        }

        private static readonly Lazy<ImmutableList<Type>> dbidQueries
            = new Lazy<ImmutableList<Type>>(() =>
                FilterQueries.Where(type => type.GetProperties()
                             .Any(prop => prop.Name.Equals(DbidProperty)))
                             .ToImmutableList());

        private const string DbidProperty = nameof(CfgPersonQuery.Dbid);
    }
}
