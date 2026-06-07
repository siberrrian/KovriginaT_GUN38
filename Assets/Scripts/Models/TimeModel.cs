using System;
using Core.Utils;
using Cysharp.Threading.Tasks;
using Models.Interfaces;
using UniRx;
using System.Threading;

namespace Models
{
    public sealed class TimeModel:  ITimeModel
    {
        private readonly ReactiveProperty<int> _gameTime = new();
        public IObservable<int> GameTime =>  _gameTime;

        private CancellationTokenSource _cts = new();

        public void Initialize() => CountTime(_cts.Token).Forget();

        

        private async UniTask CountTime(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Delay(NumericConstants.One * 1000, cancellationToken: cancellationToken);
                    _gameTime.Value++;
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _gameTime.Dispose();
        }
    }
}