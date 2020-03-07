// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Cti.Models;
using Genesyslab.Platform.Commons.Protocols;

namespace Platform.Models
{
    /// <summary>
    /// Represents a Genesys Event received from a Server.
    /// </summary>
    /// <typeparam name="TMessage">The PSDK event message Type.</typeparam>
    public interface IPsdkEvent<TMessage> : IPsdkObject<TMessage>, IServerEvent
        where TMessage : IMessage
    {

    }
}
