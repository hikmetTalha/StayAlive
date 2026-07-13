using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletPrefabTime = 3f;
    public int bulletVelocity = 3;

    private void Awake()
    {
        Destroy(this.gameObject, bulletPrefabTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletVelocity * Time.deltaTime);
    }
}
