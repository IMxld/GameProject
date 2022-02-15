using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider TotalVoice;
    public Slider BGM;
    public Slider EffectVoice;
    // Start is called before the first frame update
    void Start()
    {
        TotalVoice.value = MainController.mainController.TotalVoice;
        BGM.value = MainController.mainController.BGM;
        EffectVoice.value = MainController.mainController.EffectVoice;
    }

    // Update is called once per frame
    void Update()
    {
        VoiceVolume();
    }

    private void VoiceVolume()
    {
        MainController.mainController.TotalVoice = TotalVoice.value;
        MainController.mainController.BGM = BGM.value;
        MainController.mainController.EffectVoice = EffectVoice.value;
    }
}
