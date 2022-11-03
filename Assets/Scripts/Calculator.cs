using System;
using System.Linq;
using System.Numerics;

public class Calculator
{
    public ArithmeticExpression Data;

    public Calculator()
    {
        Data = new ArithmeticExpression();
    }

    public ArithmeticExpression Calculate(ArithmeticExpression data)
    {
        Data = data;
        var expression = data.Expression;
        var length = expression.Length;
        if (length == 0)
        {
            return Data;
        } else if (int.TryParse(expression, out int number))
        {
            return Data;
        }

        if (expression[length - 1] == '/')
        {
            Data.Expression = expression.Remove(length - 1);
        }

        if(length > 2)
        {
            BigInteger[] intArr = expression.Split('/').Select(BigInteger.Parse).ToArray();
            try 
            {
                var resultOfDividing = intArr[0] / intArr[1];
                Data.Expression = resultOfDividing.ToString();
            }
            catch (DivideByZeroException ex)
            {
                throw new DivideByZeroException($"Cannot divide by zero!\n" + ex);
            }
            catch (OverflowException ex)
            {              
                throw new OverflowException($"Value was either too large or too small!\n" + ex);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"The value cannot be parsed!\n" + ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong!\n" + ex);
            }       
        }

        return Data;
    }
}
