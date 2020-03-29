// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.Configuration.Protocols.Types;

namespace Cti.Platform.Adapters
{
    /// <summary>
    /// Adapters for converting between Config Server PSDK enums and POCO types.
    /// </summary>
    public static class EnumAdapter
    {
        /// <summary>
        /// Determines if an object's state is enabled.
        /// </summary>
        /// <param name="state">The state of a configuration object.</param>
        /// <returns>True if <paramref name="state"/> has the <see cref="CfgObjectState.CFGEnabled"/> value.</returns>
        public static bool? IsEnabled(this CfgObjectState? state)
            => state is null
            ? default(bool?)
            : state.Value == CfgObjectState.CFGEnabled;

        /// <summary>
        /// Determines if a flag is true.
        /// </summary>
        /// <param name="flag">The flag to check.</param>
        /// <returns>True if the <paramref name="flag"/> has the <see cref="CfgFlag.CFGTrue"/> value.</returns>
        public static bool? IsTrue(this CfgFlag? flag)
            => flag is null
            ? default(bool?)
            : flag.Value == CfgFlag.CFGTrue;
    }
}
