// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;

namespace Platform.Models.Config
{
    /// <summary>
    /// A Config Server Person object.
    /// </summary>
    public class Person : ConfigObject<CfgPerson>, IPerson
    {
        private string password = string.Empty;

        /// <summary>
        /// The LDAP (or equivalent) Username associated with the Person. Used is <see cref="UsingExternalAuth"/> is enabled.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// The password used to login. Value is only used to set the password on an Create/Update request.
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !password.Equals(value))
                {
                    IsPasswordChanged = true;
                    password = value;
                }
            }
        }

        /// <summary>
        /// Becomes <c>true</c> when the <see cref="Password"/> property is set.
        /// </summary>
        public bool IsPasswordChanged { get; private set; } = false;

        /// <summary>
        /// Determines whether LDAP (or equivalent) integration should be used.
        /// </summary>
        public bool? UsingExternalAuth { get; set; }

        /// <summary>
        /// The Person's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The Person's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Person's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The DBID of the Folder location. When creating a new object, this must be set to the correct DBID of the target Folder.
        /// </summary>
        /// <remarks>
        /// If zero, the object will be saved under "Persons" (the default folder).
        /// </remarks>
        public int? FolderId { get; set; }

        /// <summary>
        /// Full or partial CME object location path.
        /// </summary>
        /// <remarks>
        /// Required for Update.
        /// </remarks>
        public string FolderPath { get; set; }

        /// <summary>
        /// Flag indicating the Person is an Agent. Can only be set on creation.
        /// </summary>
        public bool IsAgent { get; set; }

        /// <summary>
        /// Name of the Config Server object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The state of the Config Server object.
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
