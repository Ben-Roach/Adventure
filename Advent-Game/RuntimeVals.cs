
using System.Diagnostics;

namespace Adventure
{
    public sealed class RuntimeVals
    {

        private static readonly RuntimeVals instance = new RuntimeVals();
        /// <summary>The instance of the <see cref="RuntimeVals"/> singleton.</summary>
        public static RuntimeVals Instance { get { return instance; } }

        /// <summary>True if compiled as a debug build, else false.</summary>
        public static bool IsDebug { get; private set; }

        /// <summary>
        /// Instantiate the <see cref="RuntimeVals"/> singleton.
        /// </summary>
        private RuntimeVals()
        {
            IsDebug = false;
            Debug.Assert(IsDebug = true); // not executed if release build
        }
    }
}
