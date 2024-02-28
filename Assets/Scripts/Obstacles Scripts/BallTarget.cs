using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTarget : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource balloonPopSound;
    void Start()
    {
        balloonPopSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            balloonPopSound.Play();
            Destroy(gameObject,0.2f);
        }
    }
}
