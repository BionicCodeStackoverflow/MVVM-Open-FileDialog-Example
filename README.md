# MVVM-Open-FileDialog-Example

Example relates to the Stackoverflow answer [Open File Dialog MVVM](https://stackoverflow.com/a/64861760/3141792)

This examples shows ways how to open a file dialog or dialogs in general and how to handle the result in an MVVM conform manner.

Each solution is hosted in a separate directory.

Solution 1: Very simple and basic scenario, that meets the exact requirements of the question.
Solution 2: Solution that enables to use data validation in the view model. To keep the example simple, the implementation of INotifyDataErrorInfo is omitted.
Solution 3: Another, more elegant solution that uses an ICommand and the ICommandSource.CommandParameter to send the dialog result to the view model and execute the persistence operation.
