using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary>The priority level of this conditional; higher priority conditionals will be evaluated first.
        /// A higher priority is represented by a larger number.</summary>
        public int Priority { get; }
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

        public ConditionalDef(string defID, int priority,
            Type prevNodeType = null, string prevNodeID = null, Type nextNodeType = null, string nextNodeID = null)
        {
            DefID = defID ?? throw new ArgumentNullException(nameof(defID));
            Priority = priority;
            PrevNodeType = prevNodeType;
            PrevNodeID = prevNodeID;
            NextNodeType = nextNodeType;
            NextNodeID = nextNodeID;
        }
    }
}
