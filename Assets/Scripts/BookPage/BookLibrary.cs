using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BookLibrary : MonoBehaviour
{
    public List<GameObject> Points;
    public GameObject imfo;
    // Start is called before the first frame update
    void Start()
    {
        
        int i = 0;
        foreach (GameObject _item in MainController.mainController.CurrentPlayerController.DramaLibrary)
        {
            GameObject _drama = Instantiate(_item, Points[i].GetComponent<RectTransform>());
            _drama.transform.SetParent(gameObject.transform);
            _drama.GetComponent<Image>().sprite = _drama.GetComponent<MainDrama>().ImagePreference;
            ChangeSize(_drama, 355, 490);
            BookInformation bookInformation = _drama.AddComponent<BookInformation>();
            bookInformation.information = imfo;
            i++;
        }
    }

    private void ChangeSize(GameObject _gameObject,float _targetX, float _targetY)
    {
        _gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _targetX);
        _gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _targetY);
    }
}
