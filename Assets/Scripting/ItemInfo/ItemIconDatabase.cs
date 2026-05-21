using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Icon Database")]
public class ItemIconDatabase : ScriptableObject
{
    [System.Serializable]
    public class IconData
    {
        public string itemId;
        public Sprite icon;
    }

    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private IconData[] icons;

    public Sprite GetIcon(string itemId)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (icons[i].itemId == itemId)
                return icons[i].icon;
        }

        return defaultIcon;
    }
}