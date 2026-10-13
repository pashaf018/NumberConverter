using NumberConverter.Model;
using NumberConverter.MVVM;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NumberConverter.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private string _targetText;
		private string _pkText;
		private Visibility _pkVisibility;

		public double SourceSystemValue { get; set; }
		public double FiniteSystemValue { get; set; }
		public string StartNumberText { get; set; }

		public ICommand ButtonTransform { get; }

		public MainViewModel()
		{
			ButtonTransform = new RelayCommand(TransformNumber);
			SourceSystemValue = 2;
			FiniteSystemValue = 2;
			PKVisibility = Visibility.Hidden;
		}

		public Visibility PKVisibility
		{
			get => _pkVisibility;
			set
			{
				if (_pkVisibility != value)
				{
					_pkVisibility = value;
					OnPropertyChanged();
				}
			}
		}

		public string PKText
		{
			get => _pkText;
			set
			{
				if (_pkText != value)
				{
					_pkText = value;
					OnPropertyChanged();
				}
			}
		}

		public string TargetText
		{
			get => _targetText;
			set
			{
				if (_targetText != value)
				{
					_targetText = value;
					OnPropertyChanged();
				}
			}
		}

		private void TransformNumber(object parameter)
		{
			double startSystem = SourceSystemValue;
			double endSystem = FiniteSystemValue;
			string startNumber = StartNumberText;

			try
			{
				string result = NumberConverter.Converter.CalculateForTextBlocks(startNumber, (int)startSystem, (int)endSystem);
				if (result.Contains("ПК"))
				{
					int sepIndex = result.IndexOf("ПК");
					PKText = result.Substring(sepIndex);
					PKVisibility = Visibility.Visible;
					TargetText = result.Substring(0,sepIndex);
				}
				else
				{
					TargetText = result;
					PKVisibility = Visibility.Hidden;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
