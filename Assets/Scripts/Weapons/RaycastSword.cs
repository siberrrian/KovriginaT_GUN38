using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSword : MonoBehaviour
{
    /*
    class Bullet {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
        public int bounce;
    }*/
    public string weaponName;
    public ActiveWeapon.WeaponSlot weaponSlot;
    public MeshSockets.SocketId holsterSocket;
    public LayerMask layerMask;
    public bool isFiring = false;
    public bool debug = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public int maxBounces = 0;
    public int ammoCount = 30;
    public int clipSize = 30;
    public int clipCount = 2;
    public float damage = 10;

    public RuntimeAnimatorController animator;
   // public ParticleSystem[] muzzleFlash;
   // public ParticleSystem hitEffect;
   // public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    //public WeaponRecoil recoil;
   // public GameObject magazine;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    //List<Bullet> bullets = new List<Bullet>();
    float maxLifetime = 3.0f;

    public Transform raycastTip; // конец меча
    public float attackRange = 3.5f; // Дистанция удара
    public float attackRadius = 0.6f; // Ширина удара 

    public void StartFiring()
    {
        // Ограничиваем частоту ударов (fireRate)
        float fireInterval = 1.0f / fireRate;
        if (accumulatedTime >= fireInterval)
        {
            PerformSimpleAttack();
            accumulatedTime = 0.0f;
        }
    }

    private void PerformSimpleAttack()
    {
        Vector3 direction = raycastOrigin.forward;

        if (Physics.SphereCast(raycastOrigin.position, attackRadius, direction, out hitInfo, attackRange, layerMask))
        {
            var hitBox = hitInfo.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRaycastHit(this, direction);
                //Debug.Log("ПОПАЛ ПО: " + hitInfo.collider.name);
            }
            else
            {
               // Debug.Log("Задел объект без хитбокса: " + hitInfo.collider.name);
            }

            var rb = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb) rb.AddForceAtPosition(direction * 10, hitInfo.point, ForceMode.Impulse);
        }
        else
        {
            //Debug.Log("ПРОМАХ");
        }

        //if (recoil) recoil.GenerateRecoil(weaponName);
    }
    public void StopFiring() { }
    public void UpdateWeapon(float deltaTime, Vector3 target)
    {
        accumulatedTime += deltaTime;
    }
}
