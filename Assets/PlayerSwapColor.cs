using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwapColor : MonoBehaviour
{
    [SerializeField] private InputActionProperty swapButton;
    [SerializeField] private CharacterController cc;

    private void Update()
    {

        //Debug.Log(_isGrounded);
        if(swapButton.action.WasPressedThisFrame())
        {
            //Debug.Log("jumping");
            Swap();
        }
    }
    private void Swap()
    {
        int newLayer;

        if (gameObject.layer == LayerMask.NameToLayer("Blue"))
        {
            newLayer = LayerMask.NameToLayer("Red");
        }
        else
        {
            newLayer = LayerMask.NameToLayer("Blue");
        }

        gameObject.layer = newLayer;
        SetColor(transform, newLayer);
        Debug.Log("Current layer: "+gameObject.layer);
    }

    private void SetColor(Transform node, int newLayer)
    {
        foreach (Transform child in node)
        {
            child.gameObject.layer = newLayer;
            SetColor(child, newLayer);
        }
    }

}
