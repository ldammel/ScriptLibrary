using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance
    {
        get
        {
            if (_instance!= null) return _instance;
            _instance = FindObjectOfType<NotificationManager>();
            return _instance!= null ? _instance : CreateNewInstance();
        }
    }
    private static NotificationManager _instance;
    private static NotificationManager CreateNewInstance()
    {
        NotificationManager prefab = Resources.Load<NotificationManager>("Prefabs/Managers/NotificationManager");
        _instance = Instantiate(prefab);
        return _instance;
    }
    
    
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private float fadeTime;
    private IEnumerator _notificationCoroutine;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetNewNotification(string message)
    {
        if (_notificationCoroutine != null) StopCoroutine(_notificationCoroutine);
        _notificationCoroutine = FadeOutNotification(message);
        StartCoroutine(_notificationCoroutine);
    }

    private IEnumerator FadeOutNotification(string message )
    {
        displayText.text = message;
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            displayText.color = new Color(displayText.color.r,displayText.color.g,displayText.color.b,Mathf.Lerp(1f,0f,t/fadeTime));
            yield return null;
        }
    }
}
