using System.Linq;
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
        private bool _sumaPresionada;
        private bool _divisionPresionada;
        private bool _multiplicacionPresionada;
        private bool _restaPresionada;
        #endregion

        #region CONSTRUCTOR
        public VMpage1(INavigation navigation)
        {
            Navigation = navigation;
            ResetearCalculadora();
        }
        #endregion

        #region OBJETOS
        public bool DivisionPresionada
        {
            get { return _divisionPresionada; }
            set
            {
                if (_divisionPresionada != value)
                {
                    SetValue(ref _divisionPresionada, value);
                    OnPropertyChanged(nameof(DivisionPresionada));
                }
            }
        }

        public bool MultiplicacionPresionada
        {
            get { return _multiplicacionPresionada; }
            set
            {
                if (_multiplicacionPresionada != value)
                {
                    SetValue (ref _multiplicacionPresionada, value);
                    OnPropertyChanged(nameof(MultiplicacionPresionada));
                }
            }
        }

        public bool RestaPresionada
        {
            get { return _restaPresionada; }
            set
            {
                if (_restaPresionada != value)
                {
                    SetValue(ref _restaPresionada, value);
                    OnPropertyChanged(nameof(RestaPresionada));
                }
            }
        }

        public bool SumaPresionada
        {
            get {return _sumaPresionada; }
            set
            {
                if (_sumaPresionada != value)
                {
                    SetValue(ref _sumaPresionada , value);
                    OnPropertyChanged(nameof(SumaPresionada));
                }
            }
        }

        public double Valor1
        {
            get { return _valor1; }
            set { SetValue(ref _valor1, value);}
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

                
                switch (operador)
                {
                    case "+":
                        SumaPresionada = true;
                        break;
                    case "-":
                        RestaPresionada = true;
                        break;
                    case "x":
                        MultiplicacionPresionada = true;
                        break;
                    case "/":
                        DivisionPresionada = true;
                        break;
                }
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
            SumaPresionada = false;
            RestaPresionada = false;
            MultiplicacionPresionada = false;
            DivisionPresionada = false;
            
        }

        private void ResetearCalculadora()
        {
            ResultadoTexto = "0";
            OperacionTexto = "";
            Valor1 = 0;
            Valor2 = 0;
            _operador = "";
            SumaPresionada = false;
            RestaPresionada = false;
            MultiplicacionPresionada = false;
            DivisionPresionada = false;
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
        #endregion

        #region COMANDOS
        public ICommand NumeroCommand => new Command<string>(Numero);
        public ICommand OperacionCommand => new Command<string>(Operacion);
        public ICommand IgualCommand => new Command(Igual);
        public ICommand LimpiarPantallaCommand => new Command(ResetearCalculadora);
        public ICommand BorrarUnNumeroCommand => new Command(BorrarUnNumero);
        #endregion
    }
}
