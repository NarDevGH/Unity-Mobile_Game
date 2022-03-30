using UnityEngine;
using TMPro;

public class GameoverPanel_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointsTxt;

    public void SetPoints(int points) 
    {
        _pointsTxt.text = "Score : " + points.ToString();    
    }
}
