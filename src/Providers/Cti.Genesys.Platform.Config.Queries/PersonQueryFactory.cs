// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.Queries;
using System;
using System.Collections.Generic;

namespace Platform.Config.Queries
{
    /// <summary>
    /// Static factory exposing methods to construct Genesys Config Server Person queries.
    /// </summary>
    public static class PersonQueryFactory
    {
        /// <summary>
        /// Constructs a delegate capable of retrieving a Config Server Person object by Username using a provided Service.
        /// </summary>
        /// <param name="queryBuilder">Optional delegate used to set additional filters.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Server Person object matching the provided name.</returns>
        public static Func<IConfService, string, CfgPerson> GetUserNameQuery(Action<CfgPersonQuery> queryBuilder = default)
            => (svc, name)
                => svc.RetrieveObject<CfgPerson>(BuildQuery(queryBuilder, q => q.UserName = name));

        /// <summary>
        /// Constructs a delegate capable of retrieving a Config Server Person object by Username using the provided <paramref name="service"/>.
        /// </summary>
        /// <param name="service">A valid Configuration Service object.</param>
        /// <param name="queryBuilder">Optional delegate used to set additional filters.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Server Person object matching the provided name.</returns>
        public static Func<string, CfgPerson> GetUserNameQuery(this IConfService service, Action<CfgPersonQuery> queryBuilder = default)
            => name
                => BuildExecutableQuery(service, queryBuilder, q => q.UserName = name).ExecuteSingleResult();

        /// <summary>
        /// Constructs a delegate capable of retrieving a Config Server Person object by First Name using a provided Service.
        /// </summary>
        /// <param name="queryBuilder">Optional delegate used to set additional filters.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Server Person object matching the provided name.</returns>
        public static Func<IConfService, string, IEnumerable<CfgPerson>> GetFirstNameQuery(Action<CfgPersonQuery> queryBuilder = default)
            => (svc, name)
                => svc.RetrieveMultipleObjects<CfgPerson>(BuildQuery(queryBuilder, q => q.FirstName = name));

        /// <summary>
        /// Constructs a delegate capable of retrieving a Config Server Person object by First Name using the provided <paramref name="service"/>.
        /// </summary>
        /// <param name="service">A valid Configuration Service object.</param>
        /// <param name="queryBuilder">Optional delegate used to set additional filters.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Server Person object matching the provided name.</returns>
        public static Func<string, IEnumerable<CfgPerson>> GetFirstNameQuery(this IConfService service, Action<CfgPersonQuery> queryBuilder = default)
            => name
                => BuildExecutableQuery(service, queryBuilder, q => q.FirstName = name).Execute();

        /// <summary>
        /// Constructs a delegate capable of retrieving a Config Server Person object by Last Name using a provided Service.
        /// </summary>
        /// <param name="queryBuilder">Optional delegate used to set additional filters.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Server Person object matching the provided name.</returns>
        public static Func<IConfService, string, IEnumerable<CfgPerson>> GetLastNameQuery(Action<CfgPersonQuery> queryBuilder = default)
            => (svc, name)
                => svc.RetrieveMultipleObjects<CfgPerson>(BuildQuery(queryBuilder, q => q.LastName = name));

        /// <summary>
        /// Constructs a delegate capable of retrieving a Config Server Person object by Last Name using the provided <paramref name="service"/>.
        /// </summary>
        /// <param name="service">A valid Configuration Service object.</param>
        /// <param name="queryBuilder">Optional delegate used to set additional filters.</param>
        /// <returns>A delegate which can be invoked to retrieve a Config Server Person object matching the provided name.</returns>
        public static Func<string, IEnumerable<CfgPerson>> GetLastNameQuery(this IConfService service, Action<CfgPersonQuery> queryBuilder = default)
            => name
                => BuildExecutableQuery(service, queryBuilder, q => q.LastName = name).Execute();

        private static CfgPersonQuery BuildExecutableQuery(IConfService service, params Action<CfgPersonQuery>[] queryBuilders)
        {
            var query = new CfgPersonQuery(service);
            foreach (var builder in queryBuilders)
                builder?.Invoke(query);
            return query;
        }

        private static CfgPersonQuery BuildQuery(params Action<CfgPersonQuery>[] queryBuilders)
        {
            var query = new CfgPersonQuery();
            foreach (var builder in queryBuilders)
                builder?.Invoke(query);
            return query;
        }
    }

}
