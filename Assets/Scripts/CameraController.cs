using UnityEngine;

public class CameraController : MonoBehaviour {

  [SerializeField] private float normalSpeed;
  [SerializeField] private float fastSpeed;
  [SerializeField] private float movementTime;
  [SerializeField] private float minZoom;
  [SerializeField] private float maxZoom;
  [SerializeField] private float rotationAmount;
  [SerializeField] private Vector3 zoomAmount;

  private Vector3 newPosition;//Hello
  private Quaternion newRotation;
  private Vector3 newZoom;

  private Vector3 dragStartPosition;
  private Vector3 dragCurrentPosition;
  private Vector3 rotateStartPosition;
  private Vector3 rotateCurrentPosition;

  private Transform _transform;
  private Camera _camera;
  private Transform cameraTransform;

  private void Awake() {
    _transform = transform;
    _camera = Camera.main;
    cameraTransform = _camera.transform;
  }

  private void Start() {
    newPosition = _transform.position;
    newRotation = _transform.rotation;
    newZoom = cameraTransform.localPosition;
  }

  private void Update() {
    HandleMovementInput();
    HandleMouseInput();
  }

  private void HandleMovementInput() {
    float movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
      newPosition += _transform.forward * movementSpeed;
    }
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
      newPosition += _transform.right * -movementSpeed;
    }
    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
      newPosition += _transform.forward * -movementSpeed;
    }
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
      newPosition += _transform.right * movementSpeed;
    }
    if (Input.GetKey(KeyCode.Q)) {
      newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
    }
    if (Input.GetKey(KeyCode.E)) {
      newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
    }

    newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
    newZoom.z = Mathf.Clamp(newZoom.z, -maxZoom, -minZoom);
    Quaternion lerpRotation = Quaternion.Lerp(_transform.rotation, newRotation, Time.deltaTime * movementTime);
    Vector3 lerpPosition = Vector3.Lerp(_transform.position, newPosition, Time.deltaTime * movementTime);

    _transform.SetPositionAndRotation(lerpPosition, lerpRotation);
    cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
  }

  private void HandleMouseInput() {
    if (Input.mouseScrollDelta.y != 0) {
      newZoom += Input.mouseScrollDelta.y * zoomAmount;
    }

    if (Input.GetMouseButtonDown(0)) {
      SetYPlaneIntersection(ref dragStartPosition);
    }
    if (Input.GetMouseButton(0)) {
      SetYPlaneIntersection(ref dragCurrentPosition);
      newPosition = _transform.position + dragStartPosition - dragCurrentPosition;
    }
    
    if (Input.GetMouseButtonDown(2)) {
      rotateStartPosition = Input.mousePosition;
    }
    if (Input.GetMouseButton(2)) {
      rotateCurrentPosition = Input.mousePosition;
      Vector3 difference = rotateStartPosition - rotateCurrentPosition;
      rotateStartPosition = rotateCurrentPosition;
      newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
    }
  }

  private void SetYPlaneIntersection(ref Vector3 position){
    Plane plane = new Plane(Vector3.up, Vector3.zero);
    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

    if (plane.Raycast(ray, out float entry)) {
      position = ray.GetPoint(entry);
    }
  }
}