using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ResetGunPosition : MonoBehaviour
{
    // Define the reset position and rotation for the gun
    public Vector3 resetPosition;
    public Quaternion resetRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    void Update()
    {
        // Initialize a list to hold the devices with the specified characteristics
        var devices = new List<InputDevice>();
        
        // Get all devices with the Right and Controller characteristics
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            bool primaryButtonPressed = false;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
            {
                PerformGunReset();
            }
        }
    }

    private void PerformGunReset()
    {
        // Set the gun's position and rotation to the predefined reset values
        transform.position = resetPosition;
        transform.rotation = resetRotation;

        // If the gun has a Rigidbody, also reset its velocity and angular velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
