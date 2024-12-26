using System.Collections;
using UnityEngine;

public class GameSaveState :  StartMenuState
{
    public GameSaveState (Canvas canvas):base(canvas) {
    }
    public override CanvasState GetNextState()
    {
       return CanvasState.GameSaveMenu;
    }

    public override void InitializeSubState()
    {
    }

    public override void onTriggerEnter(Collider other)
    {
    }

    public override void onTriggerExit(Collider other)
    {
    }

    public override void onTriggerStay(Collider other)
    {
    }

    public override void UpdateState()
    {

    }
    public void CreateNewSave() {

    }

    public void DeleteSaveFile() {

    }

    public void StartSave() {

    }
}
