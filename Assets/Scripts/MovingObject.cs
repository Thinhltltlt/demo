using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] protected Transform movingObject;
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected float duration = 3;

    private void Start()
    {
        movingObject.DOMove(direction, duration).SetLoops(-1, LoopType.Yoyo).SetRelative(true).SetEase(Ease.Linear);
    }
}
