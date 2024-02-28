using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 10f;

    private AudioSource gunfireSound;
    // Start is called before the first frame update
    private void Start()
    {
        gunfireSound = GetComponent<AudioSource>();
    }
    public void FireBullet()
    {
        gunfireSound.Play();
        GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
        Destroy(spawnedBullet, 3f);

    }
}
