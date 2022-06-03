using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0, 0, 500);
        //rb.AddForce(new Vector3(0, 1000, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftHand" || collision.gameObject.tag == "RightHand")
            return;
        Debug.Log("Destroyed bullet");
        Destroy(gameObject);
    }
}
