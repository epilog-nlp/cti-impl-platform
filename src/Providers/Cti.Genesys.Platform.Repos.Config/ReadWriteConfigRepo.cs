// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;

namespace Platform.Repos.Config
{
    using Models.Config;

    /// <summary>
    /// A repository exposing CRUD operations on Genesys Config Server objects.
    /// </summary>
    /// <typeparam name="TModel">The type of Config Server object.</typeparam>
    /// <typeparam name="TPsdk">The corresponding PSDK type.</typeparam>
    public abstract class ReadWriteConfigRepo<TModel, TPsdk> : ConfigObjectRepo<TModel, TPsdk>, IReadWriteConfigRepo<TModel>
        where TModel : ConfigObject<TPsdk>, IQueryableConfigObject
        where TPsdk : CfgObject
    {
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
        /// Adapter to convert from <typeparamref name="TModel"/> to <typeparamref name="TPsdk"/>.
        /// </summary>
        /// <param name="ctiObject">The CTI object to translate.</param>
        /// <param name="service">A Config Service object capable of creating and saving the PSDK Config object.</param>
        /// <returns>A PSDK Model equivalent to the provided CTI Model, bound directly to Genesys.</returns>
        protected abstract TPsdk ToPsdk(TModel ctiObject, IConfService service);

        /// <summary>
        /// Method that sets the Enabled state of a PSDK object to the provided value.
        /// </summary>
        /// <param name="psdkItem">The item to update.</param>
        /// <param name="isEnabled">Value to assign on the object.</param>
        /// <returns>The updated value on the object.</returns>
        protected abstract bool SetEnabledState(TPsdk psdkItem, bool isEnabled);
    }
}
