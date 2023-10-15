using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NAO ESTA EM USO---------------------------------------------------------
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Captura as entradas de teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula o vetor de movimento
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // Transforma o vetor na direção local do jogador
        moveDirection = transform.TransformDirection(moveDirection);

        // Aplica a velocidade de movimento
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
