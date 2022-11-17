using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour //Esta en el player
    //PickUpObject prácticamente es una acción que nos permite agarrar un objeto
{

    public GameObject ObjectToPickUp; //Nos permite agarrar un objeto en el Script PickableObjet
    public GameObject PickedObject; //Es el objeto que hayamos agarrado
    public Transform interactionZone; //Son las manos del player [la ubicación en el player donde tendrá ese objeto]


    // Update is called once per frame
    void Update()
    {
        //Si el player NO tiene un objeto en sus manos y Si pueda agarrar el objeto,  podrá agarrarlo pulsando la tecla F.
        if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObject>().isPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickedObject = ObjectToPickUp;
                PickedObject.GetComponent<PickableObject>().isPickable = false;
                PickedObject.transform.SetParent(interactionZone);
                PickedObject.transform.position = interactionZone.position;
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        //Si el player tiene un objeto en sus manos deberá soltarlo pulsando la tecla F.
        else if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickedObject.GetComponent<PickableObject>().isPickable = true;
                PickedObject.transform.SetParent(null);
                PickedObject.GetComponent<Rigidbody>().useGravity = true;
                PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                PickedObject = null;
            }
        }
    }
}
