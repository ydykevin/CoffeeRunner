using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Global
{
    public static float walkSpeed = 2f;
    public static float lieSpeed = 1f;
    public static float sprintSpeed = 3.5f;
    public static bool canJump = false;
    public static bool canJump2 = false;
    public static bool isJump = false;
    public static float jumpInterval = 1f;
    public static float jumpTime = 0.2f;
    public static float jumpForce = 30;
    public static float climbThreshold = -0.658f; //climb 45 degree slope
    public static float jumpSpeed = 5f;
    public static Vector2 normalOffset = new Vector2(0, 0);
    public static Vector2 normalSize = new Vector2(0.5f, 1.48f);
    public static Vector2 lieOffset = new Vector2(0,-0.37f);
    public static Vector2 lieSize = new Vector2(0.5f, 0.74f);
}
