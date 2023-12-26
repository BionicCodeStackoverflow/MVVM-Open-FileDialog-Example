# MVVM-Open-FileDialog-Example
![BC](https://img.shields.io/badge/.NET-5-informational) ![BC](https://img.shields.io/badge/.NET-6-informational) ![BC](https://img.shields.io/badge/.NET-7-informational) ![BC](https://img.shields.io/badge/.NET-8-informational)  
![BC](https://img.shields.io/badge/-WPF-informational?logo=windows)

Example relates to the Stackoverflow answer [Open File Dialog MVVM](https://stackoverflow.com/a/64861760/3141792)

This examples shows ways how to open a file dialog or dialogs in general and how to handle the result in an MVVM conform manner.

Each solution is hosted in a separate directory.

__Solution 1:__ Very simple and basic scenario, that meets the exact requirements of the question.  

__Solution 2:__ Solution that enables to use data validation in the _view model_ ([How to add validation to view model properties or how to implement `INotifyDataErrorInfo`](https://stackoverflow.com/a/56608064/3141792)). 

__Solution 3:__ Another, more elegant solution that uses an `ICommand` and the `ICommandSource.CommandParameter` to send the dialog result to the _view model_ and execute the persistence operation in the _model_.
