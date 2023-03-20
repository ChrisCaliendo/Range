using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayMovement : MonoBehaviour
{
    #region Variables
    //Components
    /* Note: 
     * Eventually find a better way to attach components then public defining
     **/
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private Collider2D slidingCollider;
    [SerializeField] private Collider2D standingGroundSensor;
    [SerializeField] private Collider2D slidingGroundSensor;
    [SerializeField] private Collider2D crouchingCollider;
    private Animator animator;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private GameObject gunArm;
    private SpriteRenderer gunArmSpriteRenderer;

    [SerializeField] LayerMask isWall;

    //Movement Adjustment Variables
    [SerializeField] private float setMoveSpeed;
    [SerializeField] private float setJumpForce;

    private Rigidbody2D player;
    private float moveSpeed;
    private float jumpForce;

    //Boolean Movement Information
    private bool facingRight;
    private bool isOnWall;
    private bool isMidair;
    private bool isPressingJump;
    private bool isSliding;
    private bool isCrouching;
    private bool isLowDown;

    //Aiming Variables
    private int aimingDirection;
    public int stateOfMovement;
    /* State of Movement Definitions 
     * 0 = Standing Still
     * 1 = Running
     * 2 = Midair
     * 3 = Wall Sliding
     * 4 = Ground Sliding
     * 5 = Crouching
     */

    //Order in Layer Variables
    private const int behindCharacterLayerOrder = 1;
    private const int characterLayerOrder = 2;
    private const int frontOfCharacterLayerOrder = 3;

    //Arrow Key Movement Variables
    private float moveH;
    private float moveV;
    private float aimBack;

    #endregion Variables

    #region Updates

    private void Awake()
    {
        gunArmSpriteRenderer = gunArm.gameObject.GetComponent<SpriteRenderer>();
        player = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = setMoveSpeed;
        jumpForce = setJumpForce;
        isMidair = false;
        isPressingJump = false;
        isSliding = false;
        isCrouching = false;
        isLowDown = false;
        facingRight = false;
        isOnWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");
        aimBack = Input.GetAxisRaw("Fire2");
        Jumping();
        
        if ((facingRight && player.velocity.x < -0.1f) || (!facingRight && player.velocity.x > 0.1f)) FlipCharacter();
    }

    private void FixedUpdate()
    {
        SideToSideMovement();//Walking
        LowDownMovement();//Looking up, Sliding and Crouching
        Aiming();
    }

    #endregion Updates

    #region Movement

    #region Jumping Movement

    void Jumping()
    {
        
            if (!isMidair)
            {
                if (Input.GetButtonDown("Jump")) Jump();
            }
            else
            {
                isOnWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, isWall);
                if (isOnWall)
                {
                    stateOfMovement = 3;
                    if (Input.GetButtonDown("Jump")) Walljump();
                }
                else stateOfMovement = 2;
                animator.SetBool("isOnWall", isOnWall);
            }
        
    }

    void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, jumpForce);
    }

    void Walljump()
    {
        
        if (!facingRight) player.velocity = new Vector2(moveSpeed/2, jumpForce);
        else player.velocity = new Vector2(-moveSpeed/2, jumpForce);
    }

    #endregion Jumping Movement

    #region Side to Side Movement
    private void SideToSideMovement()
    {
        if (!isMidair && !isLowDown)
        {
            if (moveH > 0.1f || moveH < -0.1f)
            {
                Run();
                //if ((facingRight && player.velocity.x < -0.1f) || (!facingRight && player.velocity.x > 0.1f)) FlipCharacter();
                stateOfMovement = 1;
            }
            else
            {
                player.velocity = new Vector2((player.velocity.x / 2), player.velocity.y);
                stateOfMovement = 0;
            }
            animator.SetFloat("Speed", System.Math.Abs(player.velocity.x));
        }
        
        
    }

    void Run()
    {
        float targetSpeed = moveSpeed * moveH;
        float speedDif = targetSpeed - player.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? 15 : 6;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f)* Mathf.Sign(speedDif);
        player.AddForce(movement * Vector2.right);
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        //gunArm.transform.rotation = Quaternion.Euler(gunArm.transform.rotation.x, gunArm.transform.parent.rotation.y * 180, gunArm.transform.rotation.z);
    }

    #endregion Side to Side Movement

    #region Low Down Movement

    void LowDownMovement()
    {

        if (moveV < -0.1f && !isMidair)//Sliding and Crouching
        {

            if ((player.velocity.x > 0.1f || player.velocity.x < -0.1f))//SLIDING
            {
                Crouch(false);
                Slide(true);
            }
            else//CROUCHING
            {
                Crouch(true);
                Slide(false);
            }
            isLowDown = true;
            animator.SetBool("isLowDown", true);

        }
        else if (isLowDown)
        {
            if (isCrouching) Crouch(false);
            else Slide(false);
            isLowDown = false;
            animator.SetBool("isLowDown", false);
        }
    }
    private void Slide(bool yorn)
    {
        // yorn = y or n
        isSliding = yorn;
        standingCollider.enabled = !yorn;
        slidingCollider.enabled = yorn;
        standingGroundSensor.enabled = !yorn;
        slidingGroundSensor.enabled = yorn;
        stateOfMovement = 4;
    }

    private void Crouch( bool yorn)
    {
        // yorn = y or n
        isCrouching = yorn;
        standingCollider.enabled = !yorn;
        crouchingCollider.enabled = yorn;
        stateOfMovement = 5;
    }

    #endregion Low Down Movement

    #endregion Movement

    #region Aiming
    private void Aiming()
    {
        switch (stateOfMovement)
        {
            case 0:
                IdleAiming();
                break;
            case 1:
                RunningAiming();
                break;
            case 2:
                MidairAiming();
                break;
            case 3:
                WallAiming();
                break;
            case 4:
                SlideAiming();
                break;
            case 5:
                CrouchAiming();
                break;
        }
        animator.SetFloat("aimDirH", moveH + ( 2 * moveH * player.transform.rotation.y ));
        animator.SetFloat("aimDirV", moveV);
        animator.SetFloat("aimBack", aimBack);
    }

    void IdleAiming()
    {
        if (moveV > 0.01f)
        {
            if (aimingDirection != 4)
            {
                gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y*180, -90);
                gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.3125f, 0.063f, 0);
                gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                aimingDirection = 4;
            }
        }
        else
        {
            if (aimingDirection != 0)
            {
                gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y*180, 0);
                gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.128f, -0.1875f, 0);
                gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                aimingDirection = 0;
            }
        }
    }
    void MidairAiming()
    {
        switch (moveV)
        {
            case float n when (n == 0):
                if ((moveH > 0.01f && !facingRight) || (moveH < -0.01f && facingRight))
                {
                    if (aimingDirection != 7)
                    {
                        gunArm.transform.rotation = Quaternion.Euler(0, (gunArm.transform.parent.rotation.y * 180) + 180, 0);
                        gunArm.transform.position = gunArm.transform.parent.TransformPoint(0.0625f, -0.125F, 0);
                        gunArmSpriteRenderer.sortingOrder = frontOfCharacterLayerOrder;
                        aimingDirection = 7;
                    }
                }
                else if (aimingDirection != 1)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, 0);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.249f, -0.0625f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 1;
                }
                break;
            case float n when (n < -0.01f):
                if (aimingDirection != 2)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, 90);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.1875f, -0.0625f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 2;
                }
                break;
            case float n when (n > 0.01f):
                if (aimingDirection != 3)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, -90);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.3125f, 0.25f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 3;
                }
                break;
            default:
                if (aimingDirection != 1)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, 0);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.249f, -0.0625f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 1;
                }
                break;
        }
        return;
    }
    void RunningAiming()
    {
        switch (moveV)
        {
            case float n when (n == 0):
                if (aimingDirection != 6)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, 0);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.128f, -0.1875f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 6;
                }
                break;
            case float n when (n > 0.01f):
                if (aimingDirection != 5)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, (gunArm.transform.parent.rotation.y * 180), -90);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.3125f, 0.0625f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 5;
                }
                break;
            default:
                if (aimingDirection != 6)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, 0);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.128f, -0.1875f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 6;
                }
                break;
        }
    }
    void WallAiming()
    {
        switch (moveV)
        {
            case float n when (n == 0):
                if(aimingDirection != 8)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, (gunArm.transform.parent.rotation.y * 180) + 180, 0);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(0.375f, -0.0625f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 8;
                }
                break;
            case float n when (n > 0.01f):
                if (aimingDirection != 9)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, (gunArm.transform.parent.rotation.y * 180) + 180, -90);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(0.4375f, 0.125f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 9;
                }
                break;
            case float n when (n < -0.01f):
                if (aimingDirection != 10)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, (gunArm.transform.parent.rotation.y * 180) + 180, 90);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(0.25f, -0.1875f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 10;
                }
                break;
            default:
                if (aimingDirection != 8)
                {
                    gunArm.transform.rotation = Quaternion.Euler(0, (gunArm.transform.parent.rotation.y * 180) + 180, 0);
                    gunArm.transform.position = gunArm.transform.parent.TransformPoint(0.375f, -0.0625f, 0);
                    gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                    aimingDirection = 8;
                }
                break;
        }
    }
    void SlideAiming()
    {
        if(aimBack > 0.01f)
        {
            if (aimingDirection != 11)
            {
                gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, -90);
                gunArm.transform.position = gunArm.transform.parent.TransformPoint(0, 0, 0);
                gunArmSpriteRenderer.sortingOrder = frontOfCharacterLayerOrder;
                aimingDirection = 12;

            }
        }
        else if ((moveH + (2 * moveH * player.transform.rotation.y)) > 0.01f)
        {
            if (aimingDirection != 12)
            {

                gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, -180);
                gunArm.transform.position = gunArm.transform.parent.TransformPoint(0.125f, 0, 0);
                gunArmSpriteRenderer.sortingOrder = behindCharacterLayerOrder;
                aimingDirection = 11;
            }
        }
        else
        {
            if (aimingDirection != 13)
            {
                gunArm.transform.rotation = Quaternion.Euler(0, gunArm.transform.parent.rotation.y * 180, 0);
                gunArm.transform.position = gunArm.transform.parent.TransformPoint(-0.125f, -0.1875f, 0);
                gunArmSpriteRenderer.sortingOrder = frontOfCharacterLayerOrder;
                aimingDirection = 13;
            }
        }
    }
    void CrouchAiming()
    {

    }

    #endregion Aiming

    #region Triggers and Checks

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Platform")
        {
            isMidair = false;
            animator.SetBool("isMidair", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isMidair = true;
            animator.SetBool("isMidair", true);
        }
        
    }

    #endregion Triggers and Checks

}
