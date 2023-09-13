using System;

namespace Webfuel
{
    public static class GuidGenerator
    {
        /// <summary>
        /// Generates a strictly increasing GUID for use as a database key
        /// </summary>
        public static Guid NewComb()
        {
            return NewComb(DateTime.Now);
        }


        /// <summary>
        /// Generates a strictly increasing GUID for use as a database key
        /// </summary>
        public static Guid NewComb(DateTime asAt)
        {
            byte[] uid = Guid.NewGuid().ToByteArray();
            byte[] binDate = BitConverter.GetBytes(asAt.Ticks);

            // create comb in SQL Server sort order
            byte[] comb = new byte[uid.Length];

            // the first 7 bytes are random - if two combs are generated at the same point in time
            // they are not guaranteed to be sequential. But for every DateTime.Tick there are
            // 72,057,594,037,927,935 unique possibilities so there shouldn't be any collisions
            comb[3] = uid[0];
            comb[2] = uid[1];
            comb[1] = uid[2];
            comb[0] = uid[3];
            comb[5] = uid[4];
            comb[4] = uid[5];
            comb[7] = uid[6];
            comb[6] = uid[7];

            // the last 8 bytes are sequential, these will reduce index fragmentation
            // to a degree as long as there are not a large number of Combs generated per millisecond
            comb[9] = binDate[0];
            comb[8] = binDate[1];
            comb[15] = binDate[2];
            comb[14] = binDate[3];
            comb[13] = binDate[4];
            comb[12] = binDate[5];
            comb[11] = binDate[6];
            comb[10] = binDate[7];

            return new Guid(comb);
        }
    }
}
