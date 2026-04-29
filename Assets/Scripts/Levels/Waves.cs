using UnityEngine;
using RPNEvaluator;

public class Waves : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void Hello()
    {
        Debug.Log("wee");
        var result = RPNEvaluator.RPNEvaluator.Evaluate("1 1 +", null);
        Debug.Log(result);
    }
}
