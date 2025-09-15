using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;
using System.Linq;

namespace Core
{
    public class ResourceManager 
    {
        private Dictionary<string, UnityEngine.Object> _resources = new();
        private Dictionary<string, string> _dicAddress = new Dictionary<string, string>();

        #region Load Resource
        public T Load<T>(string key) where T : Object
        {
            if (_resources.TryGetValue(key, out Object resource))
            {
                return resource as T;
            }
            else
            {
                if (_dicAddress.TryGetValue(key, out string address))
                {
                    //비동기로 수정 예정
                    var handle = Addressables.LoadAssetAsync<T>(address).WaitForCompletion();
                    _resources.Add(key, handle);
                    return handle as T;
                }
                else
                {
                    Debug.LogWarning($"<color=blue>[ResourceManager]</color> Resource key {key} not found");
                    return null;
                }
            }
        }

        public async UniTask<T> LoadAsync<T>(string key) where T : Object
        {
            if (_resources.TryGetValue(key, out Object resource))
            {
                return resource as T;
            }
            else
            {
                if (_dicAddress.TryGetValue(key, out string address))
                {
                    //비동기로 수정 예정
                    var handle = await Addressables.LoadAssetAsync<T>(address);
                    _resources.Add(key, handle);
                    return handle as T;
                }
                else
                {
                    Debug.LogWarning($"<color=blue>[ResourceManager]</color> Resource key {key} not found");
                    return null;
                }
            }
        }


        public async UniTask<T> InstantiateAsync<T>(string key, Transform parent) where T : Object
        {
            if (_dicAddress.TryGetValue(key, out string address))
            {
                //비동기로 수정 예정
                var handle = await Addressables.InstantiateAsync(address, parent);
                return handle as T;
            }
            else
            {
                Debug.LogWarning($"<color=blue>[ResourceManager]</color> Resource key {key} not found");
                return null;
            }

        }

        public bool IsResourceEmpty(string key)
        {
            // 키가 존재하지 않거나, 키가 존재하지만 null인 경우 true를 반환
            if (!_resources.TryGetValue(key, out Object resource) || resource == null)
            {
                return true;
            }

            return false;
        }

        public void Destroy(GameObject go)
        {
            if (go == null)
                return;

            /*if (Managers.Pool.Push(go))
                return;*/

            Object.Destroy(go);
        }
        #endregion

        #region Addressable

        List<string> _labels = new List<string>() { "Preload", "UIPrefab", "Preload_Sprite", "Prefab" };
        public async UniTask CheckAsync_Catalog(Action callback)
        {
            var check = await Addressables.CheckForCatalogUpdates();
            Debug.Log($"CheckAsync_Catalog: {check}");
            if (check.Count > 0)
            {
                callback?.Invoke();
                await Addressables.UpdateCatalogs(check);

                Debug.Log($"다운로드 진입 DownloadAsync: {_labels.Count}");
                foreach (var label in _labels)
                {
                    Debug.Log($"다운로드 DownloadAsync: {label}");
                    await DownloadAsync(label);
                }
            }
            else
            {
                Debug.Log($"다운로드 없음");
            }

        }

        public async UniTask DownloadAsync(string label)
        {
            var handle = Addressables.DownloadDependenciesAsync(label, true);
            await handle;
            Addressables.Release(handle);
        }

        public async UniTask LoadAsync_UIPrefab(string label, Action<string, int, int> callback)
        {
            Debug.Log($"LoadAsync_UIPrefab");
            var result = await Addressables.LoadResourceLocationsAsync(label);
            if (result == null)
            {
                Debug.LogError($"[Addressables] Failed to load locations for label: {label}");
                return;
            }

            int totalCount = result.Count;
            int loadedCount = 0;
            Debug.Log($"LoadAsync_UIPrefab: {result.Count}");
            foreach (var location in result)
            {
                string key = System.IO.Path.GetFileNameWithoutExtension(location.PrimaryKey);
                string address = location.PrimaryKey;
                if (!_dicAddress.ContainsKey(key))
                    _dicAddress.Add(key, address);
                else
                    Debug.LogWarning($"<color=blue>[Addressables]</color> 리소스 이름 중복 : {key}");

                loadedCount++;
                callback?.Invoke(key, loadedCount, totalCount);
            }
        }

