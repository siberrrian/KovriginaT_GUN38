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
        [SerializeField] private Camera _camera;
        [SerializeField] private TextMeshProUGUI _textHealth;

        [Inject] private IAxisInput _input;
        private IDisposable _disposable;
        /*
        private void OnEnable() => _disposable = _input.IAxisInput.Subscribe(Move);

        private void OnDisable() => _disposable.Dispose();*/

        protected override void Move(Vector3 direction) => _rigidbody.AddForce(direction * Speed);

        void Start()
        {
            _textHealth.text = _health.ToString();
        }
        void Update()
        {/*
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");
            _rigidbody.AddForce(new Vector3(moveH, 0, moveV) * Speed);*/


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            _rigidbody.velocity = new Vector3(move.x * Speed, _rigidbody.velocity.y, move.z * Speed);
        }

        public void ChangeHealth(int bonus = 15)
        {
            _health += bonus;
            UpdateHealthUI();
        }

        public void SetHealth(int value)
        {
            _health = value;
            UpdateHealthUI();
        }

        private void UpdateHealthUI()
        {
            if (_textHealth != null)
                _textHealth.text = _health.ToString();
        }
        public void ChangeSpeed(int bonus = 2)
        {
            Speed += bonus;
        }
    }
}

