using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool autoCycle = true;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleAutoCycle()
    {
        autoCycle = !autoCycle;
    }
}
