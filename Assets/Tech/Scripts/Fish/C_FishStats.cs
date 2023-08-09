using UnityEngine;

namespace EtienneSibeaux.Fish
{
    public class C_FishStats : MonoBehaviour
    {
        [Header("---Data---")]
        [SerializeField] private SO_FishAsset _fishAsset;

        [Header("---References---")]
        [SerializeField] private C_Fish _fish;

        public SO_FishAsset FishAsset { get => _fishAsset; }

        public void SetAsset(SO_FishAsset asset)
        {
            _fishAsset = asset;
            transform.localScale = new Vector3(_fishAsset.FishSize, transform.localScale.y, _fishAsset.FishSize);
            _fish.FishMovement.SetSpeed(asset.FishSpeed);
        }
    }
}