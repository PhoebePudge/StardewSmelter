using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    public ItemData ItemData { get { return itemData; } }

    [SerializeField] bool triggered = false;

    void OnTriggerStay(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                GameManager.Instance.AddItem(ItemData);
            }

            triggered = true;
        }
    }
}
