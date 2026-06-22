using UniRx;
using Zenject;

namespace Models.Interfaces
{
    /// <summary>
    /// Interface for Model that keeps player's score (best and current)
    /// Current can be reseted
    /// Best should be saved and loaded, so SaveLoad should be injected
    /// </summary>
    public interface IBonusUIModel : IInitializable
    {
        
        ReactiveProperty<int> LastCircles { get; }

        ReactiveProperty<int> LastTriangles { get; }

        
        void IncreaseCircles();
        void IncreaseTriangles();
        void ResetBonuses();
    }
}