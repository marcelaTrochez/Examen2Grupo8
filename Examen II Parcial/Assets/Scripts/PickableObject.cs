using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool isPickable = true;

    //Nos permite detectar si un objeto es picable o no
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = this.gameObject;
        }

        if (other.tag == "PlayerInteractionZonePet")
        {
            other.GetComponentInParent<ReviveDoggo>().ObjectToPickUp = this.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")
        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = null;
        }
        if (other.tag == "PlayerInteractionZonePet")
        {
            other.GetComponentInParent<ReviveDoggo>().ObjectToPickUp = null;
        }
    }

}
