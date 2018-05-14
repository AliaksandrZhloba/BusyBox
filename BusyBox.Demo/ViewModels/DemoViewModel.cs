using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using BusyBox.Demo.Commands;
using BusyBox.ViewModels;


namespace BusyBox.Demo.ViewModels
{
    public class DemoViewModel : BaseBusyBoxViewModel
    {
        private string _items;

        public string Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public InnerViewModel InnerViewModel { get; }

        public ICommand LoadWithMessage { get; }
        public ICommand LoadWithCancellation { get; }
        public ICommand LoadWithNestedScopes { get; }
        public ICommand LoadWithCustomBusyBox { get; }


        public DemoViewModel()
        {
            InnerViewModel = new InnerViewModel();

            LoadWithMessage = new RelayCommandAsync(LoadWithMessageExecute);
            LoadWithCancellation = new RelayCommandAsync(LoadWithCancellationExecute);
            LoadWithNestedScopes = new RelayCommandAsync(LoadWithNestedScopesExecute);
            LoadWithCustomBusyBox = new RelayCommandAsync(LoadWithCustomBusyBoxExecute);
        }


        private async Task LoadWithMessageExecute()
        {
            using (var busyBox = new MessageBusyBox(this, "Loading started."))
            {
                Items = string.Empty;

                for (int i = 0; i < 5; i++)
                {
                    Items += i.ToString() + Environment.NewLine;
                    busyBox.UpdateMessage($"Loaded: {i + 1} (of 5)");

                    await Task.Delay(250);
                }
            }
        }

        private async Task LoadWithCancellationExecute()
        {
            using (var busyBox = new CancellationBusyBox(this, "Loading started."))
            {
                Items = string.Empty;
                await LongOperation(busyBox.BusyCts.Token);
            }
        }

        private async Task LongOperation(CancellationToken token)
        {
            for (int i = 0; i < 5; i++)
            {
                Items += i.ToString() + Environment.NewLine;
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await Task.Delay(500);
            }
        }

        private async Task LoadWithNestedScopesExecute()
        {
            using (var externalBusyBox = new MessageBusyBox(this, "1st part."))
            {
                Items = string.Empty;

                for (int i = 0; i < 5; i++)
                {
                    Items += i.ToString() + Environment.NewLine;
                    externalBusyBox.UpdateMessage($"1st part: {i + 1} (of 5)");

                    await Task.Delay(250);
                }

                using (var innerBusyBox = new MessageBusyBox(this, "2nd part."))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Items += i.ToString() + Environment.NewLine;
                        innerBusyBox.UpdateMessage($"2nd part: {i + 1} (of 5)");

                        await Task.Delay(250);
                    }

                    using (var oneMoreBusyBox = new CancellationBusyBox(this, "3rd part."))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Items += i.ToString() + Environment.NewLine;
                            oneMoreBusyBox.UpdateMessage($"3rd part: {i + 1} (of 5)");

                            if (oneMoreBusyBox.BusyCts.IsCancellationRequested)
                            {
                                if (MessageBox.Show(
                                        "Are you sure to abort the operation?",
                                        "Warning",
                                        MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                                {
                                    break;
                                }

                                oneMoreBusyBox.BusyCts = new CancellationTokenSource();
                            }

                            await Task.Delay(250);
                        }
                    }
                }
            }
        }

        private async Task LoadWithCustomBusyBoxExecute()
        {
            using (var busyBox = new CancellationBusyBox(this, (ControlTemplate)Application.Current.Resources["CustomCancellationBusyBoxTemplate"], "Loading started.", "Press to Cancel"))
            {
                Items = string.Empty;
                for (int i = 0; ; i++)
                {
                    Items += i.ToString() + Environment.NewLine;
                    busyBox.UpdateMessage($"Loaded: {i + 1} (of 5)");

                    if (busyBox.BusyCts.IsCancellationRequested)
                    {
                        if (MessageBox.Show(
                                "Are you sure to abort the operation?",
                                "Warning",
                                MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                        {
                            break;
                        }

                        busyBox.BusyCts = new CancellationTokenSource();
                    }

                    await Task.Delay(500);
                }
            }
        }
    }
}
