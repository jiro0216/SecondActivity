using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private TextMeshProUGUI hoverText; // Reference to the TextMeshProUGUI component for displaying hover text

    private bool isThrowing = false;
    private float throwForce = 10f; // Adjust this value as needed for the desired throwing force
    private ObjectGrabbable objectGrabbable;
    private float telepathicLerpSpeed = 1f;

    private void Update()
    {
        // Check if the player is hovering over a grabbable GameObject and update hover text
        PickUp();
        UpdateHoverText();
        Throw();
        Telepathy();
    }


    private void UpdateHoverText()
    {
        // Check if the player is hovering over a grabbable GameObject
        RaycastHit hit;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, Mathf.Infinity, pickUpLayerMask))
        {
            // Update hover text to display relevant information
            hoverText.text = "Press E to grab";
            hoverText.gameObject.SetActive(true);
        }
        else
        {
            // Hide hover text if not hovering over a grabbable GameObject
            hoverText.gameObject.SetActive(false);
        }
    }

    private void PickUp() {


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickupDistance = 30f;

                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                // Currently carrying something 
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }


    }

    private void Throw()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isThrowing = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            isThrowing = false;
        }

        if (isThrowing && objectGrabbable != null)
        {
            // Calculate throw direction based on camera forward direction
            Vector3 throwDirection = playerCameraTransform.forward;

            // Apply force to throw the object
            objectGrabbable.Throw(throwDirection * throwForce);

            // Release the object
            objectGrabbable.Drop();
            objectGrabbable = null;
        }
    }

    private void Telepathy()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (objectGrabbable != null)
            {
                objectGrabbable.TelepathicallyMove(telepathicLerpSpeed);
            }
        }
    }

}


