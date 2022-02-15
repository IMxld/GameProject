using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sky : MonoBehaviour
{
    public static Sky _sky;
    public static Sky instance
    {
        get
        {
            if (_sky != null)
            {
                return _sky;
            }
            else
            {
                _sky = Transform.FindObjectOfType<Sky>();
                return _sky;
            }
        }
    }
    public SpriteAsset StarSprits;
    public List<Image> ConstellationImages;//大格子的位置
    // Start is called before the first frame update
    void Start()
    {
        StarSprits = Resources.Load<SpriteAsset>("ScriptsObjectStarSprites");
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 加载星图
    /// </summary>
    /// <param name="_bossNum"></param>
    /// <param name="_targetFrame"></param>
    void LoadStar(int _bossNum,int _targetFrame)
    {
        ConstellationImages[_targetFrame].sprite = StarSprits.Sprites[_bossNum];
    }
}
