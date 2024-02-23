using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoFloor : MonoBehaviour
{
    public int index;

    // Interval in which to change color
    private float period = 4.0f;
    // Tracks how much time has elapsed
    private float currentTime = 0.0f;
    private string[] colors = {"Red", "White", "Blue", "White", "Green", "White", "Yellow", "White"};
    void Start()
    {
        switch(LayerMask.LayerToName(gameObject.layer))
        {
            case "Red":
                this.index = 0;
                break;
            case "Blue":
                this.index = 2;
                break;
            case "Green":
                this.index = 4;
                break;
            case "Yellow":
                this.index = 6;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > period)
        {
            //Debug.Log("Color change in progress");
            this.index = (index + 1) % 8;
            int newLayer = LayerMask.NameToLayer(this.colors[index]);
            gameObject.layer = newLayer;
            currentTime = 0;
        }
        if (this.index % 2 == 1) {
            period = 2.0f;
        }
        else
        {
            period = 4.0f;
        }
        currentTime += Time.deltaTime;
    }
}
