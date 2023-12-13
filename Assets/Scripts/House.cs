using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField, Range(0.05f, 2f)] private float _volumeChangingSpeed;

    private AudioSource _alarm;

    private bool _isThiefInHome;
    private float _maxVolume;
    private float _minVolume;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = 0;
        _maxVolume = 1.0f;
        _minVolume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _alarm.Play();
            _isThiefInHome = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isThiefInHome = false;
        }
    }

    private void Update()
    {
        if (_isThiefInHome)
        {
            if (_alarm.volume < _maxVolume)
                _alarm.volume += Time.deltaTime * _volumeChangingSpeed;
        }      
        else
        {
            if (_alarm.volume > _minVolume)
                _alarm.volume -= Time.deltaTime * _volumeChangingSpeed;
            else
                _alarm.Stop();
        }
    }
}
