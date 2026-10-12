using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Model
{
	public static class Calculator
	{
		public static string[] Calculate(string num1, string num2, int sourceSystem, Operations op)
		{
			if (sourceSystem < 2 || sourceSystem > 16)
			{
				throw new ArgumentOutOfRangeException("Система счисления может быть только от 2 до 16");
			}

			int value1 = Converter.ConvertToTen(num1, sourceSystem);
			int value2 = Converter.ConvertToTen(num2, sourceSystem);

			int endValue1 = Convert.ToInt32(Converter.ConvertToOther(value1, 2), 2);
			int endValue2 = Convert.ToInt32(Converter.ConvertToOther(value2, 2), 2);


			int BinaryResult = 0;
			switch (op)
			{
				case Operations.Addition:
					BinaryResult = Add(endValue1, endValue2);
					break;
				case Operations.Substraction:
					BinaryResult = Substract(endValue1, endValue2);
					break;
				case Operations.Multiplication:
					BinaryResult = Multiply(endValue1, endValue2);
					break;
				case Operations.Division:
					BinaryResult = Divide(endValue1	, endValue2);
					break;
			}

			int TenResult = Converter.ConvertToTen(Convert.ToString(BinaryResult,2), 2);

			return new string[] { Converter.FillZeroes(Convert.ToString(BinaryResult,2)), TenResult.ToString() };
		}

		private static int Add(int value1, int value2)
		{
			while (value2 != 0)
			{
				int carry = value1 & value2;
				value1 = value1 ^ value2;
				value2 = carry << 1;
			}
			return value1;
		}

		private static int Substract(int value1, int value2)
		{
			int negB = Add(~value1, 1);
			return Add(value1, negB);
		}

		private static int Multiply(int value1, int value2)
		{
			int result = 0;

			bool negative = (value1 < 0) ^ (value2 < 0);
			value2 = Math.Abs(value2);
			value1 = Math.Abs(value1);

			while (value2 > 0)
			{
				if ((value2 & 1) == 1)
				{
					result = Add(result, value1);
				}

				value1 = value1 << 1;
				value2 = value2 >> 1;
			}

			return negative ? Substract(0, result) : result;
		}

		private static int Divide(int value1, int value2)
		{
			if (value2 == 0)
			{
				throw new DivideByZeroException("Делитель оказался нулём");
			}

			bool negative = (value1 < 0) ^ (value2 < 0);
			value1 = Math.Abs(value1);
			value2 = Math.Abs(value2);

			int result = 0;
			while (value1 >= 0)
			{
				value1 = Substract(value1, value2);
				result = Add(result, 1);
			}

			return negative ? Substract(0, result) : result;
		}
	}
}
