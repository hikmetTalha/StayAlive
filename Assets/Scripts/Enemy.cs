using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    float attackRange = 1.5f;
    float attackDamage = 20f;
    bool isDead = false;
    bool isStunned = false;
    NavMeshAgent navMesh;
    public GameObject medkitPrefab;
    Transform player;
    Transform sickMan;
    Transform target;
    private Vector3 startPosition;
    public AudioSource bite;
    public AudioSource walk;
    public AudioSource growl;
    void Start()
    {
        
        navMesh = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
        GameObject sickObj = GameObject.FindWithTag("SickMan");
        if (sickObj != null) sickMan = sickObj.transform;

    }

    
    void Update()
    {
        

        if (isDead || isStunned) return;
        HedefiGuncelle();
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < attackRange)
        {
            animator.SetBool("run", false);
            animator.SetBool("attack", true);
            navMesh.isStopped = true;
            if (walk.isPlaying)
            {
                walk.Stop();
            }
            if (!bite.isPlaying)
            {
                bite.Play();
            }
            if (target.CompareTag("Player"))
            {
                CharacterMovement playerScript = target.GetComponent<CharacterMovement>();
                if (playerScript != null)
                {
                    playerScript.TakeDamage(attackDamage);

                }
            }
            else if (target.CompareTag("SickMan"))
            {
                SickMan sickManScript = target.GetComponent<SickMan>();
                if (sickManScript != null)
                {
                    sickManScript.TakeDamage(attackDamage);
                }
            }
        }
            else
            {
            if (!walk.isPlaying)
            {
                walk.Play();
            }
                navMesh.isStopped = false;
                navMesh.SetDestination(target.position);
                animator.SetBool("run", true);
                animator.SetBool("attack", false);
            
            }

      
        
    
       
    }

    void HedefiGuncelle()
    {
        if (player == null && sickMan == null)
        {
            target = null;
            return;
        }
        if(sickMan == null)
        {
            target = player;
            return;
        }
        if(player == null)
        {
            target = sickMan;
            return;
        }
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        float distToSickMan = Vector3.Distance(transform.position, sickMan.position);
        if(distToSickMan < distToPlayer)
        {
            target = sickMan;
        }
        if(distToSickMan > distToPlayer)
        {
            target = player;
        }
    }
   

    public void Vuruldu(bool HeadShoot)
    {
        if (isDead) return;
        if(HeadShoot)
        {
            if (walk.isPlaying) walk.Stop();
            
            if (bite.isPlaying) bite.Stop();
            animator.SetBool("dead", true);
            isDead = true;
            Debug.Log("zombi vuruldu siliniyor...");
            navMesh.isStopped = true;
            Destroy(gameObject, 3f);
           int medKitSpawn = (int)Random.Range(1f, 20f);
            if (medKitSpawn == 1)
            {
                Instantiate(medkitPrefab, transform.position, transform.rotation);
            }
         
        }
        else
        {
            StartCoroutine(SersemlemeRoutine());
            animator.SetBool("run",false);
            animator.SetBool("attack", false);

        }
    }

     IEnumerator SersemlemeRoutine()
    {
        if (isStunned) yield break;
        isStunned = true;
        if (walk.isPlaying) walk.Stop();
        growl.Play();
        navMesh.isStopped = true;
        Debug.Log("Zombi sersemledi!!");

        yield return new WaitForSeconds(3f);

        isStunned = false;
        navMesh.isStopped = false;
        Debug.Log("Zombi toparladý!!");
    }

}
