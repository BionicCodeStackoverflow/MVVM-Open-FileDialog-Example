# MVVM-Open-FileDialog-Example
![BC](https://img.shields.io/badge/.NET-5-informational) ![BC](https://img.shields.io/badge/.NET-6-informational) ![BC](https://img.shields.io/badge/.NET-7-informational) ![BC](https://img.shields.io/badge/.NET-8-informational)  
![BC](https://img.shields.io/badge/-WPF-informational?logo=windows)

Example relates to the Stackoverflow answer [Open File Dialog MVVM](https://stackoverflow.com/a/64861760/3141792)

This examples shows ways how to open a file dialog or dialogs in general and how to handle the result in an MVVM conform manner.

Each solution is hosted in a separate directory.

__Solution 1:__ Very simple and basic scenario, that uses code-behind _view model_ method calls in order to pass on the dialog data and execute the persistence operation in the _model_.

__Solution 2:__ Solution that uses data binding to sende the dialog data to the _view model_ and execute the persistence operation in the _model_.


__Solution 3:__ Another, more elegant solution that builds on solution #2 and uses data validation in the _view model_ ([How to add validation to view model properties or how to implement `INotifyDataErrorInfo`](https://stackoverflow.com/a/56608064/3141792)) and an `ICommand` to send the dialog result to the _view model_ and execute the persistence operation in the _model_.

__Solution #4:__ Shows how the _view model_ or _model_ can open a dialog in _view_ by using a messaging infracstructure based on the _Event Aggregator_ design pattern. 
Note, although this solution does not require the _view model_ or _model_ to reference any dialog related types (e.g., a "dialog service") and is a simply event based solution, 
it still violates against the MVVM design rules (component responsibilities) and should be avoided.  
Read the [Important_Note.txt](https://github.com/BionicCodeStackoverflow/MVVM-Open-FileDialog-Example/blob/main/StackoverflowExamples/MvvmDialogs/Main/Examples/Example4.OpenMessageDialogFromViewModel/Important_Note.txt) 
document for further explanations.
