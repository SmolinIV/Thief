using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField, Range(0.2f, 2f)] private float _volumeChangingSpeed;

    private AudioSource _alarm;
    private Coroutine _volumeChange;

    private float _maxVolume;
    private float _minVolume;
    private bool _isThiefAtHome;
    private bool _isAlarmPlay;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
        _alarm.volume = 0;
        _maxVolume = 1.0f;
        _minVolume = 0f;

        _isAlarmPlay = false;
    }

    private void OnDestroy() => StopCoroutine(_volumeChange);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isThiefAtHome = true;
            _volumeChange = StartCoroutine(IncreaseAlarmVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isThiefAtHome = false;
            _volumeChange = StartCoroutine(DecreaseAlarmVolume());
        }
    }

    private IEnumerator IncreaseAlarmVolume()
    {
        if (!_isAlarmPlay)
        {
            _alarm.Play();
            _isAlarmPlay = true;
        }

        while (_alarm.volume < _maxVolume)
        {
            if (!_isThiefAtHome)
                yield break;

            _alarm.volume += Time.deltaTime * _volumeChangingSpeed;

            yield return null;
        }
    }

    private IEnumerator DecreaseAlarmVolume()
    { 
        while (_alarm.volume > _minVolume)
        {
            if (_isThiefAtHome)
                yield break;

            _alarm.volume -= Time.deltaTime * _volumeChangingSpeed;

            yield return null;
        }

        _alarm.Stop();
        _isAlarmPlay = false;
    }
}
