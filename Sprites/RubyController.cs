using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{


    public GameObject projectilePrefab;

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

    private Animator animator;
    //创建一个二维矢量，用来存储Ruby静止不动时的方向（即面向的方向）
    //与机器人象必，Ruby可以站立不动，站立不动时，Move X和Y均为0，这时状态机就没有办法获取Ruby精致时的方向，所以需要手动设置一个
    Vector2 lookDirection = new Vector2(1, 0);

    //移动二维矢量
    Vector2 move;

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
        animator = GetComponent<Animator>();
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
        //创建一个二维矢量对象来表示Ruby移动的数据信息
        move = new Vector2(horizontal, vertical);

        //如果move中的xy不为零，表示正在移动
        //将ruby面向方向设置为移动方向
        //停止移动，保持以前方向，所以这个if结构用于转向时重新赋值面朝方向
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //使用向量长度为1，可以将此方法称为向量的 “归一化”方法
            //通常用在表示方向，而非位置的额向量上
            //因为 blend tree中表示方向的参数值取值范围时-1.0到1.0
            //所以一般用向量作为Animator.SetFloat方法的参数时，一般要对向量进行“归一化”处理
            lookDirection.Normalize();
        }
        //传递Ruby面朝方向给 blend tree
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        //传递Ruby速度给blend tree
        //矢量的magitue属性，用来返回矢量的长度，是一个绝对值
        animator.SetFloat("Speed", move.magnitude);

        //添加子弹发射逻辑
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //创建一个射线投射碰撞对象，来接收射线投射碰撞信息
            //射线投射使用的是Physics2D.Raycast方法：
            //(射线投射的位置，投射方向，投射距离，射线生效的层)
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log($"射线投射碰撞到的对象是：{hit.collider.gameObject}");
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
        UiHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);
        animator.SetTrigger("Hit");

    }


    void Launch()
    {
        //在指定位置创建游戏对象
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0 / 5f, Quaternion.identity);
        //获取子弹游戏对象的脚本组件对象
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        //通过脚本对象掉用子弹移动方法
        //第一个参数时移动的方向，取得时玩家面朝的方向
        //第二个参数是力的大小
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

}
