using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("追蹤範圍"), Range(0, 500)]
    public float rangeTrack = 2;

    private Transform player;
    private void Start()
    {
        //玩家變形=尋找遊戲物件("物件名稱").變形
        player = GameObject.Find("人物").transform;
    }


    //繪製圖示事件:在Unity內顯示輔助開發
    private void OnDrawGizmos()
    {
        //先指定顏色在畫圖
        Gizmos.color = new Color(0, 0, 1, 0.35f);
        //繪製球體(中心點，半徑)
        Gizmos.DrawSphere(transform.position, rangeTrack);
    }

    private void Update()
    {
        Track();
    }

    /// <summary>
    /// 追蹤玩家
    /// </summary>
    private void Track() 
    {
        //距離 等於 三維向量 的 距離(A點，B點)
        float dis = Vector3.Distance(transform.position, player.position);
        print("距離:" + dis);

        //如果 距離 小魚等於 追蹤範圍 才開始追蹤
        if (dis <= rangeTrack) 
        {
            print("追蹤");
        }


    }

}
