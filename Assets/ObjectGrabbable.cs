using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;
    

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity = false;
    }

   

    public void Drop() 
    {
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
    }

    public void TelepathicallyMove(float lerpSpeed)
    {
        if (objectGrabPointTransform != null)
        {
            // Calculate the direction from the current position to the grab point
            Vector3 directionToGrabPoint = objectGrabPointTransform.position - transform.position;

            // Calculate the new position using Lerp to smoothly move towards the grab point
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // Apply the new position to the object's Rigidbody
            objectRigidBody.MovePosition(newPosition);
        }
    }



    public void Throw(Vector3 throwForce)
    {
     objectRigidBody.useGravity = true;
     objectRigidBody.velocity = throwForce;
    }


    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 5f; // Adjust this value as needed
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPosition);
        }
    }

}
