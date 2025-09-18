using UnityEngine;
using System.Collections.Generic;

public static class YieldCache
{
    #region WaitForSeconds
    private static readonly Dictionary<float, WaitForSeconds> _waitForSeconds = new();

    public static WaitForSeconds WaitForSeconds(float time)
    {
        if (!_waitForSeconds.ContainsKey(time))
        {
            _waitForSeconds.Add(time, new WaitForSeconds(time));
        }
        return _waitForSeconds[time];
    }
    #endregion

    #region WaitForSecondsRealtime
    private static readonly Dictionary<float, WaitForSecondsRealtime> _waitForSecondsRealtime = new();

    public static WaitForSecondsRealtime WaitForSecondsRealtime(float time)
    {
        if (!_waitForSecondsRealtime.ContainsKey(time))
        {
            _waitForSecondsRealtime.Add(time, new WaitForSecondsRealtime(time));
        }
        return _waitForSecondsRealtime[time];
    }
    #endregion    
}
