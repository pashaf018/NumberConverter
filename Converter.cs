using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter
{
	public static class Converter
	{
		private static string charBase = "0123456789ABCDEF";

		public static string Calculate(string startNumber, int startSystem, int endSystem)
		{
			if (startSystem < 2 || startSystem > 16 || endSystem < 2 || endSystem > 16)
			{
				throw new ArgumentOutOfRangeException("Система счисления должна быть в диапазоне 2-16");
			}

			if (startSystem == endSystem)
			{
				return startNumber;
			}

			int TenNumber = ConvertToTen(startNumber, startSystem);
			if (endSystem == 10)
			{
				return TenNumber.ToString();
			}

			string result = ConvertToOther(TenNumber, endSystem);

			return result;
		}

		private static int ConvertToTen(string startNumber, int startSystem)
		{
			int numberToReturn = 0;
			int i = startNumber.Length - 1;
			foreach (char c in startNumber)
			{
				if (!charBase.Contains(c))
				{
					throw new ArgumentException("Можно задать число только от 2 до 16 систем счисления");
				}

				if (charBase.IndexOf(c) > startSystem - 1)
				{
					throw new ArgumentException($"Система счисления: \"{startSystem}\" не содержит \"{c}\"");
				}

				numberToReturn += charBase.IndexOf(c) * startSystem ^ i;
			}

			return numberToReturn;
		}

		private static string ConvertToOther(int TenNumber, int endSystem)
		{
			throw new NotImplementedException();
		}
	}
}
