using System;
using UnityEngine;

public class ObjectNPC : MonoBehaviour
{
    protected Transform player;
    protected UI ui;

    [SerializeField] private Transform _npc;
    [SerializeField] private GameObject _interactTooltip;

    private bool _facingRight = true;

    [Header("Floaty tooltip details")]
    [SerializeField] private float _floatingSpeed = 8f;
    [SerializeField] private float _floatingRange = 0.1f;
    private Vector3 _startingPosition;

    protected virtual void Awake() {
        ui = FindFirstObjectByType<UI>();
    }

    protected virtual void Start() {
        _startingPosition = _interactTooltip.transform.position;
        _interactTooltip.SetActive(false);
    }

    protected virtual void Update() {
        HandleNPCFlip();
        HandleTooltipFloat();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        player = collision.transform;
        _interactTooltip.SetActive(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision) {
        player = null; // May have to remove later

        _interactTooltip.SetActive(false);
    }

    private void HandleTooltipFloat() {
        if (_interactTooltip.activeSelf) {
        float yOffset = Mathf.Sin(_floatingSpeed * Time.time) * _floatingRange;
        _interactTooltip.transform.position = _startingPosition + new Vector3(0f, yOffset);
        }
    }

    private void HandleNPCFlip() {
        if(player == null || _npc == null)
            return;
        if(_npc.position.x > player.position.x && _facingRight) {
            _npc.transform.Rotate(0f, 180f, 0f);
            _facingRight = false;
        }
        else if(_npc.position.x < player.position.x && !_facingRight) {
            _npc.transform.Rotate(0f, 180f, 0f);
            _facingRight = true;
        }

    }
}
