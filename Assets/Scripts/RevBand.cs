using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RevBand : MonoBehaviour
{
    float revs;
    float idealRevPoint;
    float revPenalty;
    
    public RevBand(float revs, float idealRevPoint)
    {
        this.revs = revs;
        this.idealRevPoint = idealRevPoint;
    }

    public float rev(float rpm, float REVLIMIT)
    {
        print("revving");
        float res = rpm;

        //the further away rpm is from the idealRevPoint, the higher the revPenalty will be
        res += revs - (math.abs(rpm - idealRevPoint) / REVLIMIT) * revPenalty;
        return res;
    }
}
