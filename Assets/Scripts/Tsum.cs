using System;
using UnityEngine;

public class Tsum : MonoBehaviour
{
    public int IndexType;
    public bool CanBeSelected;

    private void OnMouseDrag()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, 0.8f);

        foreach (var collider in collider2DArray)
        {
            if (collider.gameObject.GetComponent<Tsum>().IndexType == IndexType)
            {
                collider.gameObject.GetComponent<Tsum>().CanBeSelected = true;
            }
        }
    }
    // private void O
    // {
    //     
    //     
    //     Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, 0.8f);
    // }
}