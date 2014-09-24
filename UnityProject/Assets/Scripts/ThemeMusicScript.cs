using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts
{
    public class ThemeMusicScript : MonoBehaviour
    {
        void Awake ()
        {
            Managers.AudioManager.Instance.playLoop(LoopName.MainLoop);
        }
	
    }
}
