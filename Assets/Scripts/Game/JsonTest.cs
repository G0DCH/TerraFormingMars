using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class JsonTest : MonoBehaviour
{
    public Image Card;

    // Start is called before the first frame update
    void Start()
    {
        LoadImage();
    }

    public void LoadImage()
    {
        if (File.Exists(Application.dataPath
            + "/Resources/InfoJSONFiles/Test.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath
            + "/Resources/InfoJSONFiles/Test.json");

            JsonData playerData = JsonMapper.ToObject(jsonStr);

            for (int i = 0; i < 10; i++)
            {
                Debug.Log(playerData[0]["CardImage"]);
                Image card = Instantiate(Card.gameObject,transform).GetComponent<Image>();
                card.sprite = Resources.Load("Cards/Active/"
                    + playerData[0]["CardImage"], typeof(Sprite)) as Sprite;

                //card.transform.localPosition = Vector3.zero;
            }
        }

        else
            Debug.Log("파일이 없음");
    }
}
