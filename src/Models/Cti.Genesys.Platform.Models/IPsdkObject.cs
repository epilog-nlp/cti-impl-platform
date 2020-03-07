// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models;
using System;

namespace Platform.Models
{
    /// <summary>
    /// Represents an item with a direct correlation to a PSDK object.
    /// </summary>
    /// <typeparam name="TPsdk">The PSDK object Type.</typeparam>
    public interface IPsdkObject<TPsdk> : IGenesysObject
    {
        /// <summary>
        /// The Genesys PSDK object Type this item corresponds to.
        /// </summary>
        Type PsdkType { get; }
    }
}
