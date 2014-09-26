using UnityEngine;

namespace Assets.Scripts.Managers
{

    public class GameManager : MonoBehaviour 
    {
        public static GameManager Instance;

        [Range(0, 10)]
        public int Difficulity;

        void Awake()
        {
            Instance = FindObjectOfType<GameManager>();
            DontDestroyOnLoad(Instance);
        }
    }
}
