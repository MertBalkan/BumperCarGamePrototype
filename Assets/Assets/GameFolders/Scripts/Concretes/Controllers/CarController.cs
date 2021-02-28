using BumperCarGamePrototype.Abstracts.Combats;
using BumperCarGamePrototype.Abstracts.Inputs;
using BumperCarGamePrototype.Abstracts.Movements;
using BumperCarGamePrototype.Concretes.Combats;
using BumperCarGamePrototype.Concretes.Inputs;
using BumperCarGamePrototype.Concretes.Managers;
using BumperCarGamePrototype.Concretes.Movements;
using BumperCarGamePrototype.Entites.Controllers;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Controllers
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(BoxCollider))]

    public class CarController : MonoBehaviour, IEntityController
    {
        [Header("---Movements---")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _rotateSmoothAmount;

        //I did not seperate CarController class to AudioManager class...
        [Header("---Audios---")]
        [SerializeField] private AudioClip _moveClip;
        [SerializeField] private AudioClip[] _crashClips;
        [SerializeField] private AudioClip _coinClip;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _coinAudioSource;

        [Header("---Cameras---")]
        [SerializeField] private CameraController _cameraController;

        [Header("---Wheels---")]
        [SerializeField] private GameObject _wheelProp;

        private IHitService _hit;
        private IMoveService _move;
        private IInputService _input;
        private Vector3 _wheelFirstRot;
        private Quaternion _wheelNormalRot;

        private bool _isMoving = false;
        private bool _isCarRotating = false;
        private float _verAxis = default;
        private float _horAxis = default;
        private float _rotateAmount = default;
        private float _currentSoundTime = default;
        private float _soundPlayTime = 1f;
        private int _score = default;
        private bool _isDisqualificated = false;

        public bool IsDisqualificated => _isDisqualificated;
        public int GasInput { get; set; }
        public float CurrentSoundTime { get => _currentSoundTime; set => _currentSoundTime = value; }

        private void Awake()
        {
            _move = new RigidBodyMove(this, _moveSpeed);
            _input = new AndoridInput();
        }
        private void Start()
        {
            _hit = this.GetComponent<HitCombat>();
            _wheelFirstRot = Vector3.zero;
        }
        private void OnEnable()
        {
            GameManager.Instance.OnScoreChange += HandleOnScoreChanged;
        }
        private void OnDisable()
        {
            GameManager.Instance.OnScoreChange -= HandleOnScoreChanged;
        }
        private void Update()
        {
            _verAxis = GasInput;
            _horAxis = _input.Horizontal;
            _rotateAmount = _rotateSpeed * Time.deltaTime;

            if (_verAxis != 0) _isMoving = true;
            else { _isMoving = false; _cameraController.IsCameraShake = false; }

            CheckDamageSound();
            WheelPropController();


            CurrentSoundTime += Time.deltaTime;
            if (CurrentSoundTime >= _soundPlayTime) CurrentSoundTime = _soundPlayTime;

        }
        // We are using "FixedUpdate()" state for RigidBody
        private void FixedUpdate()
        {
            if (_isMoving)
            {
                _move.MoveAction(_verAxis);
                _cameraController.ShakeCamera();
            }
        }
        private void WheelPropController()
        {
            //There is a diffrence between car and wheel axis. We can see the diffrence in the inspector...
            if (_horAxis > 0 || _horAxis < 0) _isCarRotating = true;

            //Wheel and Car Moves
            if (_horAxis > 0 && _verAxis != 0 && _isCarRotating) RotateWheelAndCar(_rotateAmount * -1, _rotateAmount * -1);
            if (_horAxis < 0 && _verAxis != 0 && _isCarRotating) RotateWheelAndCar(_rotateAmount, _rotateAmount);

            // Only Wheel Moves
            if (_horAxis > 0 && _verAxis == 0 && _isCarRotating) RotateWheelAndCar(_rotateAmount * -1, 0.0f);
            if (_horAxis < 0 && _verAxis == 0 && _isCarRotating) RotateWheelAndCar(_rotateAmount, 0.0f);

            if (_horAxis == 0)
            {
                _isCarRotating = false;
                _wheelFirstRot = new Vector3(35, 0, 0); // 35 -> it's a default value for wheel x axis
                _wheelNormalRot = Quaternion.Euler(_wheelFirstRot);
                _wheelProp.transform.localRotation = Quaternion.Slerp(_wheelProp.transform.localRotation, _wheelNormalRot, _rotateSmoothAmount);
            }
        }
        private void CheckDamageSound()
        {
            int ranClip = Random.Range(0, _crashClips.Length);

            if (_hit.IsHit && CurrentSoundTime == _soundPlayTime)
            {
                _audioSource.PlayOneShot(_crashClips[ranClip]);
                CurrentSoundTime = 0;
            }
        }
        private void RotateWheelAndCar(float rotateWheelAmount, float rotateCarAmount)
        {
            _wheelProp.transform.Rotate(new Vector3(0, 0, rotateWheelAmount));
            transform.Rotate(new Vector3(transform.rotation.x, -1 * rotateCarAmount, transform.rotation.z));
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Coin")
            {
                _coinAudioSource.clip = _coinClip;
                _coinAudioSource.Play();

                Destroy(other.gameObject);
                GameManager.Instance.TakeScore(_score);
            }
            if (other.gameObject.tag.Equals("Corner"))
            {
                _isDisqualificated = true;
            }
        }
        private void HandleOnScoreChanged(int score)
        {
            GameManager.Instance.Score++;
            _score = GameManager.Instance.Score;
        }
        ///ANDORID INPUTS///
        public void GasPressed()
        {
            GasInput = 1;
        }
        public void GasReleased()
        {
            GasInput = 0;
        }
    }
}