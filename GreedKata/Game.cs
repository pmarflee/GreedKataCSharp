﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GreedKata
{
    public abstract class Scorer
    {
        protected readonly int Points;

        protected Scorer(int points)
        {
            Points = points;
        }

        public abstract Tuple<int, int[]> Score(int[] dice);
    }

    public abstract class OfAKindScorer : Scorer
    {
        protected readonly int _number;
        protected readonly int _count;

        public OfAKindScorer(int number, int count, int points) : base(points)
        {
            _number = number;
            _count = count;
        }

        public override Tuple<int, int[]> Score(int[] dice)
        {
            var matches = new List<int>();
            var newMatches = new List<int>();

            for (var i = 0; i < dice.Length; i++)
            {
                if (dice[i] == _number)
                {
                    newMatches.Add(i);
                    if (newMatches.Count == _count)
                    {
                        matches.AddRange(newMatches);
                        newMatches.Clear();
                    }
                }
            }

            var score = Points * (matches.Count / _count);
            var remainder = dice.Where((die, index) => !matches.Contains(index)).ToArray();

            return Tuple.Create(score, remainder);
        }
    }

    public class OneOfAKindScorer : OfAKindScorer
    {
        public OneOfAKindScorer(int number, int points) : base(number, 1, points) { }
    }

    public class TripleScorer : OfAKindScorer
    {
        public TripleScorer(int number) : this(number, number * 100) { }

        public TripleScorer(int number, int points) : base(number, 3, points) { }
    }

    public class TripleOnesScorer : TripleScorer
    {
        public TripleOnesScorer() : base(1, 1000) { }
    }

    public static class Game
    {
        private static readonly Scorer[] _scorers = 
        {
            new TripleOnesScorer(),
            new TripleScorer(2),
            new TripleScorer(3),
            new TripleScorer(4),
            new TripleScorer(5),
            new TripleScorer(6),
            new OneOfAKindScorer(1, 100),
            new OneOfAKindScorer(5, 50)
        };

        internal static int Score(int[] dice)
        {
            var overallResult = _scorers.Aggregate(Tuple.Create(0, dice), (acc, rule) =>
            {
                var result = rule.Score(acc.Item2);
                return Tuple.Create(acc.Item1 + result.Item1, result.Item2);
            });

            return overallResult.Item1;
        }
    }
}