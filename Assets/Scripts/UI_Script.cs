using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class UI_Script : MonoBehaviour
{
    public Slider PlayerHealthSlider;
    public Slider SickManHealthSlider;

    public Text ammoText;
    
    private CharacterMovement player;
    private SickMan sickMan;
    private RaycastShooting shootScript;

    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
        sickMan = FindObjectOfType<SickMan>();
        shootScript = FindObjectOfType<RaycastShooting>();

        if (player != null) PlayerHealthSlider.maxValue = player.health;
        if (sickMan != null) SickManHealthSlider.maxValue = sickMan.can;
    }

   
    void Update()
    {
        if(player != null)
        {
            PlayerHealthSlider.value = player.health;
        }
        if(sickMan != null)
        {
            SickManHealthSlider.value = sickMan.can;
        }
        if(shootScript != null)
        {
            ammoText.text = shootScript.bulletCount + "/" + shootScript.maxAmmo;
        }
    }
}
