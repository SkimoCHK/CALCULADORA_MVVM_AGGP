using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CALCU_MVVM_AGGP.ViewModel
{
    public class VMpage1 : BaseViewModel
    {
        #region VARIABLES
        private double _Valor1;
        private double _Valor2;
        private double _Valor3;
        private string _Label1;
        private string _Label2;
        private bool _LimpiarPantalla;
        #endregion

        #region CONSTRUCTOR
        public VMpage1(INavigation navigation) 
        {
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS
        public double Valor1
        {
            get { return _Valor1;  }
            set { SetValue(ref _Valor1, value); }
        }
        public double Valor2
        {
            get { return _Valor2; }
            set { SetValue(ref _Valor2, value);}
        }
        public double Valor3
        {
            get { return _Valor3; }
            set { SetValue(ref _Valor3, value); }
            
        }

        #endregion

        #region PROCESOS

        #endregion

        #region COMANDOS
        #endregion

    }
}
