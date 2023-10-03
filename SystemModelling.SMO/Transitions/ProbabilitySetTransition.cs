﻿using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Transitions;

public class ProbabilitySetTransition : ITransition
{
    private List<ProbabilityOption> _options;
    
    public ProbabilitySetTransition(List<ProbabilityOption> options)
    {
        _options = options;

        if (!ValidateOptions())
        {
            throw new ArgumentException("Sum of probabilities must be less or equal 1");
        }
    }

    private bool ValidateOptions()
    {
        var sum = 0f;
        foreach (var option in _options)
        {
            sum += option.Probability;
        }

        return sum <= 1f;
    }

  

    public Element? Next => GetNextElement();

    private Element? GetNextElement()
    {
        if (_options == null || _options.Count == 0)
        {
            return null;
        }

        double randomValue = Random.Shared.NextDouble();
        float sum = 0f;
        foreach (var option in _options)
        {
            sum += option.Probability;
            if (randomValue <= sum)
            {
                return option.Transition.Next;
            }
        }

        return null;
    }
}