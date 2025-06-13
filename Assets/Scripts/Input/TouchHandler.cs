using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TouchHandler : MonoBehaviour
{
    private Camera _camera;
    private PlayerInputActions _inputActions;

    [Inject]
    private void Construct(Camera camera)
    {
        _camera = camera;
    }

    private void Awake()
    {
        if( _camera == null )
        {
            _camera = Camera.main;
        }

        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Gameplay.Enable();
        _inputActions.Gameplay.Touch.performed += OnTouchPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Touch.performed -= OnTouchPerformed;
        _inputActions.Gameplay.Disable();
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        HandleTouch(screenPosition);
    }

    private void HandleTouch(Vector2 screenPosition)
    {
        Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        int layerMask = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                tile.OnTouch();
            }
        }
    }
}
