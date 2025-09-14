using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public Core.ResourceManager Resource { get; } = new ();

    void Awake()
    {
        TaskInit(destroyCancellationToken).Forget(Debug.LogError);
    }

    async UniTask TaskInit(CancellationToken ct)
    {
        string label = "Sprite";
        await Resource.LoadAllAsync_PreLoad_Sprite(label, null);
    }

    
    void Update()
    {
        
    }
}
