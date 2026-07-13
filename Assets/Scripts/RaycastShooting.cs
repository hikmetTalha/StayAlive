using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RaycastShooting : MonoBehaviour
{
    public Camera cam;
    public AudioSource reload;
    public MouseMovement mouseMovement;
    public float recoilAmound = 4f;
    private bool isReload = false;
    public int bulletCount = 12;
    public AudioSource ShootSound;
    public int maxAmmo = 12;
    public GameObject muzzleFlash;

    private void Start()
    {
        bulletCount = maxAmmo;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isReload  == false && bulletCount > 0f)
        {
            Shoot();
            ShootSound.Play();
             
        }
        
        if(bulletCount <= 0f && isReload == false)
        {

           StartCoroutine (Reload());
            reload.Play();
        }
    }

    void Shoot()
    {
        if(muzzleFlash != null)
        {
            StartCoroutine(MuzzleFlash());
        }
        if (isReload == true)
        {
            return;
        }
        if (mouseMovement != null)
        {
            mouseMovement.recoilVer(recoilAmound);
        }
        { 

        }
        RaycastHit hit;
       if( Physics.Raycast(cam.transform.position, cam.transform.forward, out hit,100f))
        {
            Debug.Log("Vurulan obje;" + hit.transform.name);
            HitBox vurulanHitbox = hit.transform.GetComponent<HitBox>();

           
            if (vurulanHitbox != null)
            {

                vurulanHitbox.anaKontrolcu.Vuruldu(vurulanHitbox.isHead);

            }
            else
            {
                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("Enemy scripti bulundu, Vuruldu tetikleniyor!");
                    enemy.Vuruldu(false);
                }
            }

        }
        bulletCount -= 1;
    }

    IEnumerator Reload()
    {
        isReload = true;
        yield return new WaitForSeconds(3f);
        isReload = false;
        bulletCount = maxAmmo;

    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }
}
