using UnityEngine;

[CreateAssetMenu(fileName = "New Sellable Object", menuName = "Inventory/Items/Sellable")]
public class Sellabe : ItemObject {
  public int sellPrice;
  private void Awake() {
    type = ItemType.Sellable;
  }

} 