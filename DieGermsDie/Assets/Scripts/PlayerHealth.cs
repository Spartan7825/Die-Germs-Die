using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    int currentHealth;
    [SerializeField] public Image img;
    public void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage()
    {
        currentHealth -= 10;
        if (currentHealth < 0)
        {
            FindObjectOfType<RandomSpawner>().setScore();
            Destroy(gameObject);
        }
        else
        {
            //img.gameObject.SetActive(true);
            img.fillAmount = (float)currentHealth / (float)maxHealth;
            //StartCoroutine(delay());

        }

    }


    IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);

        img.gameObject.SetActive(false);
    }

}