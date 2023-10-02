using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float sensitivity = 2.0f;
    private float rotationX = 0;

    void Update()
    {
        // Captura a movimentação do mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotaciona o jogador horizontalmente com base na movimentação do mouse
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Rotaciona a câmera verticalmente com base na movimentação do mouse
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Limita o ângulo de rotação vertical
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
