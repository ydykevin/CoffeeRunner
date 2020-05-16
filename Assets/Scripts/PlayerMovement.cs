using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator ani;
    public int movePattern; //1 can walk and jump, 2 can get down, 3 can sprint, 4 can double jump

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Normal left and right movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            doMovement(180, -1);
            //transform.localRotation = Quaternion.Euler(0, 180, 0);
            //if (Input.GetKey(KeyCode.DownArrow) && movePattern > 1)
            //{
            //    movement = Global.lieSpeed * Time.deltaTime;
            //}
            //else if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && movePattern > 2)
            //{
            //    movement = Global.sprintSpeed * Time.deltaTime;
            //    ani.SetBool("Sprint", true);
            //}
            //else
            //{
            //    ani.SetBool("Sprint", false);
            //}
            //transform.position = new Vector2(transform.position.x - movement, transform.position.y);
            //ani.SetBool("Walk", true);
            //ani.SetBool("LieStatic", false);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            doMovement(0, 1);
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            //if (Input.GetKey(KeyCode.DownArrow) && movePattern > 1)
            //{
            //    movement = Global.lieSpeed * Time.deltaTime; ;
            //}
            //else if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && movePattern > 2)
            //{
            //    movement = Global.sprintSpeed * Time.deltaTime;
            //    ani.SetBool("Sprint", true);
            //}
            //else
            //{
            //    ani.SetBool("Sprint", false);
            //}
            //transform.position = new Vector2(transform.position.x + movement, transform.position.y);
            //ani.SetBool("Walk", true);
            //ani.SetBool("LieStatic", false);
        }
        else
        {
            ani.SetBool("Sprint", false);
            ani.SetBool("Walk", false);
        }
        //End of normal left and right movement

        if (Input.GetKey(KeyCode.UpArrow))
        {

            if (Global.canJump)
            {
                Debug.Log("first jump");
                GetComponent<Rigidbody2D>().velocity = Vector2.up * Global.jumpSpeed;
                //GetComponent<ConstantForce2D>().force = new Vector2(0, Global.jumpForce);
                Global.canJump = false;
                Global.isJump = true;
                ani.SetBool("Jump", true);
            }
            else if (Global.canJump2 && movePattern > 3)
            {
                Debug.Log("second jump");
                Global.canJump2 = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.up * Global.jumpSpeed;
                ani.SetBool("Jump", true);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && movePattern > 1)
        {
            GetComponent<CapsuleCollider2D>().offset = Global.lieOffset;
            GetComponent<CapsuleCollider2D>().size = Global.lieSize;
            if (ani.GetBool("Walk"))
            {
                ani.SetBool("Lie", true);
                ani.SetBool("LieStatic", false);
            }
            else
            {
                ani.SetBool("LieStatic", true);
                ani.SetBool("Lie", false);
            }
        }
        else
        {
            GetComponent<CapsuleCollider2D>().offset = Global.normalOffset;
            GetComponent<CapsuleCollider2D>().size = Global.normalSize;
            ani.SetBool("Lie", false);
            ani.SetBool("LieStatic", false);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //GetComponent<ConstantForce2D>().force = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.up * -1;
            if (Global.isJump)
            {
                Global.canJump2 = true;
                Global.isJump = false; //but might still in the air
            }
        }

        if (!Input.anyKey)
        {
            ani.SetBool("Idle", true);
        }
        else
        {
            ani.SetBool("Idle", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            //dir = -dir.normalized;
            Debug.Log(dir);
            //Calculating collision angle, player only can jump when it stands on the ground
            if (dir.y <= Global.climbThreshold)
            {
                Global.canJump = true;
                Global.canJump2 = false;
                Global.isJump = false;
                ani.SetBool("Jump", false);
            }

        }
    }

    private void doMovement(int angle, int multipler)
    {
        float movement = Global.walkSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, angle, 0);
        if (Input.GetKey(KeyCode.DownArrow) && movePattern > 1)
        {
            movement = Global.lieSpeed * Time.deltaTime; ;
        }
        else if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && movePattern > 2)
        {
            movement = Global.sprintSpeed * Time.deltaTime;
            ani.SetBool("Sprint", true);
        }
        else
        {
            ani.SetBool("Sprint", false);
        }
        transform.position = new Vector2(transform.position.x + movement * multipler, transform.position.y);
        ani.SetBool("Walk", true);
        ani.SetBool("LieStatic", false);
    }

}
