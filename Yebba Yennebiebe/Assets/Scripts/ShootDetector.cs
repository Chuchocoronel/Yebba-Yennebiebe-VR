using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDetector : MonoBehaviour
{
    public PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LeftHand")
        {
            playerScript.leftBackTouched = true;
            Debug.Log("TOUCHEED");
        }
        if (collision.gameObject.tag == "RightHand")
        {
            playerScript.rightBackTouched = true;
            Debug.Log("TOUCHEED");
        }
    }
}
