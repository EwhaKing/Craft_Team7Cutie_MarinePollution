using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;

    private void Start()
    {
        SpawnItem("102", new Vector3(2, 0, 0));
    }

    private void SpawnItem(string itemId, Vector3 position)
    {
        GameObject obj = Instantiate(
            droppedItemPrefab,
            position,
            Quaternion.identity
        );

        obj.layer = LayerMask.NameToLayer("Trash");
        DroppedItem droppedItem = obj.GetComponent<DroppedItem>();

        droppedItem.Init(itemId);
    }
}