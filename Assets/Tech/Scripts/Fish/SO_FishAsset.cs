using UnityEngine;

namespace EtienneSibeaux.Fish
{
    [CreateAssetMenu(fileName = "A_NewFish", menuName = "Asset/Fish", order = 0)]
    public class SO_FishAsset : ScriptableObject
    {
        public int Index;
        public string FishName;
        public int Rate;

        [TextArea]
        public string FishDescription;

        public Sprite FishSprite;
        public int FishPrice;
        public float FishSize;
        public float FishSpeed;
        public float Depth;
        public bool CanAttackNet;
    }
}