using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public int maxJumps = 2; // Número máximo de pulos permitidos
    private int jumpsRemaining = 0; // Número de pulos restantes

    private float _vSpeed = 0f;

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    void Start()
    {
        jumpsRemaining = maxJumps; // Defina o número inicial de pulos restantes como o máximo
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxiVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxiVertical * speed;

        //Jump
        if (characterController.isGrounded)
        {
            _vSpeed = -0.5f; // Evita que o personagem "cole" ao chão
            jumpsRemaining = maxJumps; // Reinicia o número de pulos quando estiver no chão
        }
        
        // Permite pular mesmo se o jogador saiu do chão recentemente e ainda tem pulos restantes
        if (jumpsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _vSpeed = jumpSpeed;
            jumpsRemaining--;
        }

        animator.SetBool("Jump", !characterController.isGrounded);

        var isWalking = inputAxiVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", isWalking);
    }
}
