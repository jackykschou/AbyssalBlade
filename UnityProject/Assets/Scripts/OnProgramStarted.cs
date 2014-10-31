using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class OnProgramStarted : MonoBehaviour 
    {
        void Awake()
        {
            GameManager.Instance.ShowLoadingScreen();
            GameManager.Instance.HUD.SetActive(false);
            GameManager.Instance.PlayerMainCharacter.SetActive(false);
        }
    }
}
