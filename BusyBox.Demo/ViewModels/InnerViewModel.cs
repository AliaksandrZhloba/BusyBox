using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using BusyBox.Demo.Commands;
using BusyBox.ViewModels;


namespace BusyBox.Demo.ViewModels
{
    public class InnerViewModel : BaseBusyBoxViewModel
    {
        private double _scale = 1;

        public ICommand StartProcess { get; }

        public double Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        public InnerViewModel()
        {
            StartProcess = new RelayCommandAsync(StartProcessExecute);
        }


        private async Task StartProcessExecute()
        {
            using (new MessageBusyBox(this, "Scaling..."))
            {
                for (int i = 0; i < 3; i++)
                {
                    while (Scale < 2)
                    {
                        Scale += 0.1;
                        await Task.Delay(5);
                    }

                    while (Scale > 1)
                    {
                        Scale -= 0.1;
                        await Task.Delay(5);
                    }

                }
            }
        }
    }
}
