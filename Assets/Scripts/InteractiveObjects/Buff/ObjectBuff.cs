using System;
using System.Collections;
using UnityEngine;


public class ObjectBuff : MonoBehaviour
{
    [Header("Floaty movement details")]
    [SerializeField] private float _floatingSpeed = 1f;
    [SerializeField] private float _floatingRange = 0.1f;
    private Vector3 _startingPosition;

    [Header("Buff details")]
    [SerializeField] private BuffEffectData[] _buffs;
    [SerializeField] private float _buffDuration = 4f;
    [SerializeField] private string _buffName;


    private PlayerStats _playerStats;

    private void Awake() {

        _startingPosition = transform.position;
    }

    private void Update() {

        
        float yOffset = Mathf.Sin(_floatingSpeed * Time.time) * _floatingRange;
        transform.position = _startingPosition + new Vector3(0, yOffset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        _playerStats = collision.GetComponent<PlayerStats>();

        if (_playerStats.CanApplyBuffs(_buffName)) {
            _playerStats.ApplyBuffs(_buffs, _buffDuration, _buffName);
            Destroy(this.gameObject);
        }

    }
}
