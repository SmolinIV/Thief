
using UnityEngine;

[RequireComponent (typeof(Animator))]

public class Thief : MonoBehaviour
{
    [SerializeField, Min(0.5f)] private float _movingSpeed;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        int leftDirectionDegree = 180;
        int rightDirectionDegree = 0;

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            Move(rightDirectionDegree);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(leftDirectionDegree);
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

        transform.rotation = new Quaternion(0, degree, 0, 0);
        transform.Translate(_movingSpeed * Time.deltaTime, 0, 0);
    }
}
