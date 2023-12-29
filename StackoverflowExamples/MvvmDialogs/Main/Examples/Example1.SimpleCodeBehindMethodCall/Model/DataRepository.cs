namespace Main.Examples.Example1.SimpleCodeBehindMethodCall.Model
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows;

  internal class DataRepository
  {
    internal void SaveUsername(string? userName, string? destinationFilePath)
    {
      // Dialog only displayed for show purpose ==> MVVM violation!
      // See example #4 to learn how to trigger the View to show a message.
      //
      // !!! Note that there are really rare edge cases where this is legal in terms of clean application design.
      // Because there is nothing like a "user" or UI in the context of the View Model and Model, there is no need or requirement to show something in the UI to a potential user.
      // When an fatal error has occured like the lost of a network connection you usually must throw an exception (may after a number of reties). This exception can then be handled at View Model level or directly at View level
      // to ask the user to rety the action after troubleshooting the network connection. When the Model can't operate due to a certain illegal state, it must throw.
      // If the user can help the application to recover then handle this exception in the View and communicate to the user what he has to do.
      // If the user can't help then the application must crash (log the exception and send the error report to the developer).
      // I really can't come up with any reasonable scenario that doesn't violate MVVM in terms of responsibilities.
      //
      // Again, if there is nothing like a View (and therefore no think like a user) from the perspective of the View Model and Model,
      // then there can't be any reason for both components to communicate with something like the View (or even the user).
      // For example, if you unit test the Model and the Model for example fails to persist data,
      // then this marks a fatal error. Such as scenario means nothing less than losing valuable user (or business) data.
      // There is nothing the Model can recover from. It can't control e.g. the network cable that has come loose.
      // It can retry the persistence operation but at one point it must crash the application (throw an exception).
      // You don't want the application (the Model) to swallow that fatal error and silently continue - which would be the case if the Model would send a message and conmtiniue.
      //
      // In a scenario utside the unit test a UI can catch that exception to involve the user into the recover and retry procedure, that's fine.
      // But for the Model itself, the only correct behavior is to stop working! The data integrity is lost or the application's state has become unpredictable.
      // There is nothing meaningful the Model (application) can do.
      //
      // Although it is possible to send messages to the UI/user across the MVVM boundaries without violating the MVVM dependency graph (see example #4),
      // such kind of communication will always violate MVVM in terms of responsibilities.
      //
      // There must be a very good reason to break the architecture and design choice imposed by the MVVM design pattern.
      MessageBox.Show("Username saved by Model component!");
    }
  }
}
