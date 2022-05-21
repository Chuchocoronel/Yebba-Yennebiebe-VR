using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        rb.AddForce(player.transform.forward.normalized * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
