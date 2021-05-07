﻿using UnityEngine;
using UnityEngine.UI; //引用介面API
using UnityEngine.SceneManagement;    //引用場景管理API


public class Player : MonoBehaviour
{
    //註解

    //欄位語法
    //修飾詞 類型 名稱 (指定 值);
    //私人 private 不顯示 (預設值)
    //公開 public 顯示

    //類型 四大類型
    //整數 int
    //浮點數 float
    //布林值 bool true是、false否
    //字串 string 

    [Header("等級")]
    [Tooltip("這是角色的等級")]
    public int lv = 1;
    [Header("移動數度"), Range(0, 300)]
    public float speed = 10.5f;
    [Header("角色名稱"), Tooltip("這是角色的名稱")]
    public string cName = "貓咪";
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("攻擊音樂")]
    public AudioClip soundAttack;
    [Header("血量")]
    public float hp = 200;
    [Header("血條系統")]
    public HpManager hpManager;
    [Header("攻擊力"), Range(0, 1000)]
    public float attack = 20;
    [Header("等級文字")]
    public Text textLv;



    private bool isDead = false;
    private float hpMax;



    




    //事件:繪製圖示
    private void OnDrawGizmos()
    {
        //指定圖示顏色(紅，綠，藍，透明)
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        //繪製圖示 球體(中心點，半徑)
        Gizmos.DrawWireSphere(transform.position, rangeAttack);


    }



    //方法語法 Method - 儲存複雜的程式區塊或演算法
    //修飾詞 類型 名稱 () {程式區塊或演算法}
    //void 無類型

    /// <summary>
    /// 移動
    /// </summary>

    private void Move()
    {

        if (isDead) return;    //如果死亡就跳出

        //print("移動");
        float h = joystick.Horizontal;
        //print("水平:" + h);

        float v = joystick.Vertical;
        //print("垂直:" + v);

        //變形元件.位移(水平*速度*一幀的時間，垂直*速度*一幀的時間，0)
        tra.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        ani.SetFloat("水平", h);
        ani.SetFloat("垂直", v);

    }

    //要被按鈕呼叫必須設定為公開 public
    public void Attack()
    {

        if (isDead) return;    //如果死亡就跳出


        //音效來源，播放一次(音效片段，音量)
        aud.PlayOneShot(soundAttack, 0.5f);

        //2D 物理 圓形碰撞 (中心點，半徑，方向，距離，圖層)
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rangeAttack, transform.up, 0, 1 << 8);

        //如果 碰到物件存在 並且 碰到的物件 標籤 為 取得道具腳本並呼叫道具掉落方法
        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<Item>().DropProp();
        //如果 打擊的標籤是 敵人 就對她造成傷害
        if (hit && hit.collider.tag == "敵人") hit.collider.GetComponent<Enemy>().Hit(attack);


    }

    //要被其他腳本呼叫也要設定為公開public
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害值</param>

    public void Hit(float damage)
    {
        hp -= damage;                            //扣除傷害值
        hpManager.UpdateHpBar(hp, hpMax);        //更新血條
        StartCoroutine(hpManager.ShowDamage(damage));  //啟動協同程序(顯示傷害值())


        if (hp <= 0) Dead();               //如果血量<=0就死亡

    }

    /// <summary>
    /// 死亡
    /// </summary>

    private void Dead()
    {
        hp = 0;
        isDead = true;
        Invoke("Replay", 2);    //延遲呼叫("方法名稱",延遲時間)

    }

    private void Replay() 
    {

        SceneManager.LoadScene("遊戲場景");

    }


    //事件 - 特定時間會執行的方法
    //開始事件:撥放後執行一次
    private void Start()
    {
        hpMax = hp;   //取得血量最大值

    }
    //更新事件:大約一秒執行60次 60FPS
    private void Update()
    {
        //呼叫方法
        //方法名稱();
        Move();
    }

    [Header("吃金塊音效")]

    public AudioClip soundEat;

    [Header("金塊數量")]
    public Text textCoin;

    private int coin;

    //觸發事件 - 進入:兩個物件必須有一個勾選 Is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="金塊")
        {
            coin++;
            aud.PlayOneShot(soundEat);
            Destroy(collision.gameObject);
            textCoin.text = "金幣:" + coin;

        }
    }
}
