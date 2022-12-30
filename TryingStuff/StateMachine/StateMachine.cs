namespace TryingStuff.StateMachine;

internal class StateMachine : IStateMachine
{
    public StateMachine(Dictionary<string, State>.ValueCollection statesValues)
    {
        States = statesValues.ToDictionary(s => s.Name, s => s);
        CurrentState = States.First().Value;
    }

    public State CurrentState { get; private set; }

    public Dictionary<string, State> States { get; }

    public void Process(string p0)
    {
        State newState;
        try
        {
            newState = States[CurrentState.Name].GetTransition(p0);
        }
        catch (Exception)
        {
            Console.WriteLine("StateMachine: Invalid transition");
            newState = CurrentState;
        }
        
        CurrentState = newState;
    }
}

public interface IStateMachine
{
    State CurrentState { get; }
    Dictionary<string, State> States { get; }
    void Process(string p0);
}