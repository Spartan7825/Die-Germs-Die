using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEnemyPointer : MonoBehaviour
{
    Vector3 targetPosition;
   [SerializeField] RectTransform pointerRectTransform;
    [SerializeField] Transform target;
    [SerializeField]GameObject player;
    [SerializeField] Camera uiCamera;
    float y;

    void Awake()
    {
        
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = target.position;
        Vector3 toPosition = player.transform.position ;
        Vector3 fromPosition = transform.root.position;
        toPosition.y = 0f ;
        fromPosition.y = 0f;

        Vector3 dir = (fromPosition-toPosition).normalized; 
        Quaternion rotation = Quaternion.LookRotation(dir);

       pointerRectTransform.localEulerAngles = new Vector3(0, 0, -rotation.eulerAngles.y);
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        float borderSize = 100f;
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        if (isOffScreen)
        {
            pointerRectTransform.gameObject.SetActive(true);
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (player.transform.position.x <= transform.root.position.x) cappedTargetScreenPosition.x = Screen.width-borderSize ;
            if (player.transform.position.x >= transform.root.position.x) cappedTargetScreenPosition.x = borderSize;
            if (player.transform.position.z <= transform.root.position.z) cappedTargetScreenPosition.y = Screen.height-borderSize ;
            if (player.transform.position.z >= transform.root.position.z) cappedTargetScreenPosition.y = borderSize;

            
            pointerRectTransform.position = cappedTargetScreenPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        }
        else
        {
            pointerRectTransform.gameObject.SetActive(false);
        }




    }
}
