using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BNG;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public Animation anima;
    public List<AnimationClip> clips = new List<AnimationClip>();
    public GameObject viewObj;
    public GameObject leftViewObj;
    public GameObject mainMenu;
    //功法
    public GameObject gfObj;
    //动作
    public GameObject dzObj;
    //场景
    public GameObject cjObj;

    //播放动画
    public GameObject playAnimaObj;
    //暂停
    public GameObject pauseAnimaObj;
    //快进
    private bool isForward;
    //快退
    private bool isRewind;
    public int curIndex;
    private float timer1;//前进时间
    private float timer2;//后腿时间
    public Image sliderImage;
    public Transform collectionButtonObj;
    public GameObject collectionObj;
    public Transform collectionConnect;
    public List<GameObject> collectionPrefabs=new List<GameObject>();
    public GameObject lookObj;
    public List<GameObject> lookObjs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playAnimaObj.SetActive(false);
        for (int i = 0; i < lookObj.transform.childCount; i++)
        {
            lookObjs.Add(lookObj.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sliderImage.fillAmount = anima[clips[curIndex].name].normalizedTime;
        if (anima[clips[curIndex].name].normalizedTime >= 0.98f)
        {
            anima[clips[curIndex].name].normalizedTime = 0;
            pauseAnimaObj.SetActive(false);
            playAnimaObj.SetActive(true);
            foreach (AnimationState state in anima)
            {
                state.speed = 0f;
            }
        }
        //print(anima[clips[curIndex].name].normalizedTime);
        if (Input.GetKeyDown(KeyCode.T) || InputBridge.Instance.AButtonDown)
        {
            viewObj.SetActive(!viewObj.activeInHierarchy);
            if (viewObj.activeInHierarchy)
            {
                mainMenu.SetActive(true);
            }
            else
            {
                CloseAll();
            }
        }
        if (Input.GetKeyDown(KeyCode.Y) || InputBridge.Instance.XButtonDown)
        {
            leftViewObj.SetActive(!leftViewObj.activeInHierarchy);
        }
        if (isForward)
        {
            timer1 = timer1 + Time.deltaTime;
            if (timer1 > 0.1f)
            {
                timer1 = 0;
                isForward = false;
                foreach (AnimationState state in anima)
                {
                    state.speed = 1f;
                    anima.Play();
                }
            }
        }
        if (isRewind)
        {
            timer2 = timer2 + Time.deltaTime;
            if (timer2 > 0.25f)
            {
                timer2 = 0;
                isRewind = false;
                foreach (AnimationState state in anima)
                {
                    state.speed = 1f;
                    anima.Play();
                }
            }
        }
    }
    public void CloseAll()
    {
        //viewObj.SetActive(false);
        mainMenu.SetActive(false);
        gfObj.SetActive(false);
        dzObj.SetActive(false);
        cjObj.SetActive(false);
        lookObj.SetActive(false);
        collectionObj.SetActive(false);
        if (collectionConnect.childCount != 0)
        {
            for (int i = 0; i < collectionConnect.childCount; i++)
            {
                Destroy(collectionConnect.GetChild(i).gameObject);
            }
        }
        collectionButtonObj.transform.GetChild(1).gameObject.SetActive(false);
        collectionButtonObj.transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 0; i < DateManager.CollectionList.Count; i++)
        {
            if (DateManager.CollectionList[i] == curIndex)
            {
                collectionButtonObj.transform.GetChild(1).gameObject.SetActive(true);
                collectionButtonObj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void GongFa()
    {
        CloseAll();
        gfObj.SetActive(true);
    }
    public void DongZuo()
    {
        CloseAll();
        dzObj.SetActive(true);
    }
    public void ChangJing()
    {
        CloseAll();
        cjObj.SetActive(true);
    }
    //播放动画
    public void ButtonF(int index)
    {
        collectionButtonObj.transform.GetChild(1).gameObject.SetActive(false);
        collectionButtonObj.transform.GetChild(0).gameObject.SetActive(true);
        anima.clip = clips[index];
        anima.Play();
        curIndex = index;
        for (int i = 0; i < DateManager.CollectionList.Count; i++)
        {
            if (DateManager.CollectionList[i] == curIndex)
            {
                collectionButtonObj.transform.GetChild(1).gameObject.SetActive(true);
                collectionButtonObj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void PauseAnima()
    {
        pauseAnimaObj.SetActive(false);
        playAnimaObj.SetActive(true);
        foreach (AnimationState state in anima)
        {
            state.speed = 0f;
        }
    }

    public void PlayAnima()
    {
        playAnimaObj.SetActive(false);
        pauseAnimaObj.SetActive(true);
        foreach (AnimationState state in anima)
        {
            state.speed = 1f;
        }
    }
    public void ForwardButton()
    {
        anima.Play();
        isForward = true;
        timer1 = 0;
        foreach (AnimationState state in anima)
        {
            state.speed = 3f;
        }
    }
    public void RewindButton()
    {
       
        isRewind = true;
        timer2 = 0;
        foreach (AnimationState state in anima)
        {
            state.speed = -3f;
        }
    }
    public void CollectionF()
    {
        
        if (collectionButtonObj.transform.GetChild(1).gameObject.activeInHierarchy)
        {

        }
        else
        {
            collectionButtonObj.transform.GetChild(1).gameObject.SetActive(true);
            collectionButtonObj.transform.GetChild(0).gameObject.SetActive(false);
            if (!DateManager.CollectionList.Contains(curIndex))
            {
                DateManager.CollectionList.Add(curIndex);
            }
              
        }
    }
    public void MyCollecttionButton()
    {
        CloseAll();
        if (DateManager.CollectionList.Count != 0)
        {
            float curHeight=0;
            for (int i = 0; i < DateManager.CollectionList.Count; i++)
            {
                GameObject obj = GameObject.Instantiate(collectionPrefabs[DateManager.CollectionList[i]],collectionConnect);
                obj.transform.localPosition = new Vector3(0, curHeight, 0);
                curHeight = -collectionPrefabs[DateManager.CollectionList[i]].GetComponent<ListCode>().height + curHeight;
            }
        }
        collectionObj.SetActive(true);
    }
    public void LookButton(int index)
    {
        CloseAll();
        for (int i = 0; i < lookObjs.Count; i++)
        {
            lookObjs[i].SetActive(false);
        }
        lookObj.SetActive(true);
        lookObjs[index].SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
