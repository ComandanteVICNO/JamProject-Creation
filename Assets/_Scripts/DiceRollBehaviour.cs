using System.Collections;
using TMPro;
using UnityEngine;

public class DiceRollBehaviour : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject diceRollUI;
    [SerializeField] private GameObject diceAnimationObject;
    [SerializeField] private GameObject diceFinalObject;
    [SerializeField] private AnimationClip diceAnimationClip;
    [SerializeField] private TMP_Text numberTitle;
    [SerializeField] private AudioSource diceAudioSource;
    [SerializeField] private AudioClip[] diceAudios;
    [Header("Colors")]
    [SerializeField] private Color criticalFailureColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Color neutralColor;
    [SerializeField] private Color successColor;
    [SerializeField] private Color criticalSuccessColor;
    
    Coroutine diceAnimation;
    public void RollDice(int diceRoll)
    {
        if(diceAnimation != null) StopCoroutine(diceAnimation);
        
        
        if (diceRoll == 1) numberTitle.color = criticalFailureColor;
        else if(diceRoll >1 && diceRoll <= 9) numberTitle.color = failureColor;
        else if(diceRoll == 10) numberTitle.color = neutralColor;
        else if(diceRoll > 10 && diceRoll <= 19) numberTitle.color = successColor;
        else numberTitle.color = criticalSuccessColor;
        
        numberTitle.text = diceRoll.ToString();
        diceAudioSource.pitch = Random.Range(0.8f, 1.2f);
        if(diceAudioSource != null)diceAudioSource.PlayOneShot(diceAudios[Random.Range(0, diceAudios.Length)]);
        diceAnimation = StartCoroutine(PlayDiceAnimation());

    }

    IEnumerator PlayDiceAnimation()
    {
        diceRollUI.SetActive(true);
        diceFinalObject.SetActive(false);
        diceAnimationObject.SetActive(true);
        
        yield return new WaitForSecondsRealtime(diceAnimationClip.length);
        
        diceAnimationObject.SetActive(false);
        diceFinalObject.SetActive(true);
        
        yield return new WaitForSecondsRealtime(1f);
        diceRollUI.SetActive(false);
        
    }
    
}
