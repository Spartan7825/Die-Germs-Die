using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    public Joystick js;
    public CharacterController controller;
    public AudioSource spraySound;
    bool isPlaying = false;
    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    public float fallSpeed = 6f;

    public LayerMask enemyLayer;
     float attackRange = 2f;
    public bool enemyInAttackRange;
   public Animator anim;

    public ParticleSystem smoke;
    public Transform smokePos;
    ParticleSystem ps;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = js.Horizontal;
        float vertical = js.Vertical;
        //GroundCheck();
        //ground check
        if (transform.position.y != 0.5)
        {

            Vector3 desiredPositon = new Vector3(0, -0.5f, 0);
            controller.Move(desiredPositon * fallSpeed * Time.deltaTime);
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        anim.SetFloat("Speed", Mathf.Abs(direction.magnitude));
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        }

        enemyInAttackRange = Physics.CheckSphere(this.transform.position, attackRange, enemyLayer);
        if (enemyInAttackRange)
        {
            if (!isPlaying) 
            {
                spraySound.Play();
                spraySound.loop = true;
                isPlaying = true;
            }
            
            anim.SetLayerWeight(1, 1);
            ps = Instantiate(smoke, smokePos.position, transform.rotation);
        }
        else 
        { 
            anim.SetLayerWeight(1, 0);
            spraySound.Stop();
            isPlaying = false;
        }

    }

    public void GroundCheck()
    {
        if (transform.position.y != 0.5)
        {
            
            Vector3 desiredPositon = new Vector3(transform.position.x,0.5f,transform.position.z);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
