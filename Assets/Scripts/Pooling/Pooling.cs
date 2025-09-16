using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

// 오브젝트 풀링 관리하는 클래스
public class Pooling : LocalSingleton<Pooling>
{
    /// <summary>
    /// 풀링 오브젝트 컨테이너
    /// Key: 풀링 오브젝트 이름 (Addressable 키), Value: 풀링 오브젝트 컨테이너
    /// </summary>
    private Dictionary<string, PoolContainer> poolContainers = new();
    private Dictionary<string, GameObject> poolParents = new();

    public bool IsInit { get; private set; } = false;
    public void Init()
    {
        Clear();
        poolContainers = new();
        poolParents = new();
        IsInit = true;
    }

    /// <summary>
    /// 풀링 오브젝트 가져오기
    /// </summary>
    /// <param name="key"></param>
    public async UniTask<GameObject> Get(string key)
    {
        if (!IsInit)
        {
            Debug.LogError("Pooling is not initialized");
            return null;
        }

        if (!poolContainers.ContainsKey(key))
        {
            // 오브젝트 풀 부모 계층 오브젝트 생성
            poolParents.Add(key, new GameObject($"@{key}"));
            poolParents[key].transform.SetParent(transform);

            // 오브젝트 풀 생성
            poolContainers.Add(key, new PoolContainer(key, poolParents[key].transform));
        }

        var go = await poolContainers[key].Get();
        return go;
    }

    public void Push(GameObject go)
    {
        if (!IsInit)
        {
            Debug.LogError("Pooling is not initialized");
            return;
        }

        var parent = go.transform.parent;
        poolContainers[parent.name.Replace("@", "")].Push(go);
    }

    public void Clear()
    {
        foreach (var parent in poolParents)
        {
            Object.Destroy(parent.Value);
        }
        poolContainers.Clear();
        poolParents.Clear();
        IsInit = false;
    }

    /// <summary>
    /// 각 오브젝트 클래스에 따른 PoolObject 보관하는 클래스
    /// Ex) 사운드 오브젝트 보관 컨테이너, 캐릭터 오브젝트 보관 컨테이너 등 각각 다른 클래스로 분리해서 관리
    /// </summary>    
    internal class PoolContainer
    {
        private string key;
        private Transform parent;

        // 현재 활성화되고 있는 오브젝트
        private Dictionary<int, GameObject> activeObjects = new();
        // 현재 비활성화된 오브젝트
        private Queue<GameObject> inactiveObjects = new();        

        public PoolContainer(string key, Transform parent)
        {
            this.key = key;
            this.parent = parent;
        }

        /// <summary>
        /// 풀링 오브젝트 가져오기
        /// </summary>
        /// <returns></returns>
        public async UniTask<GameObject> Get()
        {
            // 현재 비활성화된 오브젝트에 하나라도 있으면 반환
            GameObject go = inactiveObjects.Count > 0 ? inactiveObjects.Dequeue() : await Managers.Resource.InstantiateAsync<GameObject>(key, parent);
            activeObjects.Add(go.GetInstanceID(), go);
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// 풀링 오브젝트 반환하기
        /// </summary>
        /// <param name="go"></param>
        public void Push(GameObject go)
        {
            int instanceID = go.GetInstanceID();
            if (go.activeSelf == false)
            {
                Debug.LogWarning($"Push: {instanceID} is not active");
                return;
            }

            activeObjects.Remove(instanceID);
            inactiveObjects.Enqueue(go);
            go.SetActive(false);
        }
    }
}

