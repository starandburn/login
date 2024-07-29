namespace Nkk.IT.Trial.Programing.Login.Models
{
    public class Variable
    {
        public static readonly Variable Empty = new();
        public object? Value { get; } = null;
        public override string ToString() => Value?.ToString() ?? string.Empty;
        public static implicit operator string(Variable var) => var.ToString();
        public static implicit operator int(Variable var) => int.TryParse(var, out var result) ? result : default;
        public static implicit operator decimal(Variable var) => decimal.TryParse(var, out var result) ? result : default;
        public static implicit operator double(Variable var) => double.TryParse(var, out var result) ? result : default;
        public static implicit operator bool(Variable var) => bool.TryParse(var, out var result) ? result : (int)var > 0;
        public static implicit operator Variable(string? str) => new Variable(str);
        public static implicit operator Variable(decimal dec) => new Variable(dec);
        public static implicit operator Variable(int n) => new Variable(n);
        public static implicit operator Variable(double d) => new Variable(d);
        public static implicit operator Variable(bool b) => new Variable(b);

        public Variable(object? value = null)
        {
            Value = value;
        }
    }
}
