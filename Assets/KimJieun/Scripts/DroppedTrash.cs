using UnityEngine;

public class DroppedTrash : MonoBehaviour
{
    public Item TrashData { get; private set; }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Item trashData)
    {
        TrashData = trashData;
        
        if (trashData is TrashItem trash)
        {
            spriteRenderer.sprite = trash.Icon; 
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
