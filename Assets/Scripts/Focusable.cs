using UnityEngine;

public class Focusable : MonoBehaviour {
  [SerializeField] private float moveThreshold;
  [SerializeField] private float cameraOffset;

  private Vector3 downPosition;

  private Transform _transform;


  private void Awake() {
    _transform = transform;
  }

  private void OnMouseDown() {
    downPosition = Input.mousePosition;
  }

  private void OnMouseUp() {
    Vector3 diff = Input.mousePosition - downPosition;
    if (diff.magnitude < moveThreshold) {
      GameEvents.current.CameraFocus(_transform.position, cameraOffset);
    }
  }
}
