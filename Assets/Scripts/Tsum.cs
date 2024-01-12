using System;
using DG.Tweening;
using UnityEngine;

public class Tsum : MonoBehaviour
{
    public int IndexType;
    public bool IsSelected;

    private void Update()
    {
        if (IsSelected)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (TsumManager.Instance.FirstSelected == null)
        {
            IsSelected = true;
            TsumManager.Instance.FirstSelected = this;
            TsumManager.Instance.TsumsSelected.Add(this);
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsSelected == false)
            {
                float dist = Vector2.Distance(transform.position,
                    TsumManager.Instance.TsumsSelected[^1].transform.position);

                print($"Distance between the last and the actual selected : " + dist);

                if (IndexType == TsumManager.Instance.FirstSelected.IndexType && dist <= 1.1f)
                {
                    IsSelected = true;
                    TsumManager.Instance.TsumsSelected.Add(this);
                }
            }
            else if (this == TsumManager.Instance.TsumsSelected[^2])
            {
                TsumManager.Instance.TsumsSelected[^1].IsSelected = false;
                TsumManager.Instance.TsumsSelected.RemoveAt(TsumManager.Instance.TsumsSelected.Count - 1);
            }
        }
    }
}