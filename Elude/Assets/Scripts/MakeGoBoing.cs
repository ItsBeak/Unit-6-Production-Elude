using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGoBoing : MonoBehaviour
{
    public int boingHeight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(0, boingHeight, 0, ForceMode.Impulse);
    }
}
