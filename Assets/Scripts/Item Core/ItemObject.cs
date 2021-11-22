using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Item item;

    void Start()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.AddItem(item);
        }
    }
}
