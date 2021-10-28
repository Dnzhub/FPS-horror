using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    EnemyHealth health;
    Collider capsuleCollider;

    public AudioSource[] audios;
    [Header("Chase State")]
    Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float rotationSpeed = 5f;
    

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<EnemyHealth>();
        target = GameObject.FindWithTag("Player").transform;
        capsuleCollider = GetComponent<Collider>();
       
        
    }
   

    private void Update()
    {
        UpdateAnimator();
        IncreaseDetectionOnFire();
        chaseState();
        if (isAttacking)
            audios[2].Stop();
        if (health.IsDead())
        {
            audios[0].Stop();
            audios[1].Stop();
            audios[2].Stop();

        }
    }

   private void IncreaseDetectionOnFire()
    {

        if (target.GetComponentInChildren<Weapon>().isFiring)
            chaseRange = 120f;
        else
        {
            chaseRange = 60f;
        }
    }
    
    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("Speed", speed);
    }
    private void chaseState()
    {
        if(health.IsDead())
        {
            capsuleCollider.enabled = false;
            agent.enabled = false;
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked)
        {
            EngageTarget();
            if(!audios[2].isPlaying)
                audios[2].Play();
          
            if (!audios[0].isPlaying && !audios[1].isPlaying)
            {
                audios[Random.Range(0, 2)].Play();
                // audios[Random.Range(0, 2)].Play();           
            }

        }
        
        else if(distanceToTarget <= chaseRange  && IsPlayerVisible())
        {
            isProvoked = true;                   
        }
    }
    public bool IsPlayerVisible()
    {
        
        RaycastHit closestTarget = new RaycastHit();
        Vector3 enemyBody = GetComponent<Collider>().bounds.size;
        Vector3 headPosition = transform.position + new Vector3(0, enemyBody.y, 0);

        Vector3 direction = target.position - headPosition;

        float closesDist = Mathf.Infinity;
        RaycastHit[] hits = Physics.RaycastAll(headPosition, direction, chaseRange);
        

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == this.gameObject || hit.collider.CompareTag("Terrain"))  continue;

            if(hit.distance < closesDist)
            {
                closesDist = hit.distance;

                closestTarget = hit;
            }
        }
        if(closestTarget.collider != null)
        {
            //if target collider has Player tag return true
            return closestTarget.transform.CompareTag("Player");
        }
        return false;
    }
    public void OnDamageTaken()
    {
        isProvoked = true;
    }
    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= agent.stoppingDistance)
        {
            agent.speed = 6;
            ChaseTarget();
           
        }
        if (distanceToTarget <= agent.stoppingDistance + 1)
        {           
            AttackTarget();
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
     
    }
    private void AttackTarget()
    {
        //--Enemy will hit with Animation event on EnemyAnimationEvents--//
        animator.SetBool("Attack", true);
      
       


    }
    private void ChaseTarget()
    {
        
        animator.SetBool("Attack", false);
     
        agent.SetDestination(target.position);
      
       
    
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
