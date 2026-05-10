using System.Collections;
using System.Collections.Generic;
using System.Model;
using UnityEngine;


namespace InteractiveObjects
{
    public sealed class Trap : InteractiveObject//, IFlay
    {
        private Material _material;
        private float _lengthFlay;
        private float y;


        private void Awake()
        {
            y = transform.localPosition.y;
            //_material = GetComponent<Renderer>().material;
            //_material.color = Color.red;
            _lengthFlay = 4.5f;
        }

        protected override void Interaction(GameObject otherGameObject)
        {
            if (otherGameObject.TryGetComponent<System.Model.PlayerBall>(out PlayerBall player))
            {
                player.ChangeHealth(-40);
            }
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
        }
    }
}