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
                    renderer.material.SetColor("_Color", new Color(1f, 0.059f, 0.09f));
;
                }
                break;
            case ("Blue"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", new Color(0.184f, 0.192f, 0.78f));
                }
                break;
            case ("Green"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", new Color(0.176f, 0.878f, 0.396f));
                }
                break;
            case ("Yellow"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", new Color(0.969f, 0.969f, 0.506f));
                }
                break;
            case ("White"):
                foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.white);
                }
                break;
            case ("Black"):
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_Color", Color.black);
                }
                break;
        }
    }
}
