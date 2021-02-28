using BumperCarGamePrototype.Abstracts.Cameras;
using BumperCarGamePrototype.Abstracts.Combats;
using BumperCarGamePrototype.Concretes.Combats;
using BumperCarGamePrototype.Entites.Controllers;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace BumperCarGamePrototype.Concretes.Controllers
{
    public class CameraController : MonoBehaviour, ICameraShakeService
    {

        [Header("------CameraShake------")]
        [SerializeField] private float _slowCameraShake = default;
        [SerializeField] private float _startDuration = default;
        [SerializeField] private float _smooth = default;
        [SerializeField] private float _smoothTime = default;

        [Header("------Others------")]
        [SerializeField] private Transform _firstCameraTransform;
        [SerializeField] private GameObject _player;

        private IEntityController _entityController;
        private IHitService _hit;

        private Vector3 _currentVelocity = default;
        private float _duration = default;
        private bool _isCameraShake = default;

        public bool IsCameraShake { get => _isCameraShake; set => _isCameraShake = value; }

        private void Awake()
        {
            _hit = _player.GetComponent<HitCombat>();
        }
        private void Start()
        {
            _entityController = FindObjectOfType<CarController>();
        }
        private void Update()
        {
            if (_entityController == null) return;

            if (_hit.IsHit)
            {
                Debug.Log("CAM" + _hit.IsHit);
                StartCoroutine(HitCamera());
                ShakeCamera();
            }
        }
        public void CheckFieldOfView(bool isMoving)
        {
            if (isMoving)
            {
                this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(60, 70, 4f);
            }
            else
            {
                this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(70, 60, 4f);
            }
        }
        public IEnumerator HitCamera()
        {
            this.transform.localPosition += new Vector3(0, 0, 0.5f * Time.deltaTime);
            yield return new WaitForSeconds(0.5f);
            this.transform.localPosition = Vector3.Lerp(transform.localPosition, _firstCameraTransform.localPosition, _smoothTime);
        }
        public void ShakeCamera()
        {
            IsCameraShake = true;
            if (IsCameraShake)
            {
                if (_duration > 0)
                {
                    transform.localPosition += Random.insideUnitSphere * _smooth;
                    _duration -= Time.deltaTime * _slowCameraShake;
                }
                else
                {
                    IsCameraShake = false;
                    _duration = _startDuration;
                  //  transform.localPosition = Vector3.Lerp(transform.localPosition, _firstCameraTransform.localPosition, 0.01f);
                }
            }
            if (!_isCameraShake) transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _firstCameraTransform.localPosition, ref _currentVelocity, _smoothTime * Time.deltaTime);
        }
    }
}