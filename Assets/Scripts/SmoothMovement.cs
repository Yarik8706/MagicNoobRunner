using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class SmoothMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _speed;
        

        private void Start()
        {
            var time = Vector3.SqrMagnitude(_points[0].position - _points[1].position) / _speed;
            Move(0, time);
        }

        private void Move(int nextPointIndex, float time)
        {
            transform.DOMove(_points[nextPointIndex].position, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (nextPointIndex == _points.Length - 1) return;
                var newTime = Vector3.SqrMagnitude(_points[nextPointIndex].position - _points[nextPointIndex + 1].position) / _speed;
                Move(nextPointIndex + 1, newTime);
            });
        }
    }
}