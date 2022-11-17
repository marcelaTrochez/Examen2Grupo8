using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    //Variables de Movimiento
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    //Variables movimiento relativo a camara
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    // Variables de Deslizamiento en pendientes
    public bool isOnSlope = false;
    private Vector3 hitNormal; //Es el angulo base [o normal] del plano.
    public float slideVelocity; //La velocidad en la que nos desplazamos hacia abajo en una rampa
    public float slopeForceDown; //Fuerza que nos hace mantener aún en la rampa mientras nos desplazamos hacia abajo.

    private Animation animation;


    // Start is called before the first frame update
    void Start(){
        player = GetComponent<CharacterController>();
        //playerAnimatorController = GetComponent<Animator>();
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update(){

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);

        if (Input.GetAxis("Vertical") != 0)
        {
            animation.Play("run");
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            animation.Play("run");
        }
        else if (Input.GetButtonDown("Jump") || player.isGrounded == false)
        {
            animation.Play("skill");
            animation.Stop("idle");
            animation.Stop("stop");
        }
        else{
            animation.Play("run");
        }

    }

    //Función para determinar la dirección de mira de la cámara.
    void camDirection()
    {

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

    }

    //Función para las habilidades del jugador.
    void PlayerSkills()
    {

        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }

    }

    //Función para la gravedad.
    void SetGravity()
    {

        if (player.isGrounded)
        {
            fallVelocity = -(gravity) * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        SlideDown();
    }

    //Comparar si el jugador esta o no esta en una rampa.
    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlope) //Aplica las fuerzas necesarias para llevar al player hacia abajo [según la gravedad]
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            movePlayer.y += slopeForceDown;
        }
    }

    //Función incluida por Unity.
    //Detecta cuando el player coliciona contra otro objeto.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

}
