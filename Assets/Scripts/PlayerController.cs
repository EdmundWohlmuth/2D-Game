using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // decleration
    public Rigidbody2D characterController;

    private float gravityValue = 9.81f;
    private float playerSpeed = 5f;
    private float jumpHeight = 10f;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        PlayerMove();
        PlayerJump();
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
        }
    }

    // -------------------------------- Player Movement ----------------------------------------
    void PlayerMove()
    {
        Vector2 moveHorizontal = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.Translate(moveHorizontal * Time.deltaTime * playerSpeed);
    }

    void PlayerJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            characterController.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }
}
