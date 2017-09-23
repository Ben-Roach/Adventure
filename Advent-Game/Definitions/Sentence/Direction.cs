
using System;
using System.Collections.Generic;

namespace Sentence
{
    /// <summary>
    /// Represents a word used by the player primarily for movement.
    /// </summary>
    class Direction : INode
    {
        /// <summary>Valid values for <see cref="DirCode"/>.</summary>
        static readonly List<string> validDirCodes = new List<string>() { "N", "E", "S", "W", "NE", "NW", "SE", "SW" };
        public string OrigToken { get; }
        /// <summary>Represents the actual direction to use.</summary>
        public string DirCode { get; }

        /// <summary>
        /// Create a new <see cref="Direction"/>.
        /// </summary>
        /// <param name="dirCode">Represents the actual direction to use.</param>
        /// <exception cref="ArgumentException">Thrown when dirCode is invalid.</exception>
        /// <exception cref="ArgumentNullException">Thrown when origToken or dirCode is null.</exception>
        public Direction(string origToken, string dirCode)
        {
            if (dirCode == null) throw new ArgumentNullException("Attempted to create a Direction with a null direction code.");
            if (!validDirCodes.Contains(dirCode)) throw new ArgumentException("Attempted to create a Direction with an invalid direction code.");
            OrigToken = origToken != null ? origToken : throw new ArgumentNullException("Attempted to create a Direction with a null origToken.");
            DirCode = dirCode;
        }
    }
}