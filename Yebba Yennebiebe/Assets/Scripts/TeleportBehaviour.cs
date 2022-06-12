using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehaviour : MonoBehaviour
{
    public GameObject player;
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - particles.transform.position).magnitude <= 5.0f)
        {
            //particles.Play();
            particles.Clear();
        }
        else if(!particles.isPlaying)
        {
            particles.Play();
        }
    }
}
