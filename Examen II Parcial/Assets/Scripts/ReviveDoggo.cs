using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveDoggo : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;

    private Animation anim;
    int repeticion = 0;

    public CharacterController player;
    int conteo = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        player = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (ObjectToPickUp != null)
        {
                PickedObject = ObjectToPickUp;
                PickedObject.GetComponent<PickableObject>().isPickable = false;
                PickedObject.transform.SetParent(interactionZone);
                PickedObject.transform.position = interactionZone.position;
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (PickedObject != null)
        {
            if (player.transform.position.z <= -99.25299f && conteo == 0)
            {
                Vector3 caminar = new Vector3(0, 0, 0.02f);
                player.transform.position += caminar;
            }
            else if(player.transform.position.z >= -114.25299f)
            {
                conteo = 1;
                player.transform.rotation = Quaternion.Euler(180, 360, -180);
                Vector3 caminar = new Vector3(0, 0, -0.02f);
                player.transform.position += caminar;
                
            }
            else
            {
                conteo = 0;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (repeticion == 0)
            {
                repeticion++;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                Vector3 stand = new Vector3(-5.07f, 0.67f, -116.13f);
                player.transform.position = stand;
                anim.Play("elf pet");

                interactionZone.transform.position = new Vector3(0, -300f, 0);
            }
        }
    }
}
