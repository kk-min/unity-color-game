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
        switch (LayerMask.LayerToName(gameObject.layer))
        {
            case ("Red"):
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_BaseColor",  new Color(204/255f, 62/255f, 62/255f));
                }
                break;
            case ("Blue"):
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_BaseColor", new Color(0f, 90/255f, 255/255f));
                }
                break;
            case ("Green"):
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_BaseColor", new Color(129/255f, 190/255f, 131/255f));
                }
                break;
            case ("Yellow"):
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_BaseColor", new Color(246/255f, 209/255f, 85/255f));
                }
                break;
            case ("White"):
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.SetColor("_BaseColor", Color.white);
                }
                break;
        }
    }
}
