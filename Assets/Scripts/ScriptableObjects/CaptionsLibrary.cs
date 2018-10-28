using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CaptionsLibrary")]
public class CaptionsLibrary : ScriptableObject {

    public string[] freeCaptions;
    public string[] strikerCaptions;
    public string[] healerCaptions;
    public string[] producerCaptions;
    public string[] deathCaptions;

    public string GetCaption(CaptionsType captionsType, PlayerSide side)
    {
        int randomGeneric = Mathf.RoundToInt(Random.value);
        int index = 0;
        switch(side)
        {
            case PlayerSide.Cats:
                index = randomGeneric * 1;
                break;

            case PlayerSide.Dogs:
                index = randomGeneric * 2;
                break;
        }

        string[] captionChoices;
        switch(captionsType)
        {
            case CaptionsType.Death:
                captionChoices = deathCaptions[index].Split(',');
                break;

            case CaptionsType.Free:
                captionChoices = freeCaptions[index].Split(',');
                break;

            case CaptionsType.Healer:
                captionChoices = healerCaptions[index].Split(',');
                break;

            case CaptionsType.Producer:
                captionChoices = producerCaptions[index].Split(',');
                break;

            case CaptionsType.Striker:
                captionChoices = strikerCaptions[index].Split(',');
                break;

            default:
                captionChoices = null;
                break;
        }

        Debug.LogWarning("CAPTIONS SIDE: " + index);
        Debug.LogWarning("Captions length: " + captionChoices.Length);
        index = Random.Range(0, captionChoices.Length);
        Debug.LogWarning("CAPTIONS RANDOM: " + index);
        Debug.LogWarning("CAPTIONS: " + captionChoices[index]);

        return captionChoices[index].Trim(' ');
        
    }

    public enum CaptionsType
    {
        Free,
        Striker,
        Healer,
        Producer,
        Death
    }
}
