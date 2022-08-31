using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // decleration
    public Rigidbody2D characterController;

    // damage boxes
    [Header("Damage Boxes")]
    public GameObject LdamageBox;
    public GameObject MdamageBox;
    public GameObject HdamageBox;

    // private float gravityValue = 9.81f;
    private float playerSpeed = 5f;
    private float jumpHeight = 13f;

    private Vector2 updatePos; // checks player pos for attacks

    private bool isGrounded;
    private bool isFacingRight; // determins the direction the player faces for attacks

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {      
        CheckGrounded();
        CheckMove();
        PlayerAttack();
    }

    void CheckGrounded()
    {
        // player has to be on "Igonre Raycast Layer" for this to work, might need work.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
        if (hit.collider != null)
        {
            //Debug.Log("isGrouned");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            FallingSpeed();
        }
    }

    // -------------------------------- Player Movement ----------------------------------------

    void CheckMove() // checks to see if player is allowed to move
    {
        if (!Input.GetKey(KeyCode.G))
        {
            // add attack wait time here
            PlayerMove();
            PlayerJump();
        }
    }

    void PlayerMove()
    {
        Vector2 moveHorizontal = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.Translate(moveHorizontal * Time.deltaTime * playerSpeed);

        // Determin Orintaion
        if (Input.GetKeyDown(KeyCode.D))
        {
            isFacingRight = true;;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            isFacingRight = false;         
        }

        // facing check
        if (isFacingRight)
        {
            updatePos = new Vector2((transform.position.x + 1), transform.position.y + 1);
        }
        else if (!isFacingRight)
        {
            updatePos = new Vector2((transform.position.x - 1), transform.position.y + 1);
        }
    }

    void PlayerJump()
    {          
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            characterController.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    // ----------------------------------- Move Set ---------------------------------------------

    void PlayerAttack()
    {
        GameObject medHitBox;
        GameObject lightHitBox;
        GameObject heavyHitBox;

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Medium Attack");
            medHitBox = Instantiate(MdamageBox);

            if (isFacingRight) medHitBox.GetComponent<DamageBox>().isFacingRight = true;
            else if (!isFacingRight) medHitBox.GetComponent<DamageBox>().isFacingRight = false;
            medHitBox.transform.position = updatePos;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Light Attack");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Heavy Attack");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Special");
        }
    }

    // ------------------------------ Aditional Physisics ---------------------------------------

    void FallingSpeed()
    {
        Vector2 fallSpeed = new Vector2(0, -1);

        characterController.AddForce(fallSpeed, ForceMode2D.Force);
    }
}
