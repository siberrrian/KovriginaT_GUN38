using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour {

    public RaycastSword weaponFab;


    private void OnTriggerEnter(Collider other) {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon) {
            RaycastSword newWeapon = Instantiate(weaponFab);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }

        AiWeapons aiWeapons = other.gameObject.GetComponent<AiWeapons>();
        if (aiWeapons) {
            RaycastSword newWeapon = Instantiate(weaponFab);
            aiWeapons.Equip(newWeapon);
            Destroy(gameObject);
        }
    }
}
