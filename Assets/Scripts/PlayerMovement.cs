using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator ani;
    public int movePattern; //1 can walk and jump, 2 can get down, 3 can sprint, 4 can double jump
    private GameObject portalPrefab;
    private GameObject portal;
    private float endCount = 0;
    private GameObject coffee;
    private bool openPortal = false;
    private bool removeBody = false;
    private int countCollider = 0;
    private bool finishFadeout = false;
    private SpriteRenderer black;
    private bool iceLeft = false;
    private bool iceRight = false;
    private bool showMovement;
    private SpriteRenderer movement;
    

    // Start is called before the first frame update
    void Start()
    {
        portalPrefab = Resources.Load("Portal") as GameObject;
        Global.stopPlayer = false;
        coffee = GameObject.FindGameObjectWithTag("Coffee");
        black = GameObject.Find("Black").GetComponent<SpriteRenderer>();
        if (GameObject.Find("Movement"))
        {
            showMovement = true;
            movement = GameObject.Find("Movement").GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > 5 && showMovement)
        {
            movement.color = new Vector4(1, 1, 1, movement.color.a - 0.01f);
            if(movement.color.a <= 0)
            {
                showMovement = false;
            }
        }
        if (Global.stopPlayer)
        {
            foreach (AnimatorControllerParameter parameter in ani.parameters)
            {
                ani.SetBool(parameter.name, false);
            }
            endCount += Time.deltaTime;
            if (!removeBody)
            {
                ani.SetBool("Idle", true);
                Destroy(GetComponent<Rigidbody2D>());
                //GetComponent<Rigidbody2D>().gravityScale = 0;
                //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                removeBody = true;
            }
            if (endCount>=6&&!finishFadeout)
            {
                //Debug.Log(black.color.a);
                if (black.color.a < 1)
                {
                    black.color = new Vector4(0, 0, 0, black.color.a + 0.01f);
                }
                else
                {
                    finishFadeout = true;
                }
            }
            if (endCount > 5)
            {
                portal.transform.localScale = new Vector2(portal.transform.localScale.x * 0.97f, portal.transform.localScale.y * 0.97f);
            }
            if (endCount >= 4.3)
            {
                //portal.transform.localScale = new Vector2(2,2);
                ani.SetBool("Drink", false);
                if (transform.rotation.y == 0)
                {
                    transform.Rotate(new Vector3(0, 0, -6), Space.Self);
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, 6), Space.Self);
                }
                transform.localScale = new Vector2(transform.localScale.x * 0.975f, transform.localScale.y * 0.975f);
                
            }
            else if (endCount >= 3.2)
            {
                if (!openPortal)
                {
                    portal = Instantiate(portalPrefab, transform.position, Quaternion.identity);
                    portal.transform.localScale = new Vector2(0.1f, 0.1f);
                    openPortal = true;
                }
                if (portal!=null)
                {
                    if (portal.transform.localScale.x < 2)
                    {
                        portal.transform.localScale = new Vector2(portal.transform.localScale.x + 0.04f, portal.transform.localScale.y + 0.04f);
                    }
                }
            }
            else if (endCount >= 1.4)
            {
                coffee.SetActive(false);
                ani.SetBool("Drink", true);
            }
            else if (endCount >= 0.5)
            {
                coffee.transform.position = new Vector2((transform.position.x - coffee.transform.position.x) > 0 ? coffee.transform.position.x + 0.005f : coffee.transform.position.x - 0.005f, (transform.position.y - coffee.transform.position.y) > 0 ? coffee.transform.position.y + 0.005f : coffee.transform.position.y - 0.005f);
                coffee.transform.localScale = new Vector2(coffee.transform.localScale.x * 0.985f, coffee.transform.localScale.y * 0.985f);
            }
        }
        else
        {
            //Normal left and right movement
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                doMovement(180, -1);
                if (movePattern == 3)
                {
                    //rb2d.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Impulse);
                    //GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);
                    iceLeft = true;
                    iceRight = false;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                doMovement(0, 1);

                if (movePattern == 3)
                {
                    //GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);
                    iceLeft = false;
                    iceRight = true;
                }
            }
            else
            {
                ani.SetBool("Sprint", false);
                ani.SetBool("Walk", false);
            }
            //End of normal left and right movement

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Global.canJump)
                {
                    //Debug.Log("first jump"+countCollider);
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * Global.jumpSpeed;
                    //GetComponent<ConstantForce2D>().force = new Vector2(0, Global.jumpForce);
                    Global.canJump = false;
                    Global.isJump = true;
                    ani.SetBool("Jump", true);
                }
                else if ((Global.canJump2 )&& movePattern > 3)
                {
                    //Debug.Log("second jump"+countCollider);
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

            if (Input.GetKeyUp(KeyCode.UpArrow)&&Global.isJump)
            {
                //GetComponent<ConstantForce2D>().force = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = Vector2.up * -1;
                
                //Debug.Log("can double");
                Global.canJump2 = true;
                Global.isJump = false; //but might still in the air
                
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
    }

    private void FixedUpdate()
    {
        if (iceLeft && !Global.stopPlayer)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.2f, 0), ForceMode2D.Impulse);
        }
        else if (iceRight && !Global.stopPlayer)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.2f, 0), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            countCollider++;
            Vector2 dir = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            //dir = -dir.normalized;
            //Debug.Log(dir);
            //Calculating collision angle, player only can jump when it stands on the ground
            if (dir.y <= Global.climbThreshold)
            {
                //Debug.Log("can jump"+dir);
                Global.canJump = true;
                Global.canJump2 = false;
                Global.isJump = false;
                ani.SetBool("Jump", false);
                ani.SetBool("Idle", true);
            }
        }
        if (collision.collider.tag == "Spike")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = GameObject.FindGameObjectWithTag("Check").transform.position;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            countCollider--;
            if (countCollider == 0)
            {
                //Debug.Log("can air");
                Global.canJump = false;
                Global.canJump2 = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coffee")
        {
            //Debug.Log("enter coffee");
            Global.stopPlayer = true;
            StartCoroutine(nextScene());
        }
        if (collision.tag == "Reset")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = GameObject.FindGameObjectWithTag("Check").transform.position;
        }
        if (collision.tag == "Fireball"&&!Global.stopPlayer)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = GameObject.FindGameObjectWithTag("Check").transform.position;
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(7.6f);

        if (SceneManager.GetActiveScene().buildIndex+1!=SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
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
