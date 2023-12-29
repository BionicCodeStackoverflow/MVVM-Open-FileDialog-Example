namespace Main.Common
{
  public class ApplicationMessageDispatchedEventArgs : EventArgs
  {
    public ApplicationMessageDispatchedEventArgs(string message, MessageSeverity severity)
    {
      this.Message = message;
      this.Severity = severity;
    }

    public string Message { get; }
    public MessageSeverity Severity { get; }
  }
}