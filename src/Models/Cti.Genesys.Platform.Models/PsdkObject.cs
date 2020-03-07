// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models;
using System;

namespace Platform.Models
{
    /// <summary>
    /// An item with a direct correlation to a PSDK object.
    /// </summary>
    /// <typeparam name="TPsdk">The PSDK object Type.</typeparam>
    public abstract class PsdkObject<TPsdk> : GenesysObject, IPsdkObject<TPsdk>
    {
        /// <summary>
        /// The Genesys PSDK object Type this item corresponds to.
        /// </summary>
        public Type PsdkType => typeof(TPsdk);
    }
}
