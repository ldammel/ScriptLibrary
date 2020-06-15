using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Keybindings keybindings;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool KeyDown(string key)
    {
        return Input.GetKeyDown(keybindings.CheckKey(key));
    }
}
