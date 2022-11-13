using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] List<InBetween> currentlyInBetween;
    [SerializeField] List<InBetween> alreadyTransparent;
    [SerializeField] Transform player;
    private Transform camera;

    private void Awake()
    {
        currentlyInBetween = new List<InBetween>();
        alreadyTransparent = new List<InBetween>();
        camera = this.gameObject.transform;
    }
    private void Update()
    {
        GetAllObjectsInBetween();

        MakeObjectsSolid();
        MakeObjectsTransparent();
    }

    void GetAllObjectsInBetween()
    {
        currentlyInBetween.Clear();
        float cameraPlayerDistance = Vector3.Magnitude(camera.position - player.position);
        Ray ray1_Forward = new Ray(camera.position, player.position - camera.position);
        Ray ray1_Backward = new Ray(player.position, camera.position - player.position);

        var hits1_Forward = Physics.RaycastAll(ray1_Forward, cameraPlayerDistance);
        var hits1_Backward = Physics.RaycastAll(ray1_Backward, cameraPlayerDistance);
        foreach (var hit in hits1_Forward)
        {
            if (hit.collider.gameObject.TryGetComponent(out InBetween inBetween))
            {
                if (!currentlyInBetween.Contains(inBetween))
                {
                    currentlyInBetween.Add(inBetween);
                }
            }
        }

        foreach (var hit in hits1_Backward)
        {
            if (hit.collider.gameObject.TryGetComponent(out InBetween inBetween))
            {
                if (!currentlyInBetween.Contains(inBetween))
                {
                    currentlyInBetween.Add(inBetween);
                }
            }
        }
    }

    void MakeObjectsTransparent()
    {
        for (int i = 0; i < currentlyInBetween.Count; i++)
        {
            InBetween inBetween = currentlyInBetween[i];
            if (!alreadyTransparent.Contains(inBetween))
            {
                inBetween.ShowTransparent();
                alreadyTransparent.Add(inBetween);
            }
        }
    }
    void MakeObjectsSolid()
    {
        for (int i = 0; i < alreadyTransparent.Count; i++)
        {
            InBetween wasInTheWay = alreadyTransparent[i];
            if (!currentlyInBetween.Contains(wasInTheWay))
            {
                wasInTheWay.ShowSolid();
                alreadyTransparent.Remove(wasInTheWay);
            }
        }
    }

}
