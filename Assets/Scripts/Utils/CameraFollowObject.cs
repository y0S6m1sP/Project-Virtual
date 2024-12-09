using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = .5f;

    private Coroutine _turnCoroutine;
    private Player _player;
    private bool _IsFacingRight;


    void Start()
    {
        _player = _playerTransform.GetComponent<Player>();
        _IsFacingRight = _player.FacingDir == 1;
    }

    void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _IsFacingRight = !_IsFacingRight;

        if (_IsFacingRight) return 0f;
        else return 180f;
    }
}
