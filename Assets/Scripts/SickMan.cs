using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SickMan : MonoBehaviour
{
    public float can = 100f;
    bool canTakeDamage = true;
    public AudioSource death;
    bool isDead = false;
    public void TakeDamage(float damage)
    {
        if (!canTakeDamage || isDead) return;

        StartCoroutine(DamageRoutine(damage));
    }
  IEnumerator DamageRoutine(float damage)
    {
        canTakeDamage = false;
        can -= damage;
        
        Debug.Log("Hasta adam hasar yedi, kalan can:" + can);
        if(can <= 0)
        {
            Debug.Log("Hasta adam öldü.");
            death.Play();
            yield return new WaitForSeconds(5f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("DeathMenu");
        }
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }
}
