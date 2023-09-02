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
    private float groundBufferTimer = 0f;
    public float groundBufferDuration = 0.3f; // Tempo de buffer em segundos

    private float _vSpeed = 0f;

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    void Update()
    {
        transform.Rotate(0,Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime,0);

        var inputAxiVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxiVertical * speed;

        //Jump
          // Atualiza o temporizador do buffer de chão
        if (characterController.isGrounded)
        {
            groundBufferTimer = groundBufferDuration;
        }
        else
        {
            groundBufferTimer -= Time.deltaTime;
        }

        // Permite pular mesmo se o jogador saiu do chão recentemente
        if (groundBufferTimer > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _vSpeed = jumpSpeed;
        }

        animator.SetBool("Jump", characterController.isGrounded!= true);

        var isWalking = inputAxiVertical !=0;
        if(isWalking)
        {
            if(Input.GetKey(keyRun))
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

        animator.SetBool("Run", inputAxiVertical != 0);

        

    }
}
