using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cloth;
using DevZilio.Core.Singleton;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("Animation")]
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public int maxJumps = 2; // Número máximo de pulos permitidos
    public int jumpsRemaining = 0; // Número de pulos restantes

    private float _vSpeed = 0f;
    private float groundBufferTimer = 0f;
    public float groundBufferDuration = 0.3f; // Tempo de buffer em segundos

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Cloths")]
    public ClothChanger clothChanger;

    [Header("Audio")]
    public SFXType sfxType;


    // Novos campos para controle de mira
    public float lookSensitivity = 2.0f;
    private float rotationX = 0;

    public Transform cameraTransform;


    void Start()
    {
        jumpsRemaining = maxJumps;
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
        characterController.Move(moveDirection * speed * Time.deltaTime);

        // Calcula a rotação do jogador horizontalmente com base na movimentação do mouse
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * lookSensitivity);

        // Calcula a rotação da câmera verticalmente com base na movimentação do mouse
        float mouseY = Input.GetAxis("Mouse Y");
        rotationX -= mouseY * lookSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);



        //Jump
        if (characterController.isGrounded)
        {
            _vSpeed = -0.5f;
            jumpsRemaining = maxJumps;
            groundBufferTimer = groundBufferDuration;
        }

        if (groundBufferTimer > 0)
        {
            groundBufferTimer -= Time.deltaTime;
        }

        if ((jumpsRemaining > 0 || groundBufferTimer > 0) && Input.GetKeyDown(KeyCode.Space))
        {
            _vSpeed = jumpSpeed;
            Play();
            jumpsRemaining--;
            // animator.SetBool("Jump", !characterController.isGrounded);
        }



        //run
        var isWalking = moveDirection.magnitude > 0; // Verifica se o jogador está andando
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                moveDirection *= speedRun; // Aplica a velocidade de corrida
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        _vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = _vSpeed;

        characterController.Move(moveDirection * Time.deltaTime);

        animator.SetBool("Run", isWalking);
    }




    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localspeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localspeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTexctureCoroutine(setup, duration));
    }

    IEnumerator ChangeTexctureCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangeTexture(setup, duration);
        yield return new WaitForSeconds(duration);
        clothChanger.ResetTexture();
    }


    public void LoadLastSave()
    {
        if (SaveManager.Instance.Setup != null)
        {
            // Carregue as informações relevantes do SaveManager
            int lastLevel = SaveManager.Instance.Setup.lastLevel;
            int lastCheckpointKey = SaveManager.Instance.Setup.lastChekPoint;
            float playerHealth = SaveManager.Instance.Setup.health;
            float playerCoins = SaveManager.Instance.Setup.coins;

            // Ajuste as informações do jogador de acordo com o carregamento
            // Por exemplo, você pode atualizar a posição, saúde e moedas aqui
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
            Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value = (int)playerCoins;
            Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value = (int)playerHealth;

            // Se você precisar fazer algo mais com as informações carregadas, faça aqui
        }
        else
        {
            Debug.LogWarning("SaveManager Setup is null. Cannot load last save.");
        }
    }

    private void Play()
    {
        SFXPool.Instance.Play(sfxType);
    }


}
