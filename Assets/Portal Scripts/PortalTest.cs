using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest : MonoBehaviour
{
    // Public variable for the destination portal
    public GameObject otherPortal;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("testCube"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Teleport the cube and reflect its velocity
                float speed = rb.velocity.magnitude;
                Vector3 newDir = otherPortal.transform.forward * speed;
                other.transform.position = otherPortal.transform.position + (otherPortal.transform.forward * 5);
                rb.velocity = newDir;
            }
        }

        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Teleport the player and reflect its velocity
                float speed = controller.velocity.magnitude;
                Vector3 newDir = otherPortal.transform.forward * speed;
                other.transform.position = otherPortal.transform.position + (otherPortal.transform.forward * 5);
                other.transform.rotation = Quaternion.LookRotation(newDir, Vector3.up);

                // Reflect the player's velocity
                Vector3 reflectedVelocity = Vector3.Reflect(controller.velocity, otherPortal.transform.forward);
                controller.Move(reflectedVelocity * Time.deltaTime);
            }
        }
    }
}
