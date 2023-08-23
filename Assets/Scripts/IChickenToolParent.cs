using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChickenToolParent {
    ChickenTool GetChickenTool();

    void SetChickenTool(ChickenTool chickenTool);

    void ClearChickenTool();

    bool HasChickenTool();

    Transform GetChickenToolFollowPoint();
}
