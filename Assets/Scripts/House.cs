using UnityEngine;

public class House : MonoBehaviour
{
    private Signaling _alarmSystem;

    private void Start()
    {
        _alarmSystem = GetComponentInChildren<Signaling>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _alarmSystem.TurnOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _alarmSystem.TurnOff();
        }
    }
}