using UnityEngine;

public class PlayLogger : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}