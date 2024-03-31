using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MvvmDialogs.Main.Common
{
  /// <inheritdoc />
  [Serializable]
  public class WrongEventHandlerSignatureException : Exception
  {
    /// <inheritdoc />
    public WrongEventHandlerSignatureException()
    {
    }

    /// <inheritdoc />
    public WrongEventHandlerSignatureException(string message) : base(message)
    {
    }

    /// <inheritdoc />
    public WrongEventHandlerSignatureException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <inheritdoc />
    protected WrongEventHandlerSignatureException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
    {
    }
  }
}
