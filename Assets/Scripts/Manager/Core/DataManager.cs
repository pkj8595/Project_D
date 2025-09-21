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
        private readonly Dictionary<int, Card> _card = new();


        #region SO

        public IReadOnlyDictionary<string, InGameData> InGame => _inGame;
        private readonly Dictionary<string, InGameData> _inGame = new();


        public IReadOnlyDictionary<string, BuildingData> Building => _building;
        private readonly Dictionary<string, BuildingData> _building = new();


        public IReadOnlyDictionary<string, CardData> SOCard => _soCard;
        private readonly Dictionary<string, CardData> _soCard = new();


        public IReadOnlyDictionary<string, CardPoolData> CardPool => _cardPool;
        private readonly Dictionary<string, CardPoolData> _cardPool = new();


        public IReadOnlyDictionary<string, EnemyData> Enemy => _enemy;
        private readonly Dictionary<string, EnemyData> _enemy = new();

        public IReadOnlyDictionary<string, UpgradeData> Upgrade => _upgrade;
        private readonly Dictionary<string, UpgradeData> _upgrade = new();

        public IReadOnlyDictionary<string, WaveEventData> WaveEventData => _waveEventData;
        private readonly Dictionary<string, WaveEventData> _waveEventData = new();


        #endregion


        public async UniTask Init()
        {
            Debug.Log("DataManager Init");

            await LoadData();
            await LoadSOData();
        }

        private async UniTask LoadData()
        {
            var textAsset = await Addressables.LoadAssetAsync<TextAsset>("Assets/_Resource/Data/tableData.json");
            var tableData = Newtonsoft.Json.JsonConvert.DeserializeObject<TableData>(textAsset.text);
            tableData.Card.ForEach(x => _card.Add(x.CardId, x));

            Debug.Log($"Card Count : {_card.Count}");
        }

        public async UniTask LoadSOData()
        {
            var handle = await Addressables.LoadAssetsAsync<SOBase>("SO", null);
            foreach (var data in handle)
            {
                switch (data)
                {
                    case BuildingData buildingData:
                        _building.Add(buildingData.name, buildingData);
                        break;
                    case InGameData gameData:
                        _inGame.Add(gameData.name, gameData);
                        break;
                    case CardData cardData:
                        _soCard.Add(cardData.name, cardData);
                        break;
                    case CardPoolData cardPoolData:
                        _cardPool.Add(cardPoolData.name, cardPoolData);
                        break;
                    case EnemyData enemyData:
                        _enemy.Add(enemyData.name, enemyData);
                        break;
                    case UpgradeData upgradeData:
                        _upgrade.Add(upgradeData.name, upgradeData);
                        break;
                    case WaveEventData waveEventData:
                        _waveEventData.Add(waveEventData.name, waveEventData);
                        break;
                }
            }

            Debug.Log($"InGame Data Count: {_inGame.Count}");
            Debug.Log("SO Table Loaded!");
        }



    }
}
