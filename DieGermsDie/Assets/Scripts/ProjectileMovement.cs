using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    float moveTime;
    Vector3 startPos;
    public Vector3 targetPos;
    [SerializeField] ParticleSystem explosion;

    private void Start()
    {
        startPos = transform.position;

    }


    void Update()
    {
        if (moveTime < 0.25)
        {
            moveTime += Time.deltaTime;
            //  moveTime = moveTime % 2;
            transform.position = MathParabola.Parabola(startPos, targetPos, 1f, moveTime / 0.25f);

        }
        else 
        {
            Instantiate(explosion, transform.position, transform.rotation);    
            Destroy(gameObject); }
    }
}
