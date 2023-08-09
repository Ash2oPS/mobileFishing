using EtienneSibeaux.Debugger;
using EtienneSibeaux.Fish;
using EtienneSibeaux.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EtienneSibeaux.Manager
{
    public class C_M_FishManager : CA_Manager
    {
        private const float MIN_X = -32f;
        private const float MAX_X = 26f;
        private const float MIN_Z = -17.5f;
        private const float MAX_Z = 21.25f;

        [Header("---Parameters---")]
        [SerializeField] private int _maxFishOnMap;

        [SerializeField] private int _numberOfFishSpawnedAtStart = 20;

        [SerializeField] private float _fishSpawnDelay;

        [Header("---References---")]
        [SerializeField] private C_Fish _fishPrefab;

        [Header("---Data---")]
        [Tooltip("From least rare to the rarest.")]
        [SerializeField] private SO_FishAsset[] _allFishAssets;

        private C_UI_NewFishNotification _newFishNotif;

        private int _currentNumberOfFish;
        private int _maxFishIndex;
        private List<SO_FishAsset> _spawnableFish;

        public int MaxFishIndex { get => _maxFishIndex; }

        private void Awake()
        {
            _newFishNotif = C_GameManager.Instance.GetManager<C_M_UIManager>().GetUI<C_UI_NewFishNotification>();
        }

        private void Start()
        {
            EditSpawnableList();

            SpawnFishAtStart();
            StartCoroutine(SpawnFishLoop());
        }

        public void SetMaxFishIndex(int value)
        {

            _maxFishIndex = value;
            EditSpawnableList();

            _newFishNotif.NewFish();

        }

        private void EditSpawnableList()
        {
            SO_FishAsset assetToAdd;
            _spawnableFish = new List<SO_FishAsset>();

            for (int fishIndex = 0; fishIndex <= _maxFishIndex; fishIndex++)
            {
                assetToAdd = _allFishAssets[fishIndex];
                for (int i = 0; i < assetToAdd.Rate; i++)
                {
                    _spawnableFish.Add(assetToAdd);
                }
            }
        }

        private SO_FishAsset GetRandomFish()
        {
            int maxRand = 0;
            int rand;

            for (int i = 0; i <= _maxFishIndex; i++)
            {
                maxRand += _allFishAssets[i].Rate;
            }

            rand = Random.Range(0, maxRand);

            return _spawnableFish[rand];
        }

        public void DecrementFishCount()
        {
            _currentNumberOfFish--;
        }

        public void SetMaxFishOnMap(int value)
        {
            _maxFishOnMap = value;
        }

        private IEnumerator SpawnFishLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_fishSpawnDelay);
                if (_currentNumberOfFish < _maxFishOnMap)
                    SpawnFish(GetRandomFish());
            }
        }

        private void SpawnFishAtStart()
        {
            for (int i = 0; i < _numberOfFishSpawnedAtStart; i++)
            {
                SpawnFish(_allFishAssets[0]);
            }
        }

        private void SpawnFish(SO_FishAsset asset)
        {
            float randX = Random.Range(MIN_X, MAX_X);
            float randZ = Random.Range(MIN_Z, MAX_Z);

            _currentNumberOfFish++;

            C_Fish fish = Instantiate(_fishPrefab, new Vector3(randX, asset.Depth, randZ), Quaternion.identity);
            fish.FishStats.SetAsset(asset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(
                new Vector3((MIN_X + MAX_X) / 2f, 0f, (MIN_Z + MAX_Z) / 2f),
                new Vector3(Mathf.Abs(MIN_X) + Mathf.Abs(MAX_X), 6f, Mathf.Abs(MIN_Z) + Mathf.Abs(MAX_Z)));
        }
    }
}