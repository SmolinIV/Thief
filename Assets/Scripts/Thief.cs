using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField, Min(0.5f)] private float _movingSpeed;

    private Animator _animator;
    private Quaternion _rotation;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rotation = transform.rotation;
    }

    public void Update()
    {
        int rightMovingDegree = 0;
        int leftMovigDegree = 180;

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            Move(rightMovingDegree);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(leftMovigDegree);
        }
        else
        {
            _animator.Play(0);
            _animator.speed = 0;
        }
    }

    private void Move(int degree)
    {
        _animator.speed = 1;
        _rotation.y = degree;

        transform.rotation = _rotation;
        transform.Translate(_movingSpeed * Time.deltaTime, 0, 0);
    }
}
