using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform _target;
    public GameObject _bulletPrefab;
    bool _isFire = false;
    float _speed = 3.0f;
    float _deltatime = 0.0f;
    private void Update()
    {
        if (!_isFire)
            gameObject.transform.Rotate(0.0f, _speed, 0.0f);
        else
            Shot();
    }

    private void OnTriggerEnter(Collider other)
    {
        _isFire = true;

    }
    void Shot()
    {
        _deltatime += Time.deltaTime;
        gameObject.transform.LookAt(_target);

        if (_deltatime >= 1.0f)
        {
            _deltatime = 0.0f;

            Vector3 spawnPosition = gameObject.transform.position;
            GameObject bullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.LookAt(_target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isFire = false;
        _deltatime = 0.0f;
    }
}
