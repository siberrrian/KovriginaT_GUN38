using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveDOTweenAnim : MonoBehaviour
{


    [Range(2f, 18.0f), SerializeField] private float _moveDuration = 1.0f;

    [SerializeField] private Ease _moveEase = Ease.Linear;

    [SerializeField] private Vector3[] _points;

    [SerializeField] private AnimationCurve _heightCurve;

    [SerializeField] private float _height = 5f;

    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            // Вычисляем "прогресс" по массиву от 0 до 1
            // (например, для 4 точек это будет: 0, 0.33, 0.66, 1.0)
            float t = (float)i / (_points.Length - 1);

            // Берем значение из кривой и умножаем на множитель
            float addedHeight = _heightCurve.Evaluate(t) * _height;

            // Прибавляем эту высоту к текущей точке 
            _points[i].y += addedHeight;
        }

        DOTween.Sequence()
            .Append(transform.DOPath(_points, _moveDuration, PathType.CatmullRom).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetLookAt(0.01f));
        // добавление анимации по массиву точек
            
    }
}