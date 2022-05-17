using UnityEngine;
using TMPro;

public class InGameNotificationManager : MonoBehaviour
{
    public static InGameNotificationManager Instance { get; private set; }
    [SerializeField] private TMP_Text _notificationText;

    private void Awake() {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    public void UpdateText(string text) {
        _notificationText.text = text;
    }

}
