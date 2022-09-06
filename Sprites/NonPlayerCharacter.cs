using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    //添加对话框显示时长
    public float displayTime = 4.0f;
    //用来获取对话框游戏对象
    public GameObject dialogBox;

    //创建一个对象，来获取tmp组件
    public GameObject dlgTxtProGameObject;
    //创建游戏组件类对象
    TMPro.TextMeshProUGUI tmTxtBox;
    //声明当前页
    int currentPage = 1;
    int totalPages;
    //计时器，用来倒计时文本框显示的时间
    float timerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
        tmTxtBox = dlgTxtProGameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        totalPages = tmTxtBox.textInfo.pageCount;
        if (timerDisplay >= 0)
        {

            //监测用户输入，每次空格键弹起时激活
            if (Input.GetKeyUp(KeyCode.Space))
            {//如果没到最后一页，就向后翻页
                if (currentPage < totalPages)
                {
                    currentPage++;
                }
                else
                {
                    currentPage = 1;
                }
                tmTxtBox.pageToDisplay = currentPage;
                DisplayDialog();
            }

            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        //重置计时器
        timerDisplay = displayTime;
        //显示对话框
        dialogBox.SetActive(true);
    }

}
