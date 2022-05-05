using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BorderType
{
    LeftBorder,
    RightBorder,
    TopBorder,
    BottomBorder
}

public class Border : MonoBehaviour
{
    public BorderType type;

    public Action<Transform, BorderType> OnBallIn = (transform1, borderType) => { };

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnBallIn?.Invoke(other.transform,type);
    }
}