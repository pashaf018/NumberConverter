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
		private string _text;
		private string _targetText;

		public ICommand ButtonTransform { get; }

		public MainViewModel()
		{
			ButtonTransform = new RelayCommand(TransformNumber);
		}

		public string Text
		{
			get => _text;
			set
			{
				if (_text != value)
				{
					_text = value;
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
					_text += value;
					OnPropertyChanged();
				}
			}
		}

		private void TransformNumber(object parameter)
		{

			if (parameter is object[] parameters)
			{
				double startSystem = (double)parameters[0];
				double endSystem = (double)parameters[1];
				string startNumber = (string)parameters[2];

				try
				{
					string result = NumberConverter.Converter.Calculate(startNumber, (int)startSystem, (int)endSystem);

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}
