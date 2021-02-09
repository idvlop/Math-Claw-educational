using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    public static Transform UsedPool;
    public static void RemoveUsedGashapon(GameObject gashapon)
    {
        gashapon.transform.parent = UsedPool;
        gashapon.SetActive(false);
    }
}
