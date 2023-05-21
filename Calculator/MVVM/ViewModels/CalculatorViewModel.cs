using PropertyChanged;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels

{
    [AddINotifyPropertyChangedInterface]
    public class CalculatorViewModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public double Number3 { get; set; }
        public string Operation1 { get; set; }
        public string Operation2 { get; set; }
        public string Operation3 { get; set; }
        public string Result { get; set; }
        public string MathExpression { get; set; }

        public ICommand AddCommand => new Command(() => Result = CalculateMathOperation());
        public ICommand AddCommandClear => new Command(() => ClearData());
        public ICommand AddCommandNumber1 => new Command((key) => MathExpression = AppendToMathExpression(key.ToString()));
        public ICommand AddCommandDelete => new Command(() => DeleteLastCharacter());

        private string CalculateMathOperation()
        {
            string operation = MathExpression;
            DataTable dt = new DataTable();
            return dt.Compute(operation, "").ToString();
        }

        private void ClearData()
        {
            Result = "0";
            MathExpression = string.Empty;
        }

        private string AppendToMathExpression(string key)
        {
            string expression = MathExpression;

            if (string.IsNullOrEmpty(expression))
            {
                expression = key;
            }
            else
            {
                string lastCharacter = expression.Substring(expression.Length - 1, 1);
                bool isLastCharacterDigit = char.IsDigit(lastCharacter[0]);
                bool isKeyDigit = char.IsDigit(key[0]);

                expression += (isLastCharacterDigit && isKeyDigit) ? key : $" {key}";
            }

            return expression;
        }


        private void DeleteLastCharacter()
        {
            if (!string.IsNullOrEmpty(MathExpression))
            {
                MathExpression = MathExpression[..^1];
            }
        }
    }
}