﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GreedKata
{
    public class ScoringRule
    {
        private readonly int _number;
        private readonly int _count;
        private readonly int _score;

        public ScoringRule(int number, int count, int score)
        {
            _number = number;
            _count = count;
            _score = score;
        }

        public Tuple<int, int[]> Score(int[] dice)
        {
            var found = new List<List<int>>();
            var match = new List<int>();

            for (var i = 0; i < dice.Length; i++)
            {
                if (dice[i] == _number)
                {
                    match.Add(i);
                    if (match.Count == _count)
                    {
                        found.Add(match);
                        match = new List<int>();
                    }
                }
            }

            var score = _score * found.Count;
            var allIndexes = found.SelectMany(index => index).ToList();
            var remainder = dice.Where((die, index) => !allIndexes.Contains(index)).ToArray();

            return Tuple.Create(score, remainder);
        }
    }

    public static class Scorer
    {
        private static readonly ScoringRule[] _rules = 
        {
            new ScoringRule(1, 3, 1000),
            new ScoringRule(2, 3, 200),
            new ScoringRule(3, 3, 300),
            new ScoringRule(4, 3, 400),
            new ScoringRule(5, 3, 500),
            new ScoringRule(6, 3, 600),
            new ScoringRule(1, 1, 100),
            new ScoringRule(5, 1, 50),
        };

        internal static int Score(int[] dice)
        {
            var overallResult = _rules.Aggregate(Tuple.Create(0, dice), (acc, rule) =>
            {
                var result = rule.Score(acc.Item2);
                return Tuple.Create(acc.Item1 + result.Item1, result.Item2);
            });

            return overallResult.Item1;
        }
    }
}