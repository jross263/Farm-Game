using UnityEngine;

public class CameraController : MonoBehaviour {
  public Transform cameraTransform;

  public float normalSpeed;
  public float fastSpeed;
  public float movementSpeed;
  public float movementTime;
  public float rotationAmount;
  public Vector3 zoomAmount;

  public float minZoom;
  public float maxZoom;

  public Vector3 newPosition;
  public Quaternion newRotation;
  public Vector3 newZoom;

  public Vector3 dragStartPosition;
  public Vector3 dragCurrentPosition;
  public Vector3 rotateStartPosition;
  public Vector3 rotateCurrentPosition;

  void Start() {
    newPosition = transform.position;
    newRotation = transform.rotation;
    newZoom = cameraTransform.localPosition;
  }

  void Update() {
    HandleMovementInput();
    HandleMouseInput();
  }

  private void HandleMovementInput() {
    float speed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
    movementSpeed = speed;


    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
      newPosition += transform.forward * movementSpeed;
    }
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
      newPosition += transform.right * -movementSpeed;
    }
    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
      newPosition += transform.forward * -movementSpeed;
    }
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
      newPosition += transform.right * movementSpeed;
    }
    if (Input.GetKey(KeyCode.Q)) {
      newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
    }
    if (Input.GetKey(KeyCode.E)) {
      newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
    }

    newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
    newZoom.z = Mathf.Clamp(newZoom.z, -maxZoom, -minZoom);

    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
  }

  private void HandleMouseInput() {
    if (Input.mouseScrollDelta.y != 0) {
      newZoom += Input.mouseScrollDelta.y * zoomAmount;
    }
    if (Input.GetMouseButtonDown(0)) {
      Plane plane = new Plane(Vector3.up, Vector3.zero);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      float entry;
      if (plane.Raycast(ray, out entry)) {
        dragStartPosition = ray.GetPoint(entry);
      }
    }
    if (Input.GetMouseButton(0)) {
      Plane plane = new Plane(Vector3.up, Vector3.zero);
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      float entry;
      if (plane.Raycast(ray, out entry)) {
        dragCurrentPosition = ray.GetPoint(entry);

        newPosition = transform.position + dragStartPosition - dragCurrentPosition;
      }
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
}