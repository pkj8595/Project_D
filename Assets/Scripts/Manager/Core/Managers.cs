using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Managers : GlobalSingleton<Managers>
{
    public static Core.ResourceManager Resource { get; } = new ();
    public static Core.DataManager Data { get; } = new ();

    // MonoBehaviour
    public static Pooling Pool { get { return Pooling.Instance; } }
    public static MapManager Map { get { return MapManager.Instance; } }
    public static InputManager Input { get { return InputManager.Instance; } }
    public static TimeManager Time { get { return TimeManager.Instance; } }

    void Awake()
    {
        TaskInit(destroyCancellationToken).Forget(Debug.LogError);
    }

    async UniTask TaskInit(CancellationToken ct)
    {
        string label = "Sprite";
        await Resource.LoadAllAsync_PreLoad_Sprite(label, null);

        label = "Prefab";
        await Resource.LoadAsync_UIPrefab(label, null);

        await Data.Init();
        Pool.Init();

        Input.Init();
        await Map.Init();
    }

    
    // void Update()
    // {
        
    // }
}
