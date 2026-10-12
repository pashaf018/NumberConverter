using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace NumberConverter
{
	public static class Converter
	{
		private static string charBase = "0123456789ABCDEF";

		public static string CalculateForTextBlocks(string startNumber, int startSystem, int endSystem)
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

			if (endSystem == 2)
			{
				result += " ПК " + FillZeroes(result);
			}

			return result;
		}

		internal static int ConvertToTen(string startNumber, int startSystem)
		{
			bool negative = startNumber[0] == '-';

			if (startNumber[0] == '-' || startNumber[0] == '+')
			{
				startNumber = startNumber.Remove(0, 1);
			}

			int numberToReturn = 0;

			if (startSystem == 10 && int.TryParse(startNumber,out numberToReturn))
			{
				return numberToReturn * (negative ? -1 : 1);
			}

			numberToReturn = ConvertingLogic(startNumber, startSystem);

			return numberToReturn * (negative ? -1 : 1);
		}

		internal static string ConvertToOther(int TenNumber, int endSystem)
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
				result += charBase[TenNumber % endSystem];
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

			return toReturn;
		}

		private static uint RoundUpToPowerOf2(uint value)
		{
			if (value == 0) return 1;
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return value + 1;
		}

		private static int ConvertingLogic(string startNumber, int startSystem)
		{
			int i = startNumber.Length - 1;
			int numberToReturn = 0;

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

				numberToReturn += charBase.IndexOf(c) * (int)Math.Pow(startSystem, i);
				i--;
			}

			return numberToReturn;
		}

		internal static string FillZeroes(string num)
		{
			bool negative = num[0] == '-';
			bool skipFirst = num[0] == '+' || num[0] == '-';

			int closest = (int)RoundUpToPowerOf2((uint)(num.Length - (skipFirst ? 1 : 0)));
			if (closest < 4)
			{
				closest = 4;
			}

			int diff = closest - (num.Length - (skipFirst ? 1 : 0));
			string add = (negative ? "1" : "0") + new string('0', diff - 1);

			return add + (skipFirst ? num.Substring(1) : num);
		}
	}
}
