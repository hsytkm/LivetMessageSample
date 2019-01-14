using Livet;
using Livet.Messaging;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace LivetMessageSample.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public ReactiveProperty<Unit> DropEvent { get; } =
            new ReactiveProperty<Unit>(mode: ReactivePropertyMode.None);

        public ReactiveProperty<Unit> MouseLeftDown { get; } =
            new ReactiveProperty<Unit>(mode: ReactivePropertyMode.None);

        public ReactiveProperty<string> Text { get; } =
            new ReactiveProperty<string>(mode: ReactivePropertyMode.DistinctUntilChanged);

        private int Counter;

        public MainWindowViewModel()
        {
            DropEvent
                .Subscribe(_ => Counter++)
                .AddTo(CompositeDisposable);

            MouseLeftDown
                .Subscribe(_ => Counter++)
                .AddTo(CompositeDisposable);

            DropEvent
                .Subscribe(_ =>
                {
                    Messenger.Raise(new InformationMessage(
                        $"Drop({Counter}回目):表示されるようになりました",
                        "Title", MessageBoxImage.Information, "Info"));
                    Text.Value = $"Drop({Counter}回目):表示されます";
                })
                .AddTo(CompositeDisposable);

            MouseLeftDown
                .Subscribe(_ =>
                {
                    Messenger.Raise(new InformationMessage(
                        $"Mouse({Counter}回目):表示されます",
                        "Title", MessageBoxImage.Information, "Info"));
                    Text.Value = $"Mouse({Counter}回目):表示されます";
                })
                .AddTo(CompositeDisposable);
        }
    }
}
