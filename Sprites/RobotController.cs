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


    Rigidbody2D robot2D;
    Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        robot2D = GetComponent<Rigidbody2D>();
        position = robot2D.position;
        initX = position.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
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

}
