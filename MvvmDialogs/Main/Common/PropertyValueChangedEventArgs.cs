namespace MvvmDialogs.Main.Common
{
  using System.ComponentModel;
  using System.Windows.Media.Converters;

  internal class PropertyValueChangedEventArgs : PropertyChangedEventArgs
  {
    public PropertyValueChangedEventArgs(object? oldValue, object? newValue, string? propertyName) : base(propertyName)
    {
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }

    public object? OldValue { get; }
    public object? NewValue { get; }
  }
}