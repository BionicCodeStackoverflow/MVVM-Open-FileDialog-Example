namespace MvvmDialogs.Main.Common
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  internal interface IApplicationMessageSource
  {
    event EventHandler<ApplicationMessageDispatchedEventArgs> ApplicationMessageDispatched;
  }
}
