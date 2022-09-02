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
    private float attackCoolDown;

    private Vector2 updatePos; // checks player pos for attacks

    private bool isGrounded;
    private bool isFacingRight; // determins the direction the player faces for attacks
    private bool canMove = true;

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
        if (canMove)
        {
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
            isFacingRight = true;
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
            canMove = false;
            attackCoolDown = 0.65f;
            StartCoroutine(damageWait());

            Debug.Log("Medium Attack");
            medHitBox = Instantiate(MdamageBox);

            if (isFacingRight) medHitBox.GetComponent<DamageBox>().isFacingRight = true;
            else if (!isFacingRight) medHitBox.GetComponent<DamageBox>().isFacingRight = false;
            medHitBox.transform.position = updatePos;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            canMove = false;
            attackCoolDown = 0.5f;
            StartCoroutine(damageWait());

            Debug.Log("Light Attack");
            lightHitBox = Instantiate(LdamageBox);

            if (isFacingRight) lightHitBox.GetComponent<DamageBox>().isFacingRight = true;
            else if (!isFacingRight) lightHitBox.GetComponent<DamageBox>().isFacingRight = false;
            lightHitBox.transform.position = updatePos;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            canMove = false;
            attackCoolDown = 1f;
            StartCoroutine(damageWait());

            heavyHitBox = Instantiate(HdamageBox);
            if (isFacingRight) heavyHitBox.GetComponent<DamageBox>().isFacingRight = true;
            else if (!isFacingRight) heavyHitBox.GetComponent<DamageBox>().isFacingRight = false;
            heavyHitBox.transform.position = updatePos;

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

    // ------------------------------ Attack IEnumerator ----------------------------------------

    IEnumerator damageWait()
    {
        yield return new WaitForSeconds(attackCoolDown);
        Debug.Log("Attack delay over");
        canMove = true;
    }
}
