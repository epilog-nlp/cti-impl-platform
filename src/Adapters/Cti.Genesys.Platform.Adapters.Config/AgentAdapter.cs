// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using Platform.Models.Config;
using System;
using System.Linq;

namespace Cti.Platform.Adapters
{
    /// <summary>
    /// Adapters for conversions between PSDK/CTI Agent objects.
    /// </summary>
    public static class AgentAdapter
    {
        /// <summary>
        /// Converts a PSDK Agent object to its CTI equivalent.
        /// </summary>
        /// <remarks>
        /// Duplicates similar code in <see cref="PersonAdapter"/> to avoid clutter.
        /// </remarks>
        /// <param name="agent">The Agent object to convert.</param>
        /// <returns>An <see cref="Agent"/> object equivalent to the provided PSDK object.</returns>
        /// <exception cref="ArgumentException">
        /// Throws if the provided <paramref name="agent"/> is not really an agent.
        /// </exception>
        public static Agent ToAgent(this CfgPerson agent)
            => (agent.IsAgent.IsTrue() ?? false)
            ? throw InvalidAgent(agent, nameof(agent))
            : new Agent
            {
                CapacityRuleDbid = agent.AgentInfo.GetCapacityRuleDBID(),
                ContractDbid = agent.AgentInfo.GetContractDBID(),
                Dbid = agent.DBID,
                DefaultPlaceDbid = agent.AgentInfo.GetPlaceDBID(),
                Email = agent.EmailAddress,
                Enabled = agent.State.IsEnabled(),
                ExternalId = agent.ExternalID,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Logins = agent.AgentInfo.AgentLogins?
                    .Select(ToLoginAssociation)
                    .ToList(),
                FolderId = agent.FolderId,
                FolderPath = agent.ObjectPath,
                IsAgent = agent.IsAgent.IsTrue() ?? false,
                Name = agent.UserName,
                Skills = agent.AgentInfo.SkillLevels?
                    .Select(ToSkillLevel)
                    .ToList(),
                TenantDbid = agent.GetTenantDBID(),
                UsingExternalAuth = agent.IsExternalAuth.IsTrue()
            };

        /// <summary>
        /// Converts a PSDK Agent Login Info object to its CTI equivalent.
        /// </summary>
        /// <param name="login">The Agent Login Info object to convert.</param>
        /// <returns>An <see cref="AgentLoginAssociation"/> object equivalent to the provided PSDK object.</returns>
        internal static AgentLoginAssociation ToLoginAssociation(this CfgAgentLoginInfo login)
            => new AgentLoginAssociation
            {
                AgentLoginDbid = login.GetAgentLoginDBID().Value,
                WrapupTime = login.WrapupTime
            };

        /// <summary>
        /// Converts a PSDK Skill Level object to its CTI eqivalent.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>An <see cref="AgentSkillLevel"/> object equivalent to the provided PSDK object.</returns>
        internal static AgentSkillLevel ToSkillLevel(this CfgSkillLevel skill)
            => new AgentSkillLevel
            {
                SkillDbid = skill.GetSkillDBID().Value,
                Level = skill.Level
            };

        private static ArgumentException InvalidAgent(CfgPerson person, string paramName)
            => new ArgumentException($"The provided Person object ({person.UserName}:{person.DBID}) is not an Agent, and is invalid for this operation.", paramName);
    }
}
