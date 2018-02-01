
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
        /// points to.</summary>
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
        /// <param name="prevNodeID"></param>
        /// <param name="nextNodeType"></param>
        /// <param name="nextNodeID"></param>
        public ConditionalDef(string defID,
            Type prevNodeType = null, string prevNodeID = null, Type nextNodeType = null, string nextNodeID = null)
        {
            DefID = defID ?? throw new ArgumentNullException(nameof(defID));
            PrevNodeType = prevNodeType;
            PrevNodeID = prevNodeID;
            NextNodeType = nextNodeType;
            NextNodeID = nextNodeID;
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
    }
}
