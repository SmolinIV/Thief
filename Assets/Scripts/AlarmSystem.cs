using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSystem : MonoBehaviour
{
    [SerializeField, Range(0.01f, 2f)] private float _volumeChangingSpeed;

    private AudioSource _alarm;
    private Coroutine _alarmVolumeControl;

    private float _maxVolume;
    private float _minVolume;

    private bool _isAlarmPlay;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
        _maxVolume = 1.0f;
        _minVolume = 0f;
        _alarm.volume = _minVolume;
    }

    private void OnDestroy() => StopCoroutine(_alarmVolumeControl);

    public void TurnOn()
    {
        if (!_isAlarmPlay)
        {
            _alarm.Play();
            _isAlarmPlay = true;
        }

        if (_alarmVolumeControl != null)
            StopCoroutine(_alarmVolumeControl);

        _alarmVolumeControl = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void TurnOff()
    {
        if (_alarmVolumeControl != null)
            StopCoroutine(_alarmVolumeControl);

        _alarmVolumeControl = StartCoroutine(ChangeVolume(_minVolume));
    }

    private IEnumerator ChangeVolume(float neededVolumeValue)
    {
        while (Mathf.Abs(_alarm.volume - neededVolumeValue) > float.Epsilon)
        {
            _alarm.volume = Mathf.MoveTowards(_alarm.volume, neededVolumeValue, Time.deltaTime * _volumeChangingSpeed);

            yield return null;
        }

        if (neededVolumeValue == _minVolume)
        {
            _alarm.Stop();
            _isAlarmPlay = false;
        }
    }
}
