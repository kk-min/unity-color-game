using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(LayerMask.LayerToName(gameObject.layer))
        {
            case ("Red"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.red);
                }
                break;
            case ("Blue"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.blue);
                }
                break;
            case ("Green"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.green);
                }
                break;
            case ("Yellow"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.yellow);
                }
                break;
            case ("White"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.white);
                }
                break;
        }
    }
}
