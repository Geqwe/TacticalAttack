using UnityEngine;

public class PumpLooper : MonoBehaviour
{
    [SerializeField] private float scaleToPump = 1.3f;
    [SerializeField] private float time = 0.8f;

    private void Awake()
    {
        Vector3 scalePump = transform.localScale * scaleToPump;
        LeanTween.scale(gameObject, scalePump, time).setEaseInOutSine().setLoopPingPong();
    }
}
