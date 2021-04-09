using UnityEngine;

public class ltem : MonoBehaviour
{
    [Header("掉落物品")]
    public GameObject prop;
    [Header("掉落機率"), Range(0f, 1f)]
    public float percent = 0.7f;


}
