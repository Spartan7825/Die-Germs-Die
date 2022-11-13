using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICircleFormationManager : MonoBehaviour
{
    public AIController[] allAIAgents;
    public Transform player;
    float radiusAroundTarget = 5f;

    public void FormationPos(AIController aiController)
    {
        allAIAgents = FindObjectsOfType<AIController>();
        for (int i = 0; i < allAIAgents.Length; i++)
        {
            
            if (allAIAgents[i] == aiController)
            {
                player = allAIAgents[i].player;

                var direction = allAIAgents[i].transform.position - player.position;

                var angle = Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI;

                angle = angle < 0 ? angle + 360 : angle;

               
              //  Debug.Log(angle);


                allAIAgents[i].agent.SetDestination(new Vector3(player.position.x + radiusAroundTarget * Mathf.Cos(2 * Mathf.PI * Mathf.Round(angle/36) / 10), player.position.y,
                  player.position.z + radiusAroundTarget * Mathf.Sin(2 * Mathf.PI * Mathf.Round(angle / 36) / 10)));

                allAIAgents[i].transform.LookAt(new Vector3(player.position.x,allAIAgents[i].transform.position.y,player.position.z));
                allAIAgents[i].AttackPlayer();

            }

          //  Debug.Log(radiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / 10));
            
        }
    }
}
