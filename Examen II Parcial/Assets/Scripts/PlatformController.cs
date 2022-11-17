using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public Rigidbody platformRB; //Es la plataforma que se moverá
    public Rigidbody damage;
    public Transform[] platformPositions; //Posición de los puntos en los que se moverá la plataforma
    public float platformSpeed; //Velocidad de la plataforma

    private int actualPosition = 0; //La primera posición de la plataforma
    private int nextPosition = 1; //La siguiente posición en la que estará la plataforma

    public bool moveToTheNext = true; //Controla si la plataforma debe moverse al siguiente punto.
    public float waitTime; //El tiempo de espera para moverse a la siguiente plataforma

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    //Hará que la plataforma se mueva
    void MovePlatform()
    {
        if (moveToTheNext == true)
        {
            StopCoroutine(WaitForMove(0));
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime));
            damage.MovePosition(Vector3.MoveTowards(platformRB.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime));
        }

        if (Vector3.Distance(platformRB.position, platformPositions[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitForMove(waitTime));
            actualPosition = nextPosition;
            nextPosition++;

            if (nextPosition > platformPositions.Length - 1)
            {
                nextPosition = 0;
            }
        }
    }

    //Para la plataforma cuando llegue a su punto
    IEnumerator WaitForMove(float time)
    {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }
}
