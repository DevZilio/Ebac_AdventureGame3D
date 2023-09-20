using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DevZilio.Core.Singleton;
using Cloth;
using UnityEngine;

public class Player : Singleton<Player>
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
    private float groundBufferTimer = 0f;
    public float groundBufferDuration = 0.3f; // Tempo de buffer em segundos

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Cloths")]
    public ClothChanger clothChanger;

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
            groundBufferTimer = groundBufferDuration; // Reinicia o temporizador de buffer de chão
        }
        
        // Atualiza o temporizador de buffer de chão
        if (groundBufferTimer > 0)
        {
            groundBufferTimer -= Time.deltaTime;
        }

        // Permite pular mesmo se o jogador saiu do chão recentemente e ainda tem pulos restantes
        if ((jumpsRemaining > 0 || groundBufferTimer > 0) && Input.GetKeyDown(KeyCode.Space))
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


}
