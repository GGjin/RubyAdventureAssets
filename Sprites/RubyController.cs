using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public int maxHealth = 5;

    public float timeInvincible = 2f;

    bool isInvincible = false;

    float invincibleTimer;

    public int health
    {
        get { return currentHealth; }
        // set { currentHealth = value;}
    }

    private int currentHealth;

    Rigidbody2D rigidbody2D;

    float horizontal;
    float vertical;



    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        //只有将垂直同步计数设置为0，才能锁帧，否则锁帧的代码无效
        //垂直同步的作用就是显著减少游戏画面撕裂、跳帧，因为画面的渲染不是整个画面
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;

        //获取当前游戏对象的刚体组件
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            //如果无敌， 进入倒计时
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2D.position = position;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        //限制方法，限制当前生命值的复制范围：0-最大生命值
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("当前生命值：" + currentHealth + "/" + maxHealth);



    }

}
