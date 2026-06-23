using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RevBand : MonoBehaviour
{
    public float revs;
    public float idealRevPoint;
    public float revPenalty;

    public RevBand(float revs = 1, float idealRevPoint = 3500, float revPenalty = 1)
    {
        this.revs = revs;
        this.idealRevPoint = idealRevPoint;
        this.revPenalty = revPenalty;
    }

    public float rev(float rpm, float REVLIMIT)
    {
        // print("revving by " + math.max(revs - (math.abs(rpm - idealRevPoint) / REVLIMIT) * revPenalty, 0));
        // print("formula output: " + (math.abs(rpm - idealRevPoint) / REVLIMIT) * revPenalty);
        float res = rpm;

        //the further away rpm is from the idealRevPoint, the higher the revPenalty will be
        res += math.max(revs - (math.abs(rpm - idealRevPoint) / REVLIMIT) * revPenalty, 0);
        return res;
    }
}
