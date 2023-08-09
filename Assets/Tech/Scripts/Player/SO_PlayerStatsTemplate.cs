using UnityEngine;

namespace EtienneSibeaux.Player
{
    [CreateAssetMenu(fileName = "A_NewPlayerTemplate", menuName = "Asset/Player Template", order = 1)]
    public class SO_PlayerStatsTemplate : ScriptableObject
    {
        public int Gold;

        [Range(1, 10)]
        public int NetSizeLevel = 1;

        [Range(1, 10)]
        public int BoatSpeedLevel = 1;

        [Range(1, 10)]
        public int MaxFishLevel = 1;

        [Range(1, 10)]
        public int MaxFishOnMapLevel = 1;
    }
}