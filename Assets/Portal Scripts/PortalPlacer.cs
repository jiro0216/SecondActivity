using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlacer : MonoBehaviour
{
    // Public GameObjects for the portals so our player can move them
    // These can be assigned in the inspector
    public GameObject bluePortal;
    public GameObject orangePortal;

    RaycastHit portalGunHit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out portalGunHit))
            {
                MovePortal(portalGunHit, bluePortal);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out portalGunHit))
            {
                MovePortal(portalGunHit, orangePortal);
            }
        }
    }

    void MovePortal(RaycastHit hit, GameObject portal)
    {
        portal.transform.position = hit.point;
        portal.transform.rotation = Quaternion.LookRotation(hit.normal);
        Debug.Log("Portal rotation: " + portal.transform.rotation.eulerAngles);
        Debug.Log("Hit normal: " + hit.normal);
        Debug.Log("Quaternion Euler angles of hit normal: " + Quaternion.Euler(hit.normal));
    }
}
