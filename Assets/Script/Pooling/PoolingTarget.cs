using System.Collections;
using UnityEngine;

public class PoolingTarget : MonoBehaviour
{
    public PoolingTag poolingTag;
    public bool isOneTime;
    public float effectTimer;

    public void Init()
    {
        if (isOneTime)
        {
            StartCoroutine(ReturnCounter());
        }
    }

    private IEnumerator ReturnCounter()
    {
        yield return new WaitForSeconds(effectTimer);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        PoolingManager.Instance.ReturnToPool(poolingTag, gameObject);
    }
}