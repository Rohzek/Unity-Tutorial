using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    float rayLength = 3f, distance = 3f;
    RaycastHit hit;
    string colliderName = null;
    RigidbodyFirstPersonController controller;
    Transform camera;
    AmmoCount ammo;
    GameObject interact;

    void Start()
    {
        controller = GetComponent<RigidbodyFirstPersonController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        ammo = GameObject.Find("GameMaster").GetComponent<AmmoCount>();

        interact = GameObject.Find("InteractMessage");
        interact.SetActive(false);
    }

    void FixedUpdate ()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayLength, Color.red, 0.5f);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rayLength))
        {
            if (colliderName != null)
            {
                interact.SetActive(true);

                if (hit.collider.name.Equals("Enemy") && colliderName.Equals("Enemy"))
                {
                    // Show GUI prompt, for now we'll just debug:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Interacted with " + hit.collider.name);
                        MoveCharacter();
                    }
                }

                else if (hit.collider.name.Equals("AmmoUp") && colliderName.Equals("Interactable"))
                {
                    // Show GUI prompt, for now we'll just debug:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ammo.Ammo += 10;
                        Debug.Log(ammo.Ammo);
                    }
                }

                else if (hit.collider.name.Equals("AmmoDown") && colliderName.Equals("Interactable"))
                {
                    // Show GUI prompt, for now we'll just debug:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (ammo.Ammo - 10 < 0)
                        {
                            ammo.Ammo = 0;
                        }
                        else
                        {
                            ammo.Ammo -= 10;
                        }

                        Debug.Log(ammo.Ammo);
                    }
                }
            }
            else
            {
                interact.SetActive(false);
            }
        }
    }

    void MoveCharacter()
    {
        var rotation = new Quaternion(0f, 0f, 0f, 1f);
        controller.mouseLook.reset = true;
        transform.position = new Vector3(0f, 0f, 0f); // Sets location
        transform.rotation = rotation;
        camera.rotation = rotation;
    }

    void OnTriggerEnter(Collider collider)
    {
        colliderName = collider.tag;
    }

    void OnTriggerExit(Collider collider)
    {
        colliderName = null;
    }
}
