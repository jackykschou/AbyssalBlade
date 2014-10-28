using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Camera
{
    public class CameraShake : GameLogic
    {
        private float _shakeIntensity;
        private const float ShakeDecay = .04f;

        public GameObject MainCamera;

        private float _shakeIntensityLeft;
        private Transform _cameraTransform;
        private bool _shaking;
        private Vector3 _originalPos;
        private Quaternion _originalRot;


        protected override void Initialize()
        {
            _shaking = false;
            MainCamera = GameManager.Instance.MainCamera;
            _cameraTransform = MainCamera.transform;
        }

        protected override void Update()
        {
            if (_shakeIntensityLeft > 0)
            {
                _cameraTransform.position = _originalPos + Random.insideUnitSphere * _shakeIntensityLeft;
                _cameraTransform.rotation = new Quaternion(
                    _originalRot.x/* + Random.Range(-_shakeIntensityLeft, _shakeIntensityLeft) * .2f*/,
                    _originalRot.y + Random.Range(-_shakeIntensityLeft, _shakeIntensityLeft) * .2f,
                    _originalRot.z/* + Random.Range(-_shakeIntensityLeft, _shakeIntensityLeft) * .2f*/,
                    _originalRot.w/* + Random.Range(-_shakeIntensityLeft, _shakeIntensityLeft) * .2f*/);
                _shakeIntensityLeft -= ShakeDecay;
            }
            else if (_shaking)
            {
                _cameraTransform.rotation = _originalRot;
                _shaking = false;
            }
        }

        [GameEvent(Constants.GameEvent.ShakeCamera)]
        public void Shake(float shakeIntensity)
        {
            if (_shaking)
            {
                return;
            }
            _shakeIntensity = shakeIntensity;
            _originalPos = _cameraTransform.position;
            _originalRot = _cameraTransform.rotation;
            _shakeIntensityLeft = _shakeIntensity;
            _shaking = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}