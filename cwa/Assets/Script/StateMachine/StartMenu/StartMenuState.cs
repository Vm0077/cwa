using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StartMenuState : State<CanvasState>
{
    public Canvas canvas;
    // Start is called before the first frame update
    public StartMenuState(Canvas canvas) {
        this.canvas = canvas;
    }

    public void Initialize(Canvas canvas) {
        this.canvas = canvas;
    }

    public override void EnterState()
    {
        canvas.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        canvas.gameObject.SetActive(false);
    }
}
