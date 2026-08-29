using UnityEngine;
namespace babbarversecheats
{
    public class CheatSettings : MonoBehaviour
    {
        public static CheatSettings Instance;
        private void Awake()
        {
            if(Instance!=null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static bool CompleteObjective { get => Instance.completeObjective;  set => Instance.completeObjective = value; }
        [SerializeField] bool completeObjective = false;
    }
}
