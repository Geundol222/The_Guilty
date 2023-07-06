using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.2f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.4f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.6f;
        yield return new WaitForSecondsRealtime(1f);

        progress = 1f;
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
