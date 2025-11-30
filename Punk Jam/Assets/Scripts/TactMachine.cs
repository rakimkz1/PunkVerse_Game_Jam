using System;
using System.Threading.Tasks;
using UnityEngine;

public class TactMachine : MonoBehaviour
{
    public float tact;
    public float difficalty;
    public float failColdown;

    public event Action onTactBeat;

    [SerializeField] private AudioClip loseBeat;

    private float _nowTact = 0f;
    public bool isBeatable = true;

    public void Update()
    {
        _nowTact += Time.deltaTime;
        
        if(_nowTact > tact)
        {
            onTactBeat?.Invoke();
            _nowTact = _nowTact - tact;
        }
    }

    public bool IsBeatTact()
    {
        float answer = Mathf.Min(Mathf.Abs(tact - _nowTact), _nowTact);

        if (answer > difficalty && isBeatable)
            FailColdown();
        return (answer < difficalty) && isBeatable;
    }

    private async Task FailColdown()
    {
        isBeatable = false;
        AudioManager.instance.PlayAudioOneShot(loseBeat, 2f);
        await Task.Delay((int)(failColdown * 1000));
        isBeatable = true;
    }
}