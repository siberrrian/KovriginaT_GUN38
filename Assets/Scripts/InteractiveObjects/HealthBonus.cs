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


        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _material.color = Color.green;
            _lengthFlay = 5.0f;
        }

        protected override void Interaction(GameObject otherGameObject)
        {
            if (otherGameObject.TryGetComponent<System.Model.PlayerBall>(out PlayerBall player))
            {
                player.ChangeHealth(15);
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
                Mathf.PingPong(Time.time, _lengthFlay), transform.localPosition.z);
        }
    }
}