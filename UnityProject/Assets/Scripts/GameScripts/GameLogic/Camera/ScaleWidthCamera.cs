using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class ScaleWidthCamera : GameLogic 
    {
        [Range(0, float.MaxValue)]
        public float TargetWidth = 1250;

        protected override void Update()
        {
            base.Update();
            float height = TargetWidth / Screen.width * Screen.height;
            camera.orthographicSize = (int)(height / Constants.WorldScaleConstant.PixelToUnit / 2f);
        }

        protected override void Deinitialize()
        {
        }
    }
}
