using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public bool spawn = false;
    public GameObject deadParticle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(7, 8);
        //rb.velocity = new Vector3(0, 0, 500);
        //rb.AddForce(new Vector3(0, 1000, 0));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<GroundEnemyBehaviour>().Die(gameObject.tag);

            GameObject go = Instantiate(deadParticle);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            
            Debug.Log("Destroyed bullet");
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground")
        {
            GameObject go = Instantiate(deadParticle);
            go.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            
            Debug.Log("Destroyed bullet");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spawn) return;

        if (collision.gameObject.tag == "LeftHand" || collision.gameObject.tag == "RightHand")
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<GroundEnemyBehaviour>().Die(gameObject.tag);
        }

        Debug.Log("Destroyed bullet");
        Destroy(gameObject);
    }
}
