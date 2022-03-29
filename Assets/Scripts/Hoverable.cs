using UnityEngine;

public class Hoverable : MonoBehaviour {

  [SerializeField] private float transitionTime;

  private Color startColor;
  private Color currentColor;

  private Renderer _renderer;

  private void Awake() {
    _renderer = GetComponent<Renderer>();
    startColor = _renderer.material.color;
    currentColor = startColor;
  }

  private void OnMouseEnter() {
    currentColor = Color.yellow;
  }

  private void OnMouseExit() {
    currentColor = startColor;
  }

  private void Update() {
    _renderer.material.color = Color.Lerp(_renderer.material.color, currentColor, Time.deltaTime * transitionTime);
  }

}