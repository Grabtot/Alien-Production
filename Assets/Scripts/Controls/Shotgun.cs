using DanPie.Framework.AudioManagement;
using DanPie.Framework.Coroutines;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject _shellPrefab;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _ejectPoint;
    [SerializeField] private float _shellSpeed = 2f;
    [SerializeField] private float _maxRange = 100f;
    [SerializeField] private LayerMask _hitLayers;
    [SerializeField] private Material _lineMaterial;
    [SerializeField] private AudioSourcesManager _audioSourcesManager;
    [SerializeField] private AudioClipDataProvider _shootSoundProvider;
    [SerializeField] private AudioClipDataProvider _rechargeSoundProvider;

    private Rigidbody _playerRigidbody;
    private LineRenderer _lineRenderer;
    private bool _gunLoaded = true;

    private void Start()
    {
        _playerRigidbody = GetComponentInParent<Rigidbody>();
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = _lineMaterial;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _gunLoaded)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 direction = _shotPoint.forward.normalized;
        Vector3 impactPoint = _shotPoint.position + direction * _maxRange;

        if (Physics.Raycast(_shotPoint.position, direction, out RaycastHit hit, _maxRange, _hitLayers))
        {
            impactPoint = hit.point;
        }
        DrawLineWithLineRenderer(_shotPoint.position, impactPoint, Color.yellow, .1f);

        PlayShootEffects();
    }

    private void DropShell()
    {
        GameObject shell = Instantiate(_shellPrefab, _ejectPoint.position, _ejectPoint.rotation);
        shell.AddComponent<Rigidbody>();
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        shellRb.velocity = _ejectPoint.TransformDirection(new Vector3(1, Random.Range(0.2f, 1f), 0)) * _shellSpeed;
        StartCoroutine(CoroutineUtilities.WaitForSeconds(1.5f, () => Destroy(shell)));
    }

    private void PlayShootEffects()
    {
        _audioSourcesManager.GetAudioSourceController().Play(_shootSoundProvider.GetClipData());
        _gunLoaded = false;

        StartCoroutine(CoroutineUtilities.WaitForSeconds(0.5f, () =>
        {
            _audioSourcesManager.GetAudioSourceController().Play(_rechargeSoundProvider.GetClipData());
            StartCoroutine(CoroutineUtilities.WaitForSeconds(
                _rechargeSoundProvider.GetClipData().AudioClip.length,
                () => _gunLoaded = true));

            DropShell();
        }));
    }

    private void DrawLineWithLineRenderer(Vector3 startPosition, Vector3 endPosition, Color color, float duration)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.material.color = color;
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);

        StartCoroutine(CoroutineUtilities.WaitForSeconds(duration, () => _lineRenderer.enabled = false));
    }
}
