using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGoBoing : MonoBehaviour
{
    public int boingHeight;
    public GameObject boing;
    public GameObject particle;
    bool timerActive = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        // Makes the trigger appear in the right spot
        //transform.localScale = new Vector3(transform.localScale.x, 0.1f /transform.parent.localScale.y, transform.localScale.z);
        //transform.localPosition = new Vector3(0, (0.1f / transform.parent.localScale.y) + transform.parent.localScale.y/2, 0);
    }

    private void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                boing.GetComponent<Animator>().SetBool("Bounced", false);
                timerActive = false;
            }
        }
        else
        {
            timer = 1;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        boing.GetComponent<Animator>().SetBool("Bounced", true);
        timerActive = true;
        particle.GetComponent<ParticleSystem>().Play();
    }
}
