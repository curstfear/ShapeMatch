using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TouchHandler : MonoBehaviour
{
    private Camera _camera;

    [Inject]
    private void Construct(Camera camera)
    {
        _camera = camera;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTouch(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            HandleTouch(Input.mousePosition);
        }
    }

    private void HandleTouch(Vector3 screenPosition)
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