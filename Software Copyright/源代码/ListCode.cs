using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListCode : MonoBehaviour
{
    public int type;
    public GameObject collectionButton1;
    public GameObject collectionButton2;
    public float height;
    public bool iscollection;
    private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<Manager>();
       
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        collectionButton2.SetActive(false);
        if (!iscollection)
        {
            for (int i = 0; i < DateManager.CollectionList.Count; i++)
            {
                print(DateManager.CollectionList[i]);
                if (DateManager.CollectionList[i] == type)
                {
                    collectionButton1.SetActive(false);
                    collectionButton2.SetActive(true);
                }
            }
        }
    }

    //查看按钮
    public void Check()
    {
        manager.LookButton(type);
    }
    //收藏
    public void Collection()
    {
        if (!DateManager.CollectionList.Contains(type))
        {
            collectionButton1.SetActive(false);
            collectionButton2.SetActive(true);
            DateManager.CollectionList.Add(type);
        }
    }
    //取消收藏
    public void CancelCollection()
    {
        collectionButton1.SetActive(true);
        collectionButton2.SetActive(false);
        if (DateManager.CollectionList.Count != 0)
        {
            for (int i = 0; i < DateManager.CollectionList.Count; i++)
            {
                if (DateManager.CollectionList[i] == type)
                    DateManager.CollectionList.Remove(DateManager.CollectionList[i]);
            }
        }
    }
}
