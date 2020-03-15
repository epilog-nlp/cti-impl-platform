// This source file is under MIT License (MIT).
// See the LICENSE file in the project root for more information.

using Genesyslab.Platform.Commons.Protocols;
using System;

namespace Cti.Platform
{
    using Protocols.Contracts;

    /// <summary>
    /// An object that can manage the state of an <see cref="IProtocol"/> connection.
    /// </summary>
    public abstract class ProtocolConnectionManager<TProtocol> : IConnectionManager<TProtocol>
        where TProtocol : IProtocol
    {
        /// <summary>
        /// Opens the protocol connection, executing (optional) provided delegates on completion.
        /// </summary>
        /// <param name="onOpenDelegates">Delegates to invoke on completion.</param>
        public virtual void Open(params Action[] onOpenDelegates)
        {
            Protocol.Open();
            foreach (var del in onOpenDelegates)
                del();
        }

        /// <summary>
        /// Closes the protocol connection, executing (optional) provided delegates on completion.
        /// </summary>
        /// <param name="onCloseDelegates">Delegates to invoke on completion.</param>
        public virtual void Close(params Action[] onCloseDelegates)
        {
            Protocol.Close();
            foreach (var del in onCloseDelegates)
                del();
        }

        /// <summary>
        /// Creates an instance of this ConnectionManager using the provided <paramref name="protocol"/>.
        /// </summary>
        /// <param name="protocol">The protocol this ConnectionManager delegates requests to.</param>
        /// <returns>An instance of this ConnectionManager, created using the provided <paramref name="protocol"/>.</returns>
        public abstract ICtiFeature Create(TProtocol protocol);

        /// <summary>
        /// The Protocol object requests are delegated to.
        /// </summary>
        protected TProtocol Protocol { get; set; }

        /// <summary>
        /// Name of this implementation. Used to distinguish from other implementations with the same signature.
        /// </summary>
        public virtual string Name => "";

    }
}
