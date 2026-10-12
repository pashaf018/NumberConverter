using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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
			bool negative = startNumber[0] == '-';

			if (startNumber[0] == '-' || startNumber[0] == '+')
			{
				startNumber.Remove(0, 1);
			}

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

			return numberToReturn * (negative ? -1 : 1);
		}

		private static string ConvertToOther(int TenNumber, int endSystem)
		{
			string result = "";
			bool negative = false;
			
			if (TenNumber < 0)
			{
				TenNumber = Math.Abs(TenNumber);
				negative = true;
			}

			while (TenNumber != 0)
			{
				result += TenNumber % endSystem;
				TenNumber /= endSystem;
			}

			if (result.Length == 0)
			{
				return "0";
			}

			char[] arr = result.ToCharArray();
			Array.Reverse(arr);
			string reversed = new string(arr);

			string toReturn = (negative ? "-" : "") + reversed;

			if (endSystem == 2)
			{
				int diff = (int)RoundUpToPowerOf2((uint)reversed.Length) - reversed.Length;
				string add = (negative ? "1" : "0") + new string('0', diff - 1);
				toReturn += " ПК " + add + reversed;
			}

			return toReturn;
		}

		private static uint RoundUpToPowerOf2(uint value)
		{
			if (value == 0) return 1;
			value--;
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return value + 1;
		}
	}
}
