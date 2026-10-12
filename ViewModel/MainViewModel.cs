using NumberConverter.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NumberConverter.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private string _targetText;

		public double SourceSystemValue { get; set; }
		public double FiniteSystemValue { get; set; }
		public string StartNumberText { get; set; }

		public ICommand ButtonTransform { get; }

		public MainViewModel()
		{
			ButtonTransform = new RelayCommand(TransformNumber);
			SourceSystemValue = 2;
			FiniteSystemValue = 2;
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
				string result = NumberConverter.Converter.Calculate(startNumber, (int)startSystem, (int)endSystem);
				TargetText = result;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
