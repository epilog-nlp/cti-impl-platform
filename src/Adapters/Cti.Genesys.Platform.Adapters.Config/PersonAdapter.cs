// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;

namespace Platform.Adapters
{
    using Models.Config;

    /// <summary>
    /// Adapters for conversions between PSDK/CTI Person objects.
    /// </summary>
    public static class PersonAdapter
    {
        /// <summary>
        /// Converts a PSDK Person object to its CTI equivalent.
        /// </summary>
        /// <param name="person">The Person object to convert.</param>
        /// <returns>A <see cref="Person"/> object equivalent to the provided PSDK object.</returns>
        public static Person ToPerson(this CfgPerson person)
            => new Person
            {
                Dbid = person.DBID,
                Email = person.EmailAddress,
                Enabled = person.State.IsEnabled(),
                ExternalId = person.ExternalID,
                FirstName = person.FirstName,
                LastName = person.LastName,
                FolderId = person.FolderId,
                FolderPath = person.ObjectPath,
                IsAgent = person.IsAgent.IsTrue() ?? false,
                Name = person.UserName,
                TenantDbid = person.GetTenantDBID(),
                UsingExternalAuth = person.IsExternalAuth.IsTrue()
            };
    }
}
