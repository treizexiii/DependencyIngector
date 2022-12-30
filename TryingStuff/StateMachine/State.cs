namespace TryingStuff.StateMachine;

public class State
{
    public string Name { get; }
    private readonly Dictionary<string, State> _transitions;
    public State(string name)
    {
        Name = name;
        _transitions = new();
    }

    public void AddTransition(string input, State state)
    {
        _transitions.Add(input, state);
    }

    public State GetTransition(string p0)
    {
        return _transitions[p0];
    }
}