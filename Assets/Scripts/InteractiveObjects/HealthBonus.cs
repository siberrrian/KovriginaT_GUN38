using System.Collections;
using System.Collections.Generic;
using System.Model;
using UnityEngine;


namespace InteractiveObjects
{
    public sealed class GoodBonus : InteractiveObject//, IFlay
    {
        private Material _material;
        private float _lengthFlay;
        private float _speed = 50;
        private float y;


        private void Awake()
        {
            _lengthFlay = 1.0f;
            y = transform.localPosition.y;
        }

        protected override void Interaction(GameObject otherGameObject)
        {
            if (otherGameObject.TryGetComponent<System.Model.PlayerBall>(out PlayerBall player))
            {
                player.ChangeHealth(15);
            }
            GetComponent<Light>().enabled = false;
        }

        public override void Execute()
        {
            if (!IsInteractable) { return; }
            Flay();
        }

        public void Flay()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.PingPong(Time.time, _lengthFlay) + y, transform.localPosition.z);
            transform.Rotate(0, _speed * Time.deltaTime, 0);
        }
    }
}