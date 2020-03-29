// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Cti.Protocols.Contracts;
using Cti.Repos.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Genesyslab.Platform.Configuration.Protocols;
using Platform.Config.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Repos.Config
{
    using Adapters;
    using Models.Config;

    /// <summary>
    /// A repository exposing Read operations on Genesys Config Server Person objects.
    /// </summary>
    public sealed class PersonRepo : NamedConfigObjectRepo<Person, IPerson, CfgPerson>, IPersonRepo
    {
        /// <summary>
        /// Adapter to convert from <see cref="CfgPerson"/> to <see cref="Person"/>.
        /// </summary>
        /// <param name="psdkObject">The Person object to convert.</param>
        /// <returns>A <see cref="Person"/> object.</returns>
        protected override IPerson FromPsdk(CfgPerson psdkObject)
            => psdkObject.ToPerson();

        /// <summary>
        /// Creates an instance of <see cref="PersonRepo"/> using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The Config Server Protocol object that will handle requests.</param>
        /// <returns>An instance of <see cref="PersonRepo"/> using the provided <paramref name="protocol"/>.</returns>
        public override ICtiFeature Create(ConfServerProtocol protocol)
            => new PersonRepo
            {
                Protocol = protocol
            };

        public override IPerson GetByName(string name)
        {
            // TODO - Add Logging
            return FromPsdk(userNameQuery.Value.Invoke(ConfService, name));
        }

        public override IEnumerable<IPerson> GetAllByName(string name)
        {
            // TODO - Add Logging
            return firstNameQuery.Value.Invoke(ConfService, name)
                .Union(lastNameQuery.Value.Invoke(ConfService, name))
                .Select(FromPsdk)
                .ToList();
        }

        private static readonly Lazy<Func<IConfService, string, CfgPerson>> userNameQuery
            = new Lazy<Func<IConfService, string, CfgPerson>>(() => PersonQueryFactory.GetUserNameQuery());

        private static readonly Lazy<Func<IConfService, string, IEnumerable<CfgPerson>>> firstNameQuery
            = new Lazy<Func<IConfService, string, IEnumerable<CfgPerson>>>(() => PersonQueryFactory.GetFirstNameQuery());

        private static readonly Lazy<Func<IConfService, string, IEnumerable<CfgPerson>>> lastNameQuery
            = new Lazy<Func<IConfService, string, IEnumerable<CfgPerson>>>(() => PersonQueryFactory.GetLastNameQuery());
    }
}