        public async UniTask LoadAllAsync_PreLoad<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
        {
            Debug.Log($"LoadAllAsync_PreLoad: {label}");
            var result = await Addressables.LoadResourceLocationsAsync(label);
            if (result == null)
            {
                Debug.LogError($"[Addressables] Failed to load locations for label: {label}");
                return;
            }

            List<IResourceLocation> filteredLocations = result.Where(FilterSpineAssets).ToList();

            if (filteredLocations.Count == 0)
            {
                Debug.LogWarning($"[Addressables] No valid assets to load for label: {label}");
                return;
            }

            Debug.Log($"LoadAllAsync_PreLoad: {filteredLocations.Count}");

            int totalCount = filteredLocations.Count;
            int loadedCount = 0;

            var prevDate = DateTime.Now;
            var opHandle = Addressables.LoadAssetsAsync<T>(filteredLocations, (obj) =>
            {
                loadedCount++;
                Debug.Log($"Loaded: {obj.name} {(DateTime.Now - prevDate).TotalMilliseconds}");
                // prevDate = DateTime.Now;
                callback?.Invoke(obj.name, loadedCount, totalCount);
            });

            await opHandle;

            foreach (var obj in opHandle.Result)
            {
                if (!_resources.ContainsKey(obj.name))
                    _resources.Add(obj.name, obj);
                else
                    Debug.LogWarning($"<color=blue>[Addressables]</color> 리소스 이름 중복 : {obj.name}");
            }
        }

        public async UniTask LoadAllAsync_PreLoad_Sprite(string label, Action<string, int, int> callback)
        {
            Debug.Log($"LoadAllAsync_PreLoad_Sprite: {label}");
            var locationResult = await Addressables.LoadResourceLocationsAsync(label);
            if (locationResult == null || locationResult.Count == 0)
            {
                Debug.LogError($"[Addressables] Failed to load locations for label: {label}");
                return;
            }
            Debug.Log($"LoadAllAsync_PreLoad_Sprite: {locationResult.Count}");

            int totalCount = locationResult.Count;
            int loadedCount = 0;

            var prevDate = DateTime.Now;
            var handle = Addressables.LoadAssetsAsync<Sprite>(label, (obj) =>
            {
                loadedCount++;
                Debug.Log($"Loaded: {obj.name} {(DateTime.Now - prevDate).TotalMilliseconds}");
                callback?.Invoke(obj.name, loadedCount, totalCount);
            });

            await handle;

            foreach (var obj in handle.Result)
            {
                if (!_resources.ContainsKey(obj.name))
                    _resources.Add(obj.name, obj);
                else
                    Debug.LogWarning($"<color=blue>[Addressables]</color> 리소스 이름 중복 : {obj.name}");
            }
        }

        // ---- Catalog & Download Utilities ----
        public async UniTask<long> GetDownloadSizeAsync(string[] labels)
        {
            var sizeHandle = Addressables.GetDownloadSizeAsync(labels);
            await sizeHandle;
            long size = sizeHandle.Result;
            Addressables.Release(sizeHandle);
            return size;
        }

        public async UniTask DownloadDependenciesAsync(CancellationToken ct, string[] labels, Action<float, int, int> onProgress = null)
        {
            int total = labels.Length;
            for (int i = 0; i < total; i++)
            {
                var label = labels[i];
                var handle = Addressables.DownloadDependenciesAsync(label, false);
                try
                {
                    while (!handle.IsDone)
                    {
                        float overall = (i + handle.PercentComplete) / total;
                        onProgress?.Invoke(overall, i + 1, total);
                        await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    }
                    onProgress?.Invoke((i + 1f) / total, i + 1, total);
                }
                finally
                {
                    Addressables.Release(handle);
                }
            }
        }


        // ---- 분리된 단계: (1) 카탈로그 업데이트 + 다운로드 용량 확인 ----
        public async UniTask<long> EnsureCatalogAndGetDownloadSizeAsync(string[] labels, Action onCatalogUpdating = null)
        {
            var updates = await Addressables.CheckForCatalogUpdates();
            if (updates != null && updates.Count > 0)
            {
                onCatalogUpdating?.Invoke();
                await Addressables.UpdateCatalogs(updates);
            }
            long bytes = await GetDownloadSizeAsync(labels);
            return bytes;
        }


        // 리소스 필터
        bool FilterSpineAssets(IResourceLocation location)
        {
            string path = location.InternalId;

            // 경로에 Spines 포함 + 특정 Spine 파일 형식이면 제외
            if (path.Contains("Assets/@Resources/Spines") &&
                    (
                        path.EndsWith("_Atlas.asset") ||
                        path.EndsWith("_Marerial.asset") ||
                        path.EndsWith(".atlas.txt") ||
                        path.EndsWith(".json") ||
                        path.EndsWith(".png") ||
                        path.EndsWith(".mat")
                    )
                )
            {
                return false;
            }
            else if (path.EndsWith(".mat") ||
                     path.EndsWith(".shader")
                     )
            {
                return false;
            }

            return true;
        }


        #endregion

    }

}

