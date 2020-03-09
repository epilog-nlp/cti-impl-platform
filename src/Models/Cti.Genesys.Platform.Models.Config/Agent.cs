// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models.Config;
using Genesyslab.Platform.ApplicationBlocks.ConfigurationObjectModel.CfgObjects;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Models.Config
{
    /// <summary>
    /// A specialized <see cref="Person"/> with Agent functionality.
    /// </summary>
    public class Agent : Person, IAgent
    {
        /// <summary>
        /// DBID of the default Place object assigned to the Agent.
        /// </summary>
        public int DefaultPlaceDbid { get; set; }

        /// <summary>
        /// DBID of the Capacity Rule assigned to the Agent.
        /// </summary>
        public int CapacityRuleDbid { get; set; }

        /// <summary>
        /// DBID of the Contract assigned to the Agent.
        /// </summary>
        public int ContractDbid { get; set; }

        /// <summary>
        /// The Agent Logins assigned to this Agent.
        /// </summary>
        public IEnumerable<IAgentLoginAssociation> Logins { get; set; } = Enumerable.Empty<IAgentLoginAssociation>();

        /// <summary>
        /// The Skills and associated proficiencies assigned to this Agent.
        /// </summary>
        public IEnumerable<IAgentSkillLevel> Skills { get; set; } = Enumerable.Empty<IAgentSkillLevel>();
    }

    /// <summary>
    /// A Genesys Config Server association between <see cref="Agent"/> and Login.
    /// </summary>
    public class AgentLoginAssociation : ConfigObject<CfgAgentLoginInfo>, IAgentLoginAssociation
    {
        /// <summary>
        /// DBID of the Agent Login object assigned to the Agent.
        /// </summary>
        public int AgentLoginDbid { get; set; }

        /// <summary>
        /// Wrap-up time (seconds) associated with the Login Id.
        /// </summary>
        public int WrapupTime { get; set; }
    }

    /// <summary>
    /// An association between an <see cref="Agent"/> and a Skill.
    /// </summary>
    public class AgentSkillLevel : ConfigObject<CfgSkillLevel>, IAgentSkillLevel
    {
        /// <summary>
        /// DBID of the associated Skill object.
        /// </summary>
        public int SkillDbid { get; set; }

        /// <summary>
        /// The level of proficiency in the Skill.
        /// </summary>
        public int Level { get; set; }
    }
}
