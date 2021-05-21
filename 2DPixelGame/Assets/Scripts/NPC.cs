using UnityEngine;

public class NPC : MonoBehaviour
{

    [Header("商店介面")]
    public GameObject ObjShop;

    /// <summary>
    /// 玩家選取的武器
    /// 0 短刀 1元
    /// 1 弓 2元
    /// 2 斧 3元
    /// </summary>
    public int indexWeapon;


    /// <summary>
    /// 武器的價格，編號與選取武器相同
    /// </summary>
    private int[] priceWeapon = { 1, 2, 3 };
    private float[] attackWeapon = { 10, 50, 100 };

    /// <summary>
    /// 玩家身上武器的物件，編號與選取武器相同
    /// </summary>
    public GameObject[] objWeapon; 

    private Player player;
    private void Start()
    {
        player = GameObject.Find("人物").GetComponent<Player>();
    }


    /// <summary>
    /// 開啟商店介面
    /// </summary>
    public void OpenShop()
    {
        ObjShop.SetActive(true);
    }
    /// <summary>
    /// 關閉商店介面
    /// </summary>
    public void CloseShop()
    {
        ObjShop.SetActive(false);
    }

    /// <summary>
    /// 玩家選了哪一個武器
    /// </summary>
    /// <param name="choose">武器編碼</param>
    public void ChooseWeapon(int choose)
    {
        indexWeapon = choose;
    }

    /// <summary>
    /// 購買武器
    /// 1.判斷玩家金幣是否足夠
    /// 2.購買後扣除金幣更新介面
    ///3. 顯示武器
    /// </summary>
    public void Buy()
    {
        if (player.coin >= priceWeapon[indexWeapon])
        {

            player.coin -= priceWeapon[indexWeapon];
            player.textCoin.text = "金幣:" + player.coin;
            //將目前購買的武器攻擊力給玩家
            player.attackWeapon = attackWeapon[indexWeapon];

            //顯示武器前關閉所有武器
            for (int i =0; i < objWeapon.Length; i++)
            {
                objWeapon[i].SetActive(false);
            }

            objWeapon[indexWeapon].SetActive(true);

        }
    }
   
}
