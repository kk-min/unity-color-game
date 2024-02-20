using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObs : MonoBehaviour
{
	public float distance = 5f; //Distance that moves the object
	public bool horizontal = true; //If the movement is horizontal or vertical
	public float speed = 3f;
	public float offset = 0f; //If yo want to modify the position at the start 
	public bool reverse = false;

	private bool isForward = true; //If the movement is out
	private Vector3 startPos;
	private Vector3 horizontalVector;
	private Vector3 verticalVector;
    void Awake()
    {
		if (reverse)
		{
			this.horizontalVector = -Vector3.right;
			this.verticalVector = -Vector3.forward;
		}
		else
		{
			this.horizontalVector = Vector3.right;
			this.verticalVector = Vector3.forward;
		}

		startPos = transform.position;
		if (horizontal)
			transform.position += horizontalVector * offset;
		else
			transform.position += verticalVector * offset;

		
	}

    // Update is called once per frame
    void Update()
    {
		if (horizontal)
		{
			if (isForward)
			{
				if (reverse)
				{
					if (transform.position.x > startPos.x - distance)
					{
						transform.position += horizontalVector * Time.deltaTime * speed;
					}
					else
					{
						isForward = false;
					}
				}
				else
				{
					if (transform.position.x < startPos.x + distance)
					{
						transform.position += horizontalVector * Time.deltaTime * speed;
					}
					else
						isForward = false;
					}
			}
			else
			{
				if (reverse)
				{
					if (transform.position.x < startPos.x)
					{
						transform.position -= horizontalVector * Time.deltaTime * speed;
					}
					else
						isForward = true;
				}
				else
				{
					if (transform.position.x > startPos.x)
					{
						transform.position -= horizontalVector * Time.deltaTime * speed;
					}
					else
						isForward = true;
				}
			}
		}
		else
		{
			if (isForward)
			{
				if (transform.position.z < startPos.z + distance)
				{
					transform.position += verticalVector * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (transform.position.z > startPos.z)
				{
					transform.position -= verticalVector * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		}
    }

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = null;
        }
    }

	private void OnCollisionStay(Collision other)
	{
        if (other.gameObject.tag == "Player")
		{
			if (isForward)
						{
							if (reverse)
							{
								if (transform.position.x > startPos.x - distance)
								{
									other.gameObject.transform.position += horizontalVector * Time.deltaTime * speed;
								}
								else
								{
									isForward = false;
								}
							}
							else
							{
								if (transform.position.x < startPos.x + distance)
								{
									other.gameObject.transform.position += horizontalVector * Time.deltaTime * speed;
								}
								else
									isForward = false;
								}
						}
						else
						{
							if (reverse)
							{
								if (transform.position.x < startPos.x)
								{
									other.gameObject.transform.position -= horizontalVector * Time.deltaTime * speed;
								}
								else
									isForward = true;
							}
							else
							{
								if (transform.position.x > startPos.x)
								{
									other.gameObject.transform.position -= horizontalVector * Time.deltaTime * speed;
								}
								else
									isForward = true;
							}
						}
		}
	}
}
