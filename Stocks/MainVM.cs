using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Stocks
{
    public class MainVM : BaseVM
    {
        public ICommand ChangeToAdvanced { get; private set; }

        private RegularVM _regularVM;
        private AdvancedVM _advancedVM;

        public MainVM() 
        {
            _regularVM = new RegularVM();
            _advancedVM = new AdvancedVM();

            this.CurrentVM = _regularVM;

            this.ChangeToAdvanced = new DelegateCommand(o => this.ChangeViewToAdvanced());
        }

        private void ChangeViewToAdvanced()
        {
            if (this.CurrentVM is RegularVM)
            {
                this.CurrentVM = _advancedVM;
            }
            else if (this.CurrentVM is AdvancedVM)
            {
                this.CurrentVM = _regularVM;
            }            
        }

        private BaseVM _currentVM;
        public BaseVM CurrentVM
        {
            get { return _currentVM; }
            set
            {
                _currentVM = value;
                OnPropertyChanged("CurrentVM");
            }
        }        
    }
}
