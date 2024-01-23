using System.Windows.Input;
using Xamarin.Forms;

namespace CALCU_MVVM_AGGP.ViewModel
{
    public class VMpage1 : BaseViewModel
    {
        #region VARIABLES
        private double _valor1;
        private double _valor2;
        private string _operador;
        private bool _limpiarPantalla;
        private string _resultadoTexto;
        private string _operacionTexto;
        #endregion
        #region CONSTRUCTOR
        public VMpage1(INavigation navigation)
        {
            Navigation = navigation;
            _resultadoTexto = "0";
            _operacionTexto = "";
        }
        #endregion
        #region OBJETOS
        public double Valor1
        {
            get { return _valor1; }
            set { SetValue(ref _valor1, value); }
        }

        public double Valor2
        {
            get { return _valor2; }
            set { SetValue(ref _valor2, value); }
        }

        public string ResultadoTexto
        {
            get { return _resultadoTexto; }
            set { SetValue(ref _resultadoTexto, value); }
        }

        public string OperacionTexto
        {
            get { return _operacionTexto; }
            set { SetValue(ref _operacionTexto, value); }
        }
        #endregion

        #region PROCESOS
        private void Numero(string numero)
        {
            if (_limpiarPantalla)
            {
                ResultadoTexto = "0";
                _limpiarPantalla = false;
            }

            if (ResultadoTexto == null)
            {
                ResultadoTexto = numero;
            }
            else if (!ResultadoTexto.Contains(".") || numero != ".")
            {
                ResultadoTexto += numero;
            }

            OperacionTexto += numero;
        }

        private void Operacion(string operador)
        {
            if (!string.IsNullOrEmpty(ResultadoTexto))
            {
                if (!string.IsNullOrEmpty(_operador) && !_limpiarPantalla)
                {
                    Calcular();
                }

                _valor1 = double.Parse(ResultadoTexto);
                _operador = operador;
                _limpiarPantalla = true;

                OperacionTexto = $"{_valor1} {_operador} ";
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

        private void LimpiarPantalla()
        {
            ResultadoTexto = "0";
            OperacionTexto = "";
            _valor1 = 0;
            _valor2 = 0;
            _operador = "";
        }

        private void BorrarUnNumero()
        {
            if (ResultadoTexto.Length > 0)
            {
                ResultadoTexto = ResultadoTexto.Substring(0, ResultadoTexto.Length - 1);
            }
            if (ResultadoTexto == "")
            {
                ResultadoTexto = "0";
            }
        }

        private void Calcular()
        {
            if (double.TryParse(ResultadoTexto, out double resultadoNumerico))
            {
                _valor2 = resultadoNumerico;
                ResultadoTexto = RealizarOperacion(_valor1, _valor2, _operador).ToString();
                _limpiarPantalla = true;
            }
            else
            {
                // Manejar el error, por ejemplo, mostrar un mensaje de error o establecer ResultadoTexto en un valor predeterminado.
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
                    if (num2 != 0)
                    {
                        return num1 / num2;
                    }
                    else
                    {
                        return double.NaN;
                    }
                default:
                    return num2;
            }
        }
        #endregion

        #region COMANDOS
        public ICommand NumeroCommand => new Command<string>(Numero);
        public ICommand OperacionCommand => new Command<string>(Operacion);
        public ICommand IgualCommand => new Command(Igual);
        public ICommand LimpiarPantallaCommand => new Command(LimpiarPantalla);
        public ICommand BorrarUnNumeroCommand => new Command(BorrarUnNumero);
        #endregion
    }
}
