using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public GameManager gameManager;




    void Start()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.collectibleCounter++;
            gameObject.SetActive(false);

        }
    }
}
