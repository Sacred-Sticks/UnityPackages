using Kickstarter.Inputs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private InputManager inputManager;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;

        inputManager.Initialize(out int numPlayers);
    }
}
