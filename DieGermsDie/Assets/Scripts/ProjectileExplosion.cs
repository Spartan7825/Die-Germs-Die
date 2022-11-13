using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{

    float blastRange = 1f; 
     bool playerInBlastRange;
    [SerializeField]LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        playerInBlastRange = Physics.CheckSphere(transform.position, blastRange, player);
        if (playerInBlastRange)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage();
            //FindObjectOfType<PlayerHealth>().TakeDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRange);

    }

}
