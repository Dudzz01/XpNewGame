using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region ComponentsVariablesOfPlayer
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private SpriteRenderer spritePlayer;
    #endregion
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
    [SerializeField]private LayerMask objectsGroundMask; 
    [SerializeField]private Transform transformFeet;
    [SerializeField]private Transform transformArm;
    [SerializeField]private float powerJump;
    private Vector2 rightOffSetArm;
    private Vector2 leftOffSetArm;
    private bool jump;
    #endregion

    private void Awake()
    {
        rightOffSetArm = new Vector2(0.13f,0);// Offset da posicao do colisor do braco direito do player
        leftOffSetArm = new Vector2(-0.03f,0);// Offset da posicao do colisor do braco esquerdo do player

        CanMove = true;
    }

    private void Update()
    {
        directionPlayerH = Input.GetAxisRaw("Horizontal"); // variavel para saber a direcao h do player
        directionPlayerY = Input.GetAxisRaw("Vertical"); // variavel para saber a direcao y do player

        IsGround = Physics2D.OverlapBox((Vector2)transformFeet.position ,new Vector2(0.25f,0.20f),0,groundMask) || Physics2D.OverlapBox(transformFeet.position,new Vector2(0.25f,0.24f),0,objectsGroundMask); // verifica se o pé do player está colidindo com o chao
        IsWallRight = Physics2D.OverlapCircle((Vector2)transformArm.position+rightOffSetArm,0.19f,groundMask); // retornará true se o colisor do braco direito do player estiver colidindo na parede
        IsWallLeft = Physics2D.OverlapCircle((Vector2)transformArm.position+leftOffSetArm,0.19f,groundMask); // retornará true se o colisor do braco esquerdo do player estiver colidindo na parede

        #region ActionPlayerMethods
        JumpInput();
        SlidingWall();
        WallJump();
        #endregion
    }

    private void FixedUpdate()
    {
        Walk();
        JumpMovimentEffects();
    }

    public void Walk()
    {
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
            

             Debug.Log("Horzintal speed "+ horizontalSpeedPlayerH);
             rig.velocity = new Vector2(Mathf.Clamp(horizontalSpeedPlayerH,-8.5f,8.5f),rig.velocity.y);
        }
    }

    private void JumpAction()
    {      //first jump
           rig.velocity = new Vector2(rig.velocity.x,powerJump);
           jump = false;
           doubleJump = !doubleJump;
           
           
    }

    private bool JumpInput()
    {

        if( Input.GetKeyDown(KeyCode.W) && IsGroundTimerJumpCoyote>0) // Se apertar W e colidir com o chao... 
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
            Invoke("StopWallJump",0.2f);  //Em 0.2 segundos, o metodo StopWallJump é invocado
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

             IsSliding = true;
             rig.velocity = new Vector2(rig.velocity.x, -2); // Deslizando
        }
        else
        {
            IsSliding = false;
        }
       
    }

    private void WallJump() // Método que faz o player pular quando estiver deslizando na parede
    {   
        
        
        if(IsWallJump == true)
        {
            
            if(IsWallRight)
            {
               rig.velocity = new Vector2(-9,12);
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

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere((Vector2)transformArm.position+rightOffSetArm,0.19f);
        Gizmos.DrawWireSphere((Vector2)transformArm.position+leftOffSetArm,0.19f);
        Gizmos.DrawWireCube((Vector2)transformFeet.position,new Vector2(0.25f,0.20f));
    }
}
