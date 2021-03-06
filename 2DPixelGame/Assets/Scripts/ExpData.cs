﻿using UnityEngine;


[CreateAssetMenu(fileName = "經驗值資料", menuName = "me/經驗值資料")]
public class ExpData : ScriptableObject
{
    //陣列
    //語法:在類型後面加上一對中括號
    //陣列的用法:儲存多筆相同類型的資料
    [Header("每個等級經驗值需求，從一等開始")]
    public float[] exp;
}
