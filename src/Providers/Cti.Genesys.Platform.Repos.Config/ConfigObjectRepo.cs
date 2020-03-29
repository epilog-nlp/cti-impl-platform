// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.Configuration.Protocols;
using Platform.Config.Extensions;
using Platform.Config.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Repos.Config
{
    using Models.Config;

    /// <summary>
    /// A repository exposing retrieval methods for Genesys Config Server objects by their unique DBID.
    /// </summary>
    /// <typeparam name="TModel">The type of Config Server object.</typeparam>
    /// <typeparam name="TContract">The contract for the Model exposed to consumers.</typeparam>
    /// <typeparam name="TPsdk">The corresponding PSDK type.</typeparam>
    public abstract class ConfigObjectRepo<TModel, TContract, TPsdk> : PsdkRepo<TContract, ConfServerProtocol>, IConfigObjectRepo<TContract>
        where TModel : ConfigObject<TPsdk>, TContract
        where TContract : IQueryableConfigObject
        where TPsdk : CfgObject
    {
        /// <summary>
        /// Retrieves a Config Server object by its unique <paramref name="dbid"/>.
        /// </summary>
        /// <param name="dbid">The unique DBID of the object to retrieve.</param>
        /// <returns>The Config Server object with the provided <paramref name="dbid"/>, if it exists.</returns>
        public virtual TContract Get(int dbid)
        {
            // TODO - Add Logging
            return FromPsdk(GetById(dbid));
        }

        /// <summary>
        /// Retrieves multiple Config Server objects by their unique <paramref name="dbids"/>.
        /// Retrieves all items if no <paramref name="dbids"/> are provided.
        /// </summary>
        /// <param name="dbids">A collection of unique DBIDs of the objects to retrieve.</param>
        /// <returns>A collection of Config Server objects matching the provided <paramref name="dbids"/>.</returns>
        public virtual IEnumerable<TContract> Get(params int[] dbids)
        {
            // TODO - Add Logging
            var psdkItems = dbids.Any()
                ? dbids.Select(GetById)
                : GetAll();
            return psdkItems.Select(FromPsdk)
                            .ToList();
        }

        /// <summary>
        /// A Config Service object capable of handling requests. Built using the <see cref="PsdkRepo{TModel, TProtocol}.Protocol"/>.
        /// </summary>
        protected IConfService ConfService => Protocol.GetConfService();

        /// <summary>
        /// Adapter to convert from <typeparamref name="TPsdk"/> to <typeparamref name="TContract"/>.
        /// </summary>
        /// <param name="psdkObject">The PSDK Config object to translate.</param>
        /// <returns>A CTI Model equivalent to the provided PSDK object.</returns>
        protected abstract TContract FromPsdk(TPsdk psdkObject);

        /// <summary>
        /// Retrieves all PSDK objects without translating.
        /// </summary>
        /// <returns>A collection of all PSDK objects.</returns>
        protected virtual IEnumerable<TPsdk> GetAll()
            => getAllQuery.Value(ConfService);

        /// <summary>
        /// Retrieves a PSDK object by its DBID without translating.
        /// </summary>
        /// <param name="dbid">The unique DBID of the PSDK object.</param>
        /// <returns>A PSDK object with the provided DBID, if it exists.</returns>
        protected virtual TPsdk GetById(int dbid)
            => getByIdQuery.Value(ConfService, dbid);

        /// <summary>
        /// Query to retrieve all objects from the Config Server.
        /// </summary>
        protected static readonly Lazy<Func<IConfService, IEnumerable<TPsdk>>> getAllQuery
            = new Lazy<Func<IConfService, IEnumerable<TPsdk>>>(() => AbstractQueryFactory.BuildGetAllQuery<TPsdk>());

        /// <summary>
        /// Query to retrieve a single object from the Config Server using its unique DBID.
        /// </summary>
        protected static readonly Lazy<Func<IConfService, int, TPsdk>> getByIdQuery
            = new Lazy<Func<IConfService, int, TPsdk>>(() => DbidQueryFactory.BuildDbidQuery<TPsdk>());
    }
}
