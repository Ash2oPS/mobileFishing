using UnityEngine;

namespace EtienneSibeaux.Debugger
{
    public static class C_DebugLogExtention
    {
        public static void ErrorMessage(this string errorMessage)
        {
#if UNITY_EDITOR
            ($" ERROR - {errorMessage}").DLE(Color.yellow);
#endif //UNITY_EDITOR
        }

        public static void ErrorMessage(this object obj, string errorMessage)
        {
#if UNITY_EDITOR
            ($" ERROR - {obj.GetType()} - {errorMessage}").DLE(Color.yellow);
#endif //UNITY_EDITOR
        }

        public static void ErrorMessage(this object obj, string methodName, string errorMessage)
        {
#if UNITY_EDITOR
            ($" ERROR - {obj.GetType()} - {methodName} - {errorMessage}").DLE(Color.yellow);
#endif //UNITY_EDITOR
        }

        public static void ErrorMessage(this MonoBehaviour obj, string methodName, string errorMessage)
        {
#if UNITY_EDITOR
            ($" ERROR - {obj.GetType()} - {obj.name} - {methodName} - {errorMessage}").DLE(Color.yellow);
#endif //UNITY_EDITOR
        }

        public static void DL(this object o, Color color)
        {
#if UNITY_EDITOR
            Debug.Log(UpgradeLog(o, color));
#endif //UNITY_EDITOR
        }

        public static void DL(this object o)
        {
#if UNITY_EDITOR
            o.DL(Color.white);
#endif //UNITY_EDITOR
        }

        public static void DLW(this object o, Color color)
        {
#if UNITY_EDITOR
            Debug.LogWarning(UpgradeLog(o, color));
#endif //UNITY_EDITOR
        }

        public static void DLW(this object o)
        {
#if UNITY_EDITOR
            o.DLW(Color.white);
#endif //UNITY_EDITOR
        }

        public static void DLE(this object o, Color color)
        {
            Debug.LogError(UpgradeLog(o, color));
        }

        public static void DLE(this object o)
        {
            o.DLE(Color.white);
        }

        private static string UpgradeLog(object o, Color color)
        {
            string baseString = "";
            string output;
            string hex = ColorUtility.ToHtmlStringRGB(color);

            if (o.ToString() != "")
                baseString = o.ToString() + " ";
            output = $"<color=#{hex}>{baseString}</color>";

            return output;
        }
    }
}