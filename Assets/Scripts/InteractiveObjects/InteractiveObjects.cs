using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
namespace InteractiveObjects
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        private bool _isInteractable;

        protected bool IsInteractable
        {
            get => _isInteractable;
            private set
            {
                _isInteractable = value;
                GetComponent<Renderer>().enabled = _isInteractable;
                GetComponent<Collider>().enabled = _isInteractable;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable || !other.CompareTag("Player"))
            {
                return;
            }
            Interaction(other.gameObject);
            IsInteractable = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!IsInteractable || !other.gameObject.CompareTag("Player"))
            {
                return;
            }
            Interaction(other.gameObject);
            IsInteractable = false;
        }

        protected abstract void Interaction(GameObject otherGameObject);

        public abstract void Execute();

        private void Start()
        {
            IsInteractable = true;

            this.OnCollisionEnterAsObservable()
                .Where(other => IsInteractable && other.gameObject.CompareTag("Player"))
                .Subscribe(collision => Interaction(collision.gameObject))
                .AddTo(this);
            this.OnTriggerEnterAsObservable()
                .Where(other => IsInteractable && other.CompareTag("Player"))
                .Subscribe(other => Interaction(other.gameObject))
                .AddTo(this);
            Observable.EveryUpdate().Subscribe(l => Execute()).AddTo(this);
        }

        private void Update()
        {
            Execute();
        }
    }

}
