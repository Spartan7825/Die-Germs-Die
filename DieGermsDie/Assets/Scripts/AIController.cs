using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIController : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    [SerializeField]bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform projectileSpawnPos;

    //States
    public float sightRange, attackRange,closeRange,damageRange;
    public bool playerInSightRange, playerInAttackRange,playerInCloseRange,playerInDamageRange;

    public NavMeshSurface surface;

    public AICircleFormationManager circleFormationManager;

    [SerializeField] Animator anim;



    public void Start()
    {
        circleFormationManager = FindObjectOfType<AICircleFormationManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInCloseRange = Physics.CheckSphere(transform.position, closeRange, whatIsPlayer);
        playerInDamageRange = Physics.CheckSphere(transform.position, damageRange, whatIsPlayer);

        if (playerInDamageRange) { TakeDamage(1); } else { anim.SetLayerWeight(1, 0); }

        if (!playerInSightRange && !playerInAttackRange&&!playerInCloseRange) Patroling();
        if (playerInSightRange && !playerInAttackRange && !playerInCloseRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !playerInCloseRange) AttackPlayer();//circleFormationManager.FormationPos(this);
        if (playerInSightRange && playerInCloseRange) RunAway();
    }

    private void Patroling()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsRunning", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(randomX, transform.position.y, randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsRunning", true);
        agent.SetDestination(player.position);
    }

    public void RunAway()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsRunning", true);
        Vector3 dirToPlayer = transform.position - player.position;
        Vector3 newPos = transform.position + dirToPlayer;

        if(agent!=null)
        {
            agent.SetDestination(newPos);
        }

    }

    public void AttackPlayer()
    {
      
            anim.SetBool("IsRunning", false);
            if (!alreadyAttacked)
            {
                anim.SetBool("IsAttacking", true);
            }
        





        agent.SetDestination(transform.position);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
      
    }

    public void triggerAttack()
    {
        
        if (!alreadyAttacked)
        {
               anim.SetBool("IsAttacking", false);
           
               Instantiate(projectile,projectileSpawnPos.position, Quaternion.identity).GetComponent<ProjectileMovement>().targetPos = player.position;
     

               alreadyAttacked = true;
               Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    

    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("IsAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        anim.SetLayerWeight(1, 1);
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0f);
            //DestroyEnemy();
        } 
    }
    private void DestroyEnemy()
    {
        FindObjectOfType<RandomSpawner>().EnemiesKilled();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }
}