using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    void Start()
    {
        this.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.score >= 35)
        {
            GameObject trapDoor = gameObject.transform.Find("TrapDoor").gameObject;
            trapDoor.layer = LayerMask.NameToLayer("Blue");
        }
    }
}
