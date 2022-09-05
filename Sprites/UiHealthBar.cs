using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthBar : MonoBehaviour
{

    //构造单例，获取当前血条本身
    public static UiHealthBar Instance { get; private set; }

    //创建Ui图像对象 mask
    public Image mask;

    float originalSize;

    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValue(float value)
    {
        //设置更改的是mask遮罩层的宽度，
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);

    }

}
