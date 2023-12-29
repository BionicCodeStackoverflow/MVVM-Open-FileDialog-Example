namespace Main.Common
{
  internal interface INotifyPropertyValueChanged
  {
    event PropertyValueChangedEventHandler PropertyValueChanged;
  }
}