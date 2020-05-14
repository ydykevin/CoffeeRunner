using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator ani;
    private float jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float movement = Global.walkSpeed * Time.deltaTime;


        //Normal left and right movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector2(transform.position.x - movement, transform.position.y);
            ani.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector2(transform.position.x + movement, transform.position.y);
            ani.SetBool("Walk", true);
        }
        else
        {
            ani.SetBool("Walk", false);
        }
        //End of normal left and right movement

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Global.canJump && !Global.isJump)
            {
                GetComponent<ConstantForce2D>().force = new Vector2(0, Global.jumpForce);
                Global.isJump = true;
                ani.SetBool("Jump", true);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

        }

        if (Global.isJump)
        {
            Global.canJump = false;
            jumpCount += Time.deltaTime;
            if (jumpCount >= Global.jumpTime)
            {
                GetComponent<ConstantForce2D>().force = new Vector2(0, 0);
                jumpCount = 0;
                Global.isJump = false; //But might still in the air
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;
            //Calculating collision angle, player only can jump when it stands on the ground
            if (Mathf.Abs(dir.x) <= 0.1)
            {
                Global.canJump = true;
                Global.isJump = false;
                ani.SetBool("Jump", false);
            }

        }
    }

}
