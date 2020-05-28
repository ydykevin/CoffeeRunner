using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//负责生成火球
public class FireBallManager : MonoBehaviour
{
    private GameObject fireBall;
    void Start()
    {
       fireBall= Resources.Load<GameObject>("Fireball");

        //反复调用CreatFireBall方法，改第一个数字可以修改产生火球的频率，修改第二个可以修改第一个火球出现的时间
        InvokeRepeating("CreatFireBall", 2, 2);
    }

    //生成火球
    private void CreatFireBall()
    {
        Instantiate<GameObject>(fireBall, new Vector2(10, Random.Range(-3.5f, 1)), Quaternion.identity);
    }

    private void Update()
    {
        //打包后的exe可执行文件esc可直接退出程序
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }
}
