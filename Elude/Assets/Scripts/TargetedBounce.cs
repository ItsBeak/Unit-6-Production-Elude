using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedBounce : MonoBehaviour
{
    // The second child of the mushrooms
    Transform child;
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        child = transform.parent.GetChild(1);
        // Makes the trigger appear in the right spot
        transform.localScale = new Vector3(transform.localScale.x, 0.1f / transform.parent.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(0, (0.1f / transform.parent.localScale.y) + transform.parent.localScale.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Set the players parabola to that of the mushroom and go
            player = other.GetComponent<Player>();
            player.settingsParabola.transform.GetChild(0).position = child.GetChild(0).position;
            player.settingsParabola.transform.GetChild(1).position = child.GetChild(1).position;
            player.settingsParabola.transform.GetChild(2).position = child.GetChild(2).position;
            player.isBouncing = true;
            player.parabolaController.FollowParabola();
        }
    }
}
