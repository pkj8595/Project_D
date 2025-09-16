using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [field: SerializeField] public PlayerController Player { get;}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
