using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMakeDamages : MonoBehaviour
{

    public int cantidad = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_and_Damage>().RestarVida(cantidad);
            if (other.GetComponent<PickUpObject>().PickedObject != null)
            {
                other.GetComponent<PickUpObject>().ObjectToPickUp = null;
                other.GetComponent<PickUpObject>().PickedObject.GetComponent<PickableObject>().isPickable = true;
                other.GetComponent<PickUpObject>().PickedObject.transform.SetParent(null);
                other.GetComponent<PickUpObject>().PickedObject.GetComponent<Rigidbody>().useGravity = true;
                other.GetComponent<PickUpObject>().PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<PickUpObject>().PickedObject = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_and_Damage>().RestarVida(cantidad);
        }
    }

}
