using Cti.Platform.Config.Extensions;
using Cti.Platform.Config.Queries;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.Configuration.Protocols;
using Platform.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cti.Platform.Repos.Config
{
    using Models.Config;

    /// <summary>
    /// A repository exposing CRUD operations on Genesys Config Server objects.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TPsdk"></typeparam>
    public abstract class ConfigObjectRepo<TModel,TPsdk> : PsdkRepo<TModel, ConfServerProtocol>, IConfigObjectRepo<TModel>
        where TModel : ConfigObject<TPsdk>, IQueryableConfigObject
        where TPsdk : CfgObject
    {
        /// <summary>
        /// Retrieves a Config Server object by its unique <paramref name="dbid"/>.
        /// </summary>
        /// <param name="dbid">The unique DBID of the object to retrieve.</param>
        /// <returns>The Config Server object with the provided <paramref name="dbid"/>, if it exists.</returns>
        public virtual TModel Get(int dbid)
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
        public virtual IEnumerable<TModel> Get(params int[] dbids)
        {
            // TODO - Add Logging
            var psdkItems = dbids.Any()
                ? dbids.Select(GetById)
                : GetAll();
            return psdkItems.Select(FromPsdk);
        }

        /// <summary>
        /// Creates the provided Config Server object and returns its value.
        /// </summary>
        /// <param name="itemToAdd">The item to add.</param>
        /// <returns>The created Config Server object, with all fields populated.</returns>
        public virtual TModel Add(TModel itemToAdd)
        {
            // TODO - Add Logging
            var psdkItem = ToPsdk(itemToAdd, ConfService);
            psdkItem.Save();
            return FromPsdk(psdkItem);
        }

        /// <summary>
        /// Updates an existing Config Server object and returns its value.
        /// </summary>
        /// <param name="itemToUpdate">The existing item, with updated properties.</param>
        /// <returns>The updated Config Server object, with all fields populated.</returns>
        public virtual TModel Update(TModel itemToUpdate) => Add(itemToUpdate);

        /// <summary>
        /// Enables a Config Server object with the provided <paramref name="dbid"/>.
        /// </summary>
        /// <param name="dbid">The unique DBID of the Config Server object to enable.</param>
        /// <returns>The value of <see cref="IQueryableConfigObject.Enabled"/> after the operation.</returns>
        public virtual bool Enable(int dbid)
        {
            // TODO - Add Logging
            var psdkItem = GetById(dbid);
            var result = SetEnabledState(psdkItem, true);
            psdkItem.Save();
            return result;
        }

        /// <summary>
        /// Disables a Config Server object with the provided <paramref name="dbid"/>.
        /// </summary>
        /// <param name="dbid">The unique DBID of the Config Server object to disable.</param>
        /// <returns>The value of <see cref="IQueryableConfigObject.Enabled"/> after the operation.</returns>
        public virtual bool Disable(int dbid)
        {
            // TODO - Add Logging
            var psdkItem = GetById(dbid);
            var result = SetEnabledState(psdkItem, false);
            psdkItem.Save();
            return result;
        }

        /// <summary>
        /// A Config Service object capable of handling requests. Built using the <see cref="PsdkRepo{TModel, TProtocol}.Protocol"/>.
        /// </summary>
        protected IConfService ConfService => Protocol.GetConfService();

        /// <summary>
        /// Adapter to convert from <typeparamref name="TPsdk"/> to <typeparamref name="TModel"/>.
        /// </summary>
        /// <param name="psdkObject">The PSDK Config object to translate.</param>
        /// <returns>A CTI Model equivalent to the provided PSDK object.</returns>
        protected abstract TModel FromPsdk(TPsdk psdkObject);

        /// <summary>
        /// Adapter to convert from <typeparamref name="TModel"/> to <typeparamref name="TPsdk"/>.
        /// </summary>
        /// <param name="ctiObject">The CTI object to translate.</param>
        /// <param name="service">A Config Service object capable of creating and saving the PSDK Config object.</param>
        /// <returns>A PSDK Model equivalent to the provided CTI Model, bound directly to Genesys.</returns>
        protected abstract TPsdk ToPsdk(TModel ctiObject, IConfService service);

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
        /// Method that sets the Enabled state of a PSDK object to the provided value.
        /// </summary>
        /// <param name="psdkItem">The item to update.</param>
        /// <param name="isEnabled">Value to assign on the object.</param>
        /// <returns>The updated value on the object.</returns>
        protected abstract bool SetEnabledState(TPsdk psdkItem, bool isEnabled);

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
