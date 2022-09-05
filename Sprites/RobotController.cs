using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{

    public float maxX;

    public float maxY;

    public float speed = 1f;

    float positionX = 0;
    float positionY;

    float initX;

    bool isBackX = false;

    private Animator animator;

    Rigidbody2D robot2D;
    Vector2 position;
    bool isBroked = true;
    //开放接口，用来获取烟雾特效
    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        robot2D = GetComponent<Rigidbody2D>();
        position = robot2D.position;
        initX = position.x;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isBroked)
        {
            return;
        }
        // Debug.Log(position.x);

        if (maxX != 0f)
        {

            if (positionX >= maxX || isBackX)
            {
                positionX += speed * Time.deltaTime;
                position.x -= positionX;
                if (initX - position.x >= maxX)
                {
                    isBackX = false;
                    positionX = 0;
                }
            }
            else
            {
                positionX += speed * Time.deltaTime;
                position.x += positionX;

                if (position.x - initX >= maxX)
                {
                    isBackX = true;
                    positionX = 0;
                }
            }
            animator.SetFloat("MoveX", positionX);
        }
        if (maxY != 0f)
        {

        }
        // Debug.Log(position.x);
        robot2D.MovePosition(position);
    }


    //刚体碰撞事件，在这个方法中添加对玩家伤害的逻辑
    private void OnCollisionEnter(Collision other)
    {
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        //更改状态为已修复
        isBroked = false;
        //让机器人不会再碰撞
        //这里采用的是刚体对象取消物理引擎效果
        robot2D.simulated = false;
        animator.SetTrigger("Fix");
        // Destroy(smokeEffect);
        //特效停止产生，但是已产生的会走完声明周期
        smokeEffect.Stop();
    }

}
