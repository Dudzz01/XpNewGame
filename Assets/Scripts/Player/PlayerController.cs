using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private SpriteRenderer spritePlayer;
    [SerializeField] private Animator playerAnimController;

    [Header("Audio")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip[] soundClipPlayer = new AudioClip[3];
    

    private static PlayerController _Instance;

        public static PlayerController Instance
        {
            get
            {
                _Instance = FindObjectOfType<PlayerController>();

                return _Instance;
            }
        }
    #region MovimentPlayerVariables

    public float directionPlayerH{get; private set;} // direcao horizontal do player
    public float directionPlayerY{get; private set;} // direcao horizontal do player
    [SerializeField] private float fHorizontalDampingBasic = 0.5f;
    [SerializeField] private float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField] private float fHorizontalDampingWhenTurning = 0.5f;
    #endregion
    #region WallJump/Jump/Collisions/Actions Variables
    public bool IsGround {get; private set;} // verifica se o player está colidindo com o chao ou nao
    public float IsGroundTimerJumpCoyote{get; private set;}
    public bool IsWallRight {get; private set;} // verifica se o player está colidindo com a parede ou nao
    public bool IsWallLeft{get; private set;}
    public bool IsSliding {get; private set;}
    public bool IsWallJump {get; private set;}
    public bool IsWallJumping {get; private set;}
    public bool CanMove{get; private set;}
    private bool doubleJump;
    [SerializeField]private LayerMask groundMask;
    [SerializeField]private LayerMask wallJumpMask; 
    [SerializeField]private Transform transformFeet;
    [SerializeField]private Transform transformArm;
    [SerializeField]private float powerJump;
    private Vector2 rightOffSetArm;
    private Vector2 leftOffSetArm;
    private bool jump;

    public static bool playerIsAlive;

    private float saveEnergyValor;

    public float durAnimGameOver {get; set;}
    #endregion

    void Start()
    {
        FadePlayer();
        playerIsAlive = true;
        saveEnergyValor = 1;
        energyBar = GetComponent<EnergyBar>();
        playerAnimController = GetComponent<Animator>();
        
    }

    private void Awake()
    {
        rightOffSetArm = new Vector2(0.2f,0);// Offset da posicao do colisor do braco direito do player
        leftOffSetArm = new Vector2(-0.13f,0);// Offset da posicao do colisor do braco esquerdo do player

        //CanMove = true; modificado pela animação.
    }

    private void Update()
    {
        directionPlayerH = Input.GetAxisRaw("Horizontal"); // variavel para saber a direcao h do player
        directionPlayerY = Input.GetAxisRaw("Vertical"); // variavel para saber a direcao y do player

        IsGround = Physics2D.OverlapBox((Vector2)transformFeet.position ,new Vector2(0.25f,0.20f),0,groundMask) || Physics2D.OverlapBox(transformFeet.position,new Vector2(0.25f,0.24f),0,wallJumpMask); // verifica se o pé do player está colidindo com o chao
        playerAnimController.SetBool("isGrounded", IsGround);

        IsWallRight = Physics2D.OverlapCircle((Vector2)transformArm.position+rightOffSetArm,0.19f,wallJumpMask); // retornará true se o colisor do braco direito do player estiver colidindo na parede
        IsWallLeft = Physics2D.OverlapCircle((Vector2)transformArm.position+leftOffSetArm,0.19f,wallJumpMask); // retornará true se o colisor do braco esquerdo do player estiver colidindo na parede

        #region ActionPlayer
            if(playerIsAlive == true)
            {
                JumpInput();
                SlidingWall();
                WallJump();
                
            }
        #endregion
            
        SoundPlayer();
        Debug.Log(playerIsAlive);
        //Animacao de morte do player
        if(playerIsAlive == false)
        {
            playerAnimController.SetTrigger("dead");
            rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        
       durAnimGameOver =  playerAnimController.GetCurrentAnimatorStateInfo(0).length;
    }

    private void FixedUpdate()
    {
        if(energyBar.remainingEnergy <= 0f)
        {
            saveEnergyValor = 0;
        }
        
        if(saveEnergyValor <= 0)
        {
            playerIsAlive = false;
        }

        if(playerIsAlive == true)
        {

            Walk();
            JumpMovimentEffects();
        }
        else
        {
            playerIsAlive = false;
            CanMove = false;
        }
    }

    public void Walk()
    {
        #region AnimMovimentWalk
        
        if(directionPlayerH == 0 && IsGround)
        {
           // playerAnimController.SetInteger("Condicao Nothing to AnimPlayer",5); // numero default para iniciar qualquer animacao na tree animation
            playerAnimController.SetBool("isWalking", false);
            
            if(soundSource.clip == soundClipPlayer[0])
            {
                soundSource.loop = false;
            }
        }

        if(directionPlayerH != 0  && IsGround)
        {
            playerAnimController.SetBool("isWalking", true);
        }

        if(directionPlayerH > 0)
        {
            spritePlayer.flipX = false;
        }
        else if(directionPlayerH < 0)
        {
            spritePlayer.flipX = true;
        }
        #endregion

        if(CanMove)
        {
            //movimento de walk do player otimizado, para funcionar de forma mais fluida
            float horizontalSpeedPlayerH = rig.velocity.x;
            horizontalSpeedPlayerH += directionPlayerH;

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            {
                horizontalSpeedPlayerH *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
            }
            else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalSpeedPlayerH))
            {
                horizontalSpeedPlayerH *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
            }
            else
            {
                horizontalSpeedPlayerH *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);
            }
            
            //Debug.Log("Horzintal speed "+ horizontalSpeedPlayerH);
            rig.velocity = new Vector2(Mathf.Clamp(horizontalSpeedPlayerH, -8.5f, 8.5f), rig.velocity.y);
        }
    }

    private void JumpAction()
    {      
        //first jump
        rig.velocity = new Vector2(rig.velocity.x, powerJump);
        jump = false;
        doubleJump = !doubleJump;
    }

    private bool JumpInput()
    {
        #region AnimJump
        if(!IsGround && !IsSliding)
        {
            playerAnimController.SetBool("isJumping", true);
            soundSource.clip = soundClipPlayer[1];
            soundSource.loop = false;
            soundSource.volume = 0.05f;
            //soundSource.Play();
        }
        else
        {
            playerAnimController.SetBool("isJumping", false);
        }
        
        #endregion

        if( Input.GetKeyDown(KeyCode.W) && IsGroundTimerJumpCoyote > 0) // Se apertar W e colidir com o chao... 
        {
            jump = true;
            IsGroundTimerJumpCoyote = 0;
            doubleJump = false;
        }
        
        if(jump || doubleJump == true && Input.GetKeyDown(KeyCode.W)) //Se cumprir essa condicao, a acao do pulo é executada
        {
            JumpAction();
        }

        if(Input.GetKeyDown(KeyCode.W) && IsSliding) // Se estiver deslizando na parede e usa o W para pular, o Wall Jump funcionará
        {
            IsWallJump = true;
            CanMove = false;
            IsWallJumping = true;
            Invoke("StopWallJump", 0.2f);  //Em 0.2 segundos, o metodo StopWallJump é invocado
        }

        return jump;
    }

    public void JumpMovimentEffects()
    {
        float fallMultiplier = 5.5f; //variavel que faz o player cair mais rapido
        float lowJumpMultiplier = 4.5f; //variavel que faz o player subir mais "lento"
        const float valueTimerJumpCoyote = 0.15f; // efeito coyote
        
        if(rig.velocity.y < 0)
        {
            rig.velocity+= Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // se o player estiver caindo, será somado a velocidade dele um valor através dessa formula aritmetica para cair mais rapido
        }
        else if(rig.velocity.y>0 && !Input.GetKey(KeyCode.W))
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // se o player estiver subindo, será somado a velocidade dele um valor através dessa formula aritmetica para subir mais "devagar"
        } 

        if(IsGround)
        {
            //IsWallJumping = false;
            IsGroundTimerJumpCoyote = valueTimerJumpCoyote; // se o player estiver colidindo com o chao, a variavel recebe 0.2f
        }
        else
        {
            IsGroundTimerJumpCoyote-=Time.deltaTime; // se o player nao estiver colidindo com o chao, a variavel ficará diminuindo até 0, logo o player consegue pular mesmo no ar com a condicao dessa variavel ser maior que 0 (Efeito Coyote)
        }
    }

    private void SlidingWall()
    {
        if((IsWallRight && directionPlayerH == 1 || IsWallLeft && directionPlayerH == -1) && !IsGround) // se estiver colidindo com a parede e pressionando o botao de movimento em direcao a parede e se não estiver colidindo com o chao, ele fara o sliding
        {
            playerAnimController.SetBool("isSliding", true);
            IsSliding = true;
            rig.velocity = new Vector2(rig.velocity.x, -2); // Deslizando
        }
        else
        {
            IsSliding = false;
            playerAnimController.SetBool("isSliding", false);
        }
    }

    private void WallJump() // Método que faz o player pular quando estiver deslizando na parede
    {   
        if(IsWallJump == true)
        {   
            if(IsWallRight)
            {
               rig.velocity = new Vector2(-9, 12);
               spritePlayer.flipX = true; // Váriavel que basicamente inverte a sprite para condizer com o movimento do player
            }
            if(IsWallLeft)
            {
                rig.velocity = new Vector2(9,12);
                spritePlayer.flipX = false;    
            }
        }
    }

    private void StopWallJump()
    {
        IsWallJump = false;
        CanMove = true;
    }
    
    void SoundPlayer()
    {
        if(Input.GetKeyDown(KeyCode.W) && IsGround == true) // pulo
        {
           soundSource.PlayOneShot(soundClipPlayer[1]);
           
        }

        if(Input.GetKeyDown(KeyCode.A ) || Input.GetKeyDown(KeyCode.D) )
        {
            if(IsGroundTimerJumpCoyote > 0 && IsGround)
            {
                soundSource.clip = soundClipPlayer[0];
                soundSource.loop = true;
                soundSource.Play();
            }
        }

        if(directionPlayerH == 0 && !Input.GetKeyDown(KeyCode.A ) && !Input.GetKeyDown(KeyCode.D) )
        {
            soundSource.loop = false;
        }
    }
        
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.tag == "Diamond")
        {
            SpawnDiamonds.collectedDiamonds++;
            SpawnDiamonds.canSpawnDiamond = true;
            col.gameObject.SetActive(false);
            
        }

        if(col.gameObject.tag == "Door")
        {
            if(SpawnDiamonds.canPassNextLevel == true && playerIsAlive == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SpawnDiamonds.collectedDiamonds = 0;
                SpawnDiamonds.canSpawnDiamond = false;
                SpawnDiamonds.canPassNextLevel = false;
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere((Vector2)transformArm.position+rightOffSetArm,0.19f);
        Gizmos.DrawWireSphere((Vector2)transformArm.position+leftOffSetArm,0.19f);
        Gizmos.DrawWireCube((Vector2)transformFeet.position,new Vector2(0.25f,0.20f));
    }

    void FadePlayer()
    {
        Sequence introSeq = DOTween.Sequence();
        float dur = 1.0f;

        introSeq.Append(spritePlayer.DOFade(1f, dur).OnComplete(()=> CanMove = true).SetDelay(.5f));
        introSeq.Join(transform.DOMoveX(transform.position.x + 0.5f, dur));
    }
}