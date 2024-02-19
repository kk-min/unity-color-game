using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Obstacle") 
        {
            Physics.IgnoreCollision(other, this.gameObject.GetComponent<Collider>());
        }
    }
}
