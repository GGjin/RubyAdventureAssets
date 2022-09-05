using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{

    public int amount = 1;
    //添加触发器碰撞事件，每次碰撞触发器时，执行其中的代码
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"和当前物体发生碰撞的对象是:{other}");
        //获取Ruby游戏对象的脚本对象
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null)
        {
            if (rubyController.health < rubyController.maxHealth)
            {

                rubyController.ChangeHealth(amount);
                Destroy(gameObject);
            }
        }
        else
        {

        }


    }
}
