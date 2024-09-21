using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    private float wallJumpColldown;
    [SerializeField]
    private float speed;
    [SerializeField]
    public float jumpPower;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] public AudioClip jumpSound;

    private PlayerControl control;
    Vector2 move;

    private void Awake()
    {
        //Grab reference 
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        control = new PlayerControl();
        control.Gameplay.Move.performed += x => move = x.ReadValue<Vector2>();
        control.Gameplay.Move.canceled += x => move = Vector2.zero;
        control.Gameplay.Jump.performed += x => Jump();
        control.Gameplay.Attack.canceled += x => move = Vector2.zero;
        //control.Gameplay.Menu.performed += x => Grow();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);


        //flip player when move
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
        }



        //set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("ground", isGrounded());

        //wall jump logic
        if (wallJumpColldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpColldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            SoundManager.instance.PlaySound(jumpSound);
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, jumpPower);
            }
            wallJumpColldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
    private void OnEnable()
    {
        control.Gameplay.Enable();
    }
    private void OnDisable()
    {
        control.Gameplay.Disable();
    }
    void Grow()
    {
        transform.localScale *= 1.1f;
    }
}
