using DanPie.Framework.AudioManagement;
using DanPie.Framework.Coroutines;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject _shellPrefab;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _ejectPoint;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _shellSpeed = 2f;
    [SerializeField] private int _pelletCount = 8;
    [SerializeField] private float _recoilForce = 10f;
    [SerializeField] private float _maxRange = 1000f;
    [SerializeField] private LayerMask _hitLayers;
    [SerializeField] private AudioSourcesManager _audioSourcesManager;
    [SerializeField] private AudioClipDataProvider _shootSoundProvider;
    [SerializeField] private AudioClipDataProvider _rechargeSoundProvider;
    private Rigidbody _playerRigidbody; // Если ваш персонаж имеет Rigidbody

    void Start()
    {
        _playerRigidbody = GetComponentInParent<Rigidbody>(); // Если ваш персонаж имеет Rigidbody
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < _pelletCount; i++)
        {
            Vector3 randomSpread = new(Random.Range(-1f, 100f), Random.Range(-1f, 1f), 0);
            Vector3 direction = (_shotPoint.forward + randomSpread).normalized;


            if (Physics.Raycast(_shotPoint.position, direction, out RaycastHit hit, _maxRange, _hitLayers))
            {
                // Обработка попадания (например, нанесение урона)
                Debug.DrawLine(_shotPoint.position, hit.point, Color.yellow, 1f);
            }
            else
            {
                Debug.DrawLine(_shotPoint.position, _shotPoint.position + direction * _maxRange, Color.yellow, 1f);
            }

        }

        // Воспроизведение звука выстрела
        _audioSourcesManager.GetAudioSourceController().Play(_shootSoundProvider.GetClipData());
        StartCoroutine(CoroutineUtilities.WaitForSeconds(0.5f, () =>
            _audioSourcesManager.GetAudioSourceController().Play(_rechargeSoundProvider.GetClipData())));


        // Отдача
        if (_playerRigidbody != null)
        {
            _playerRigidbody.AddForce(-_shotPoint.forward * _recoilForce, ForceMode.Impulse);
        }

        // Выброс гильзы
        GameObject shell = Instantiate(_shellPrefab, _ejectPoint.position, _ejectPoint.rotation);
        shell.AddComponent<Rigidbody>();
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        shellRb.velocity = _ejectPoint.TransformDirection(new Vector3(1, Random.Range(0.2f, 1f), 0)) * _shellSpeed;
        //  CoroutineUtilities.WaitForSeconds(1f, () => shell.);
    }
}
