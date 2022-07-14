using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeTurret : MonoBehaviour
{
    public Transform _target;
    private Vector3 _standardVector;
    public GameObject bulletPrefab;

    float _deltatime = 0.0f;
    float _coolTime = 1f;

    private bool _isTargetWithinRange = false;


    private void Start()
    {
        _standardVector = transform.right * -1;
    }

    private void Update()
    {
        if (_isTargetWithinRange)
        {
            if (isFindTarget())
                Shot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTargetWithinRange = true;
        }
    }

    private bool isFindTarget()
    {
        Vector3 distanceVector = _target.transform.position - transform.position;
        Vector3 crossVector = Vector3.Cross(_standardVector, distanceVector.normalized);
        float dotProduct = Vector3.Dot(_standardVector, distanceVector.normalized);

        if (Mathf.Rad2Deg * (Math.Acos(dotProduct / (distanceVector.normalized.magnitude * _standardVector.magnitude))) < 60 && crossVector.y > 0)
            return true;
        else
            return false;
    }

    void Shot()
    {
        _deltatime += Time.deltaTime;
        gameObject.transform.LookAt(_target);

        if (_deltatime >= _coolTime)
        {
            _deltatime = 0.0f;

            Vector3 spawnPosition = gameObject.transform.position;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.LookAt(_target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isTargetWithinRange = false;
    }

}
