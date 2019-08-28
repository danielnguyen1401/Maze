namespace Maze
{
    [System.Serializable]
    public struct IntVec
    {
        public int x, z;

        public IntVec(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public static IntVec operator +(IntVec a, IntVec b)
        {
            a.x += b.x;
            a.z += b.z;
            return a;
        }
    }
}