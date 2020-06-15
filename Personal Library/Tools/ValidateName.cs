using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ValidateName : MonoBehaviour
{
    [SerializeField] private string[] badWords;
    [SerializeField] private int minLength;
    [SerializeField] private int maxLength;
    
    [SerializeField] private GameObject badWordWarning;
    [SerializeField] private GameObject tooShortWarning;
    [SerializeField] private GameObject tooLongWarning;

    public void ValidateInputName(string username)
    {
        Debug.Log("started validation on: "+ username);
        badWordWarning.SetActive(false);
        tooShortWarning.SetActive(false);
        if (username.IsNullOrWhitespace() || username.Length < minLength)
        {
            tooShortWarning.SetActive(true);
            return;
        }
        if (username.Length > maxLength)
        {
            tooLongWarning.SetActive(true);
            return;
        }
        if (badWords.Any(username.Contains))
        {
            badWordWarning.SetActive(true);
            return;
        }
    }
}
