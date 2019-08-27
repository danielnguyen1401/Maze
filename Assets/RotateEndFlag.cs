using DG.Tweening;
using UnityEngine;

public class RotateEndFlag : MonoBehaviour
{
    public float speed;
    private float _yPosition;

    private void Start()
    {
        speed *= Time.deltaTime;
        _yPosition = transform.position.y;
        MoveUpAndDown();
    }

    private void MoveUpAndDown()
    {
        transform.DOMoveY(_yPosition + 0.5f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * speed);
    }
}