using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public float health = 100f;
    private CharacterController controller;
    public float HighSpeed = 6f;
    public float MidSpeed = 4f;
    public float LowSpeed = 2f;
    private float currentSpeed;
    public float gravity = -9.87f * 2;
    public float jumpHeight = 1f;
    bool canTakeDamage = true;
    public AudioSource walkingSound;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = HighSpeed;

    }


    void Update()
    {
       
       
        if(health > 60f)
        {
            currentSpeed = HighSpeed;
        }
        if(health <= 60f)
        {
            currentSpeed = MidSpeed;
        }
        if(health <= 30f)
        {
            currentSpeed = LowSpeed;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        controller.Move(move * currentSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isGrounded == true && health > 30f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonDown("Jump") && health <= 30f)
        {
            Debug.Log("Çok Yaralýsýn zýplayamazsýn");
        }
            velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        bool isRealyMoving = (Mathf.Abs(x) > 0.2f || Mathf.Abs(z) > 0.2f);

        if (isRealyMoving && isGrounded)
        {
            isMoving = true;
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            isMoving = false;
            if (walkingSound.isPlaying)
            {
                walkingSound.Pause();
            }
        }
       
        

    }

    public void TakeDamage(float damageAmount)
    {
        if (canTakeDamage)
        {
            StartCoroutine(DamageRoutine(damageAmount));
        }
    }

    IEnumerator DamageRoutine(float damageAmount)
    {
        
        health -= damageAmount;
        canTakeDamage = false;
        Debug.Log("Hasar Yedik !!! KALAN CAN; " +health);
        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("DeathMenu");
            yield break;
        }
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;

        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("MedKit") && health < 100)
        {
            health += 30f;
            if (health >= 100f) 
            health = 100f;
            Debug.Log("Medkit buldun, Yeni can:" + health);
            Destroy(col.gameObject);
        }
    }

}
