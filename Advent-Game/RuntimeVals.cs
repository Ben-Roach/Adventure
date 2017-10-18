
using System.Diagnostics;

namespace Adventure
{
    public sealed class RuntimeVals
    {

        private static readonly RuntimeVals instance = new RuntimeVals();
        /// <summary>The instance of the <see cref="RuntimeVals"/> singleton.</summary>
        public static RuntimeVals Instance { get { return instance; } }

        /// <summary>True if compiled as a debug build, else false.</summary>
        public static bool Debugging { get; private set; }

        /// <summary>
        /// Instantiate the <see cref="RuntimeVals"/>.
        /// </summary>
        private RuntimeVals()
        {
            Debugging = false;
            SetDebug();
        }

        [Conditional("DEBUG")]
        public static void SetDebug()
        {
            Debugging = true;
        }
    }
}
