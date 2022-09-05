using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;

    //公开一个特效 来挂接粒子特效
    public ParticleSystem hitEffect;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果没有碰撞到任何碰撞体，在飞行距离超过100后自动销毁
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        //通过刚体对象掉用物理系统的AddForce方法
        //对游戏对象施加一个力，使其移动
        rigidbody2D.AddForce(direction * force);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //获取齿轮飞弹碰撞到的机器人对象脚本组件
        RobotController robotController = other.collider.GetComponent<RobotController>();
        if (robotController != null)
        {
            robotController.Fix();
        }
        Debug.Log($"齿轮子弹碰撞到了：{other.gameObject}");
        Destroy(gameObject);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}
