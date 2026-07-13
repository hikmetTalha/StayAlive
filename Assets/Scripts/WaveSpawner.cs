using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int zombieCount;
    public float bornDelay;

}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject zombiePrefab;
    public float beklemeSuresi = 10f;

    private int nextWaveIndex = 0;
    private bool zombieWaiting = false;
    private float zombikontrolSayaci = 1f;

    private void Start()
    {
        StartCoroutine(dalgaBaslat(waves[nextWaveIndex]));
    }
    private void Update()
    {

        if (zombieWaiting)
        {
            zombikontrolSayaci -= Time.deltaTime;
            if(zombikontrolSayaci <= 0f)
            {
                zombikontrolSayaci = 1f;
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    DalgaBitti();
                }
            }
        }

    }

    IEnumerator dalgaBaslat (Wave wave)
    {
        Debug.Log(wave.waveName + "Baslýyor, gelecek zombi sayýsý;" + wave.zombieCount);

        for(int i = 0; i < wave.zombieCount; i++)
        {
            zombiKlonla();
            yield return new WaitForSeconds(wave.bornDelay);
        }
        zombieWaiting = true;
    }

    void zombiKlonla()
    {
        Transform rastgeleNokta = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, rastgeleNokta.position, rastgeleNokta.rotation);
    }

    void DalgaBitti()
    {
        zombieWaiting = false;
        nextWaveIndex++;

        if(nextWaveIndex < waves.Length)
        {
            StartCoroutine(YeniDalgayaGecis());
        }
        else
        {
            StartCoroutine(WinScreen());
            
            
        }

    }

    IEnumerator YeniDalgayaGecis()
    {
        Debug.Log("Bölge temizlendi, Yeni Dalga" + beklemeSuresi + "saniye sonra baþlayacak.");
        yield return new WaitForSeconds(beklemeSuresi);
        StartCoroutine(dalgaBaslat(waves[nextWaveIndex]));
    }

    IEnumerator WinScreen()
    {
        Debug.Log("Tüm dalgalar bitti!!!");
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("FinalMenu");
    }
}
