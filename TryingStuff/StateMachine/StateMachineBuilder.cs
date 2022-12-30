namespace TryingStuff.StateMachine;

public class StateMachineBuilder : IStateMachineBuilder
{
    private bool _hasInitialState;
    private readonly Dictionary<string, State> _states = new();

    public IStateMachineBuilder WithInitialState(string stateName)
    {
        _states.Add(stateName, new State(stateName));
        _hasInitialState = true;
        return this;
    }

    public IStateMachineBuilder WithTransition(string fromState, string toState, string input)
    {
        if (!_states.TryGetValue(fromState, out var fState))
        {
            fState = new State(fromState);
            _states.Add(fromState, fState);
        }

        if (!_states.TryGetValue(toState, out var tState))
        {
            tState = new State(toState);
            _states.Add(toState, tState);
        }

        _states[fromState].AddTransition(input, _states[toState]);
        return this;
    }

    public IStateMachine Build()
    {
        if (!_hasInitialState)
        {
            throw new Exception("No initial state defined");
        }

        return new StateMachine(_states.Values);
    }
}

public interface IStateMachineBuilder
{
    IStateMachineBuilder WithInitialState(string stateName);
    IStateMachineBuilder WithTransition(string fromState, string toState, string input);
    IStateMachine Build();
}