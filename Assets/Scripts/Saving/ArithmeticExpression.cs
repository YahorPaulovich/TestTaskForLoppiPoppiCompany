using System;

[Serializable]
public class ArithmeticExpression
{
    public string Expression { get; set; }

    public ArithmeticExpression()
    {
        Expression = string.Empty;
    }

    public ArithmeticExpression(string expression)
    {
        Expression = expression;
    }
}
