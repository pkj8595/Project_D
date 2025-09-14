using Cysharp.Threading.Tasks;
using Data.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;



namespace Core
{
    public class DataManager 
    {
        public IReadOnlyDictionary<int, Card> Card => _card;
        private readonly Dictionary<int, Card> _card = new ();


        public async UniTask Init()
        {
            Debug.Log("DataManager Init");
            var textAsset = await Addressables.LoadAssetAsync<TextAsset>("Assets/_Resource/Data/tableData.json");
            var tableData = Newtonsoft.Json.JsonConvert.DeserializeObject<TableData>(textAsset.text);
            LoadData(tableData);
        }

        private void LoadData(TableData tableData)
        {
            tableData.Card.ForEach(x => _card.Add(x.CardId, x));

            Debug.Log($"Card Count : {_card.Count}");
        }

    }
}
