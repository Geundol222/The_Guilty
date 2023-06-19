using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHolder : MonoBehaviour
{
    [SerializeField] EnemyGun gun;

    public void Fire()
    {
        gun.Fire();
    }
}
