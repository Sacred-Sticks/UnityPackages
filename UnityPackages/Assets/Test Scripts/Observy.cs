using Kickstarter.Observer;
using UnityEngine;

public class Observy : Observable, IObserver<Observy.ObserveMe>, IObserver<Observy.Observer>
{
    [SerializeField] private bool trigger;
    [SerializeField] private bool trigger2;
    
    #region Unity Events
    private void OnEnable()
    {
        AddObserver(this);
    }

    private void OnDisable()
    {
        RemoveObserver(this);
    }

    private void Update()
    {
        if (trigger)
            NotifyObservers(new ObserveMe());
        if (trigger2)
            NotifyObservers(new Observer());
    }
    #endregion

    public void OnNotify(ObserveMe argument)
    {
        Debug.Log("good");
        transform.position = new Vector3(0, 100, 0);
    }

    public class ObserveMe
    {
        
    }
    
    public class Observer
    {
        
    }
    public void OnNotify(Observer argument)
    {
        Debug.Log("Bussy");
    }
}
