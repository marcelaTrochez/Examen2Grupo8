using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Vector3 offset; //Distancia entre la cámara y el jugador
    private Transform target; //Target representa al player, y la cámara siempre lo seguirá.
    [Range (0,1)] public float lerpValue; //Es la rápidez en la que pasa una posición a otra
    public float sensibilidad; //Es la sensiblidad en la que se mueve la cámara

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Es lo último que se va a ejecutar [La cámara se posicionará de último]
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up) * offset;

        transform.LookAt(target); //Posicionamiento automático de la cámara con el player
    }
}
