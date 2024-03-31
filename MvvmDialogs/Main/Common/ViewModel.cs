namespace MvvmDialogs.Main.Common
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Reflection;
  using System.Runtime.CompilerServices;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows;
  using JetBrains.Annotations;

  internal abstract class ViewModel : INotifyPropertyChanged, INotifyPropertyValueChanged, INotifyDataErrorInfo
  {
    protected ViewModel()
    {
      this.Errors = new Dictionary<string, IList<object>>();
      this.ValidatedAttributedProperties = new HashSet<string>();
    }

    protected virtual bool TrySet<TValue>(TValue newValue, ref TValue backingField, [CallerMemberName] string? propertyName = null)
      => TrySet(newValue, ref backingField, value => ValidationResult.Success!, propertyName);

    protected virtual bool TrySet<TValue>(TValue newValue, ref TValue backingField, Func<TValue?, ValidationResult?>? validationDelegate, [CallerMemberName] string? propertyName = null)
    {
      if (newValue is TValue value && value.Equals(backingField)
        || newValue is null && backingField is null)
      {
        return false;
      }

      _ = IsValueValid(newValue, propertyName, validationDelegate);

      object? oldValue = backingField;
      backingField = newValue;
      OnPropertyChanged(propertyName);
      OnPropertyValueChanged(oldValue, newValue);

      return true;
    }

    protected virtual bool IsValueValid<TValue>(TValue? newValue, string? propertyName, Func<TValue?, ValidationResult?>? validationDelegate)
    {
      ValidationResult? validationResult = validationDelegate?.Invoke(newValue);
      bool isValid = validationResult == ValidationResult.Success;
      if (propertyName is not null)
      {
        _ = ClearErrors(propertyName);

        if (!isValid)
        {
          AddErrors(propertyName, new[] { validationResult });
        }
      }

      bool allAttributesValid = IsPropertyAttributeValid(newValue, propertyName);

      return isValid && allAttributesValid;
    }

    protected virtual bool IsPropertyAttributeValid<TValue>(TValue value, string? propertyName)
    {
      if (propertyName is null)
      {
        return true;
      }

      _ = this.ValidatedAttributedProperties.Add(propertyName);

      // The result flag
      bool isValueValid = true;

      // Check if property is decorated with validation attributes
      // using reflection
      IEnumerable<Attribute> validationAttributes = GetType()
        .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        ?.GetCustomAttributes(typeof(ValidationAttribute)) ?? new List<Attribute>();

      // Validate using attributes if present
      if (validationAttributes.Any())
      {
        var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateProperty(value, validationContext, validationResults))
        {
          isValueValid = false;
          AddErrors(propertyName, validationResults);
        }
      }

      return isValueValid;
    }

    private void AddErrors(string propertyName, IEnumerable<ValidationResult>? validationResults, bool isWarning = false)
    {
      if (validationResults is null || !validationResults.Any())
      {
        return;
      }

      if (!this.Errors.TryGetValue(propertyName, out IList<object>? propertyErrors))
      {
        propertyErrors = new List<object>();
        this.Errors.Add(propertyName, propertyErrors);
      }

      foreach (ValidationResult validationResult in validationResults)
      {
        object errorContent = validationResult.ErrorMessage ?? string.Empty;

        if (errorContent is IEnumerable<object?> newErrors)
        {
          if (isWarning)
          {
            foreach (object? error in newErrors)
            {
              propertyErrors.Add(error ?? string.Empty);
            }
          }
          else
          {
            foreach (object? error in newErrors)
            {
              propertyErrors.Insert(0, error ?? string.Empty);
            }
          }
        }
        else
        {
          if (isWarning)
          {
            propertyErrors.Add(errorContent);
          }
          else
          {
            propertyErrors.Insert(0, errorContent);
          }
        }
      }

      OnErrorsChanged(propertyName);
    }

    /// <summary>
    /// Removes all error objects related to a property.
    /// </summary>
    /// <param name="propertyName">The property to clear error objects  for.</param>
    /// <returns><c>true</c> if an item was removed or <c>false</c> if no item was removed or the property was not found.</returns>
    protected virtual bool ClearErrors(string propertyName)
    {
      _ = this.ValidatedAttributedProperties.Remove(propertyName);
      bool hasRemovedItem = this.Errors.Remove(propertyName);
      if (hasRemovedItem)
      {
        OnErrorsChanged(propertyName);
      }

      return hasRemovedItem;
    }

    public virtual bool PropertyHasError([CallerMemberName] string? propertyName = null)
      => propertyName is null ? this.HasErrors : this.Errors.ContainsKey(propertyName);

    public IEnumerable GetErrors(string? propertyName = null) =>
        string.IsNullOrWhiteSpace(propertyName)
          ? this.Errors.SelectMany(entry => entry.Value)
          : this.Errors.TryGetValue(propertyName, out IList<object>? errors)
            ? errors
            : Enumerable.Empty<object>();

    public IEnumerable<string> GetPropertyErrors(string? propertyName = null) => GetErrors(propertyName).Cast<string>();

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
      => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected virtual void OnPropertyValueChanged(object? oldValue, object? newValue, [CallerMemberName] string? propertyName = null)
      => this.PropertyValueChanged?.Invoke(this, new PropertyValueChangedEventArgs(oldValue, newValue, propertyName));

    protected virtual void OnErrorsChanged(string propertyName)
      => this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyValueChangedEventHandler? PropertyValueChanged;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => this.Errors.Any();
    private Dictionary<string, IList<object>> Errors { get; }
    private HashSet<string> ValidatedAttributedProperties { get; }
  }
}
