
using System;

namespace Adventure.Controller
{
    /// <summary>
    /// Used to point to a <see cref="Definition"/> with a conditional delegate and a priority rank.
    /// Does not inherit from or contain a <see cref="Definition"/>.
    /// </summary>
    public class ConditionalDef
    {
        /// <summary>The <see cref="Definition.ID"/> of the <see cref="Definition"/> this <see cref="ConditionalDef"/>
        /// refers to.</summary>
        public string DefID { get; }
        /// <summary>Denotes that the previous <see cref="Node"/> in the <see cref="Sentence"/> must be of this type
        ///  before using this <see cref="ConditionalDef"/>. Null if not applicable.</summary>
        public Type PrevNodeType { get; }
        /// <summary>A string that the <see cref="Node.DefID"/> of the previous <see cref="Node"/> in the <see cref="Sentence"/>
        /// must match before using this <see cref="ConditionalDef"/>. Null if not applicable.</summary>
        public string PrevNodeID { get; }
        /// <summary>Denotes that the previous <see cref="Node"/> in the <see cref="Sentence"/> must be of this type
        ///  before using this <see cref="ConditionalDef"/>. Null if not applicable.</summary>
        public Type NextNodeType { get; }
        /// <summary>A string that the <see cref="Node.DefID"/> of the next <see cref="Node"/> in the <see cref="Sentence"/>
        /// must match before using this <see cref="ConditionalDef"/>. Null if not applicable.</summary>
        public string NextNodeID { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defID"></param>
        /// <param name="prevNodeType"></param>
        /// <param name="prevNodeDefID"></param>
        /// <param name="nextNodeType"></param>
        /// <param name="nextNodeDefID"></param>
        public ConditionalDef(string defID,
            Type prevNodeType = null, string prevNodeDefID = null, Type nextNodeType = null, string nextNodeDefID = null)
        {
            DefID = defID ?? throw new ArgumentNullException(nameof(defID));
            PrevNodeType = prevNodeType;
            PrevNodeID = prevNodeDefID;
            NextNodeType = nextNodeType;
            NextNodeID = nextNodeDefID;
        }

        /// <summary>
        /// Returns true if this <see cref="ConditionalDef"/> depends on the previous <see cref="Node"/> in the <see cref="Sentence"/>.
        /// </summary>
        public bool DependsOnPrev()
        {
            if (PrevNodeType == null && PrevNodeID == null)
                return false;
            return true;
        }

        /// <summary>
        /// Returns true if this <see cref="ConditionalDef"/> depends on the next <see cref="Node"/> in the <see cref="Sentence"/>.
        /// </summary>
        public bool DependsOnNext()
        {
            if (NextNodeType == null && NextNodeID == null)
                return false;
            return true;
        }

        /// <summary>
        /// Returns whether a <see cref="Node"/> matches the next node criteria of this <see cref="ConditionalDef"/>.
        /// </summary>
        /// <param name="prev">The <see cref="Node"/> to test. Will return false if null.</param>
        public bool MatchesPrevNode(Node prev)
        {
            if (prev != null
                && (PrevNodeType == null || prev.GetType() == PrevNodeType)
                && (PrevNodeID == null || prev.DefID == PrevNodeID))
                return true;
            return false;
        }

        /// <summary>
        /// Returns whether a <see cref="Node"/> matches the next node criteria of this <see cref="ConditionalDef"/>.
        /// </summary>
        /// <param name="next">The <see cref="Node"/> to test. Will return false if null.</param>
        public bool MatchesNextNode(Node next)
        {
            if (next != null
                && (NextNodeType == null || next.GetType() == NextNodeType)
                && (NextNodeID == null || next.DefID == NextNodeID))
                return true;
            return false;
        }
    }
}
