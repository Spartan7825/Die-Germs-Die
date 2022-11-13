using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;

    public void Start()
    {

    }
    //public bool isStarted = false;

    /*    public void startGame() 
        {
            FindObjectOfType<RandomSpawner>().Gamestart();
            isStarted = true;
        }*/
    private void LateUpdate()
    {
/*        if (isStarted)
        {
        }*/
        Vector3 desiredPositon = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPositon, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }


}
