using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class DisplayList : MonoBehaviour
{
    [SerializeField]
    private DisplayItem[] displayItemsPrefabs;

    private ScrollRect scrollRect;

    public List<DisplayItem> DisplayItems { get; private set; } = new List<DisplayItem>();

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public T DisplayItem<T>() where T : DisplayItem
    {
        DisplayItem itemToInstantiate = null;
        foreach(var i in displayItemsPrefabs)
        {
            if (i.GetComponent<T>())
            {
                itemToInstantiate = i;
            }
        }

        if (itemToInstantiate == null)
        {
            throw new System.Exception("Display item does not exist");
        }

        DisplayItem item = Instantiate(itemToInstantiate, scrollRect.content);
        item.gameObject.SetActive(true);

        DisplayItems.Add(item);

        return item as T;
    }

    public void ClearList()
    {
        for (int i = 0; i < DisplayItems.Count; i++)
        {
            Destroy(DisplayItems[i].gameObject);
        }

        DisplayItems.Clear();
    }
}
