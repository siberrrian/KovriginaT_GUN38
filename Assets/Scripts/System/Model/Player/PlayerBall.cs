using System.Collections;
using System.Collections.Generic;
using System.Model;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace System.Model
{
    public sealed class PlayerBall : PlayerBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _speed;
        [SerializeField] private int _health = 100;
        [SerializeField] private TextMeshProUGUI _textHealth;

        [Inject] private IAxisInput _input;
        private IDisposable _disposable;

        //private void OnEnable() => _disposable = _input.IAxisInput.Subscribe(Move);

        //private void OnDisable() => _disposable.Dispose();

        protected override void Move(Vector3 direction) => _rigidbody.AddForce(direction * Speed);

        void Start()
        {
            _textHealth.text = _health.ToString();
        }
        void Update()
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");
            _rigidbody.AddForce(new Vector3(moveH, 0, moveV) * _speed);
           // _textHealth.text = _health.ToString();
        }

        public void ChangeHealth(int bonus = 15)
        {
            _health += bonus;
            _textHealth.text = _health.ToString();
        }
    }
}

