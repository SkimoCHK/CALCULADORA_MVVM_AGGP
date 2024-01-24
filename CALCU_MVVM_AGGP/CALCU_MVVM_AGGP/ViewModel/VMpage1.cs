using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CALCU_MVVM_AGGP.ViewModel
{
    public class VMpage1 : BaseViewModel
    {
        private double _valor1;
        private double _valor2;
        private string _operador;
        private bool _limpiarPantalla;
        private string _resultadoTexto;
        private string _operacionTexto;


        public VMpage1(INavigation navigation)
        {
            Navigation = navigation;
            ResetearCalculadora();
        }

        public double Valor1
        {
            get => _valor1;
            set => SetValue(ref _valor1, value);
        }

        public double Valor2
        {
            get => _valor2;
            set => SetValue(ref _valor2, value);
        }

        public string ResultadoTexto
        {
            get => _resultadoTexto;
            set => SetValue(ref _resultadoTexto, value);
        }

        public string OperacionTexto
        {
            get => _operacionTexto;
            set => SetValue(ref _operacionTexto, value);
        }

        public ICommand NumeroCommand => new Command<string>(Numero);
        public ICommand OperacionCommand => new Command<string>(Operacion);
        public ICommand IgualCommand => new Command(Igual);
        public ICommand LimpiarPantallaCommand => new Command(ResetearCalculadora);
        public ICommand BorrarUnNumeroCommand => new Command(BorrarUnNumero);

        private void Numero(string numero)
        {
            if (_limpiarPantalla)
            {
                // No limpiar completamente, solo reiniciar el número actual
                ResultadoTexto = "0";
                _limpiarPantalla = false;
            }

            if (!string.IsNullOrEmpty(_operacionTexto) && "+-x/".Contains(_operacionTexto.Last()))
            {
                _operacionTexto += " ";
            }

            if (ResultadoTexto == "0" && numero != ".")
            {
                
                ResultadoTexto = numero;
            }
            else if (!ResultadoTexto.Contains(".") || numero != ".")
            {
                ResultadoTexto += numero;
            }

            _operacionTexto += numero;
        }


        private void Operacion(string operador)
        {
            if (!string.IsNullOrEmpty(ResultadoTexto))
            {
                if (!_limpiarPantalla)
                {
                    Calcular();
                    Valor1 = double.Parse(ResultadoTexto);
                    OperacionTexto = $"{Valor1} {_operador}";
                }

                _operador = operador;
                _limpiarPantalla = true;
                OperacionTexto = $"{Valor1} {_operador}";
            }
            else
            {
                ResultadoTexto = "Error";
            }
        }


        private void Igual()
        {
            Calcular();
            _operador = "";
        }

        private void ResetearCalculadora()
        {
            ResultadoTexto = "0";
            OperacionTexto = "";
            Valor1 = 0;
            Valor2 = 0;
            _operador = "";
        }

        private void BorrarUnNumero()
        {
            if (ResultadoTexto.Length > 0)
            {
                ResultadoTexto = ResultadoTexto.Substring(0, ResultadoTexto.Length - 1);
            }

            if (string.IsNullOrEmpty(ResultadoTexto))
            {
                ResultadoTexto = "0";
            }
        }

        private void Calcular()
        {
            if (double.TryParse(ResultadoTexto, out double resultadoNumerico))
            {
                Valor2 = resultadoNumerico;

                // Utilizamos el operador almacenado en "_operador" en lugar de "+"
                ResultadoTexto = RealizarOperacion(Valor1, Valor2, _operador).ToString();
                _limpiarPantalla = false;
            }
            else
            {
                ResultadoTexto = "Error";
            }
        }


        private double RealizarOperacion(double num1, double num2, string operacion)
        {
            switch (operacion)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "x":
                    return num1 * num2;
                case "/":
                    return num2 != 0 ? num1 / num2 : double.NaN;
                default:
                    return num2;
            }
        }
    }
}
