using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidBody : MonoBehaviour //Esta en el player
{

    public float pushPower = 2.0f; //La fuerza en la que el jugador podrá mover el objeto
    private float targetMass; //Representa la masa del objeto que queremos mover

    //Actua cuando el player coliciona con un objeto (Nos permite mover el objeto)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }
        targetMass = body.mass;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower / targetMass;
    }

}
