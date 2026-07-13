using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform bulletSpawn;
    public float bulletPrefabTime = 3f;
    public int bulletVelocity = 3;

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();

        }
  
    }

    void FireWeapon()
    {
        if ( bulletSpawn != null && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        }
        
        
    }


}
