using com.google.zxing.qrcode.decoder;
/*
* Copyright 2007 ZXing authors
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using BitMatrix = com.google.zxing.common.BitMatrix;

namespace com.google.zxing.microqrcode.decoder
{

    /// <summary> 
    /// See ISO 18004:2006 Annex D
    /// </summary>
    /// <author>  Sean Owen
    /// </author>
    /// <author>www.Redivivus.in (suraj.supekar@redivivus.in) - Ported from ZXING Java Source 
    /// </author>
    public sealed class Version
    {
        public int VersionNumber
        {
            get
            {
                return versionNumber;
            }
        }
        public int TotalCodewords
        {
            get
            {
                return totalCodewords;
            }
        }
        public int DimensionForVersion
        {
            get
            {
                return 9 + 2 * versionNumber;
            }
        }

        /// <summary> 
        /// See ISO 18004:2006 Annex D.
        /// Element i represents the raw version bits that specify version i + 7
        /// </summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'VERSION_DECODE_INFO'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //private static readonly int[] VERSION_DECODE_INFO = new int[] { 0x07C94, 0x085BC, 0x09A99, 0x0A4D3, 0x0BBF6, 0x0C762, 0x0D847, 0x0E60D, 0x0F928, 0x10B78, 0x1145D, 0x12A17, 0x13532, 0x149A6, 0x15683, 0x168C9, 0x177EC, 0x18EC4, 0x191E1, 0x1AFAB, 0x1B08E, 0x1CC1A, 0x1D33F, 0x1ED75, 0x1F250, 0x209D5, 0x216F0, 0x228BA, 0x2379F, 0x24B0B, 0x2542E, 0x26A64, 0x27541, 0x28C69 };

        //UPGRADE_NOTE: Final was removed from the declaration of 'VERSIONS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private static readonly Version[] VERSIONS = buildVersions();

        //UPGRADE_NOTE: Final was removed from the declaration of 'versionNumber '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int versionNumber;
        //UPGRADE_NOTE: Final was removed from the declaration of 'ecBlocks '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private ECBlocks[] ecBlocks;
        //UPGRADE_NOTE: Final was removed from the declaration of 'totalCodewords '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int totalCodewords;

        private Version(int versionNumber, ECBlocks ecBlocks1, ECBlocks ecBlocks2, ECBlocks ecBlocks3, ECBlocks ecBlocks4)
        {
            this.versionNumber = versionNumber;
            this.ecBlocks = new ECBlocks[] { ecBlocks1, ecBlocks2, ecBlocks3, ecBlocks4 };
            int total = 0;
            int ecCodewords = ecBlocks1.ECCodewordsPerBlock;
            ECB[] ecbArray = ecBlocks1.getECBlocks();
            for (int i = 0; i < ecbArray.Length; i++)
            {
                ECB ecBlock = ecbArray[i]; //ECBlocks(7, new ECB(1, 19) 19+7=26
                total += ecBlock.Count * (ecBlock.DataCodewords + ecCodewords);
            }
            this.totalCodewords = total;
        }

        public ECBlocks getECBlocksForLevel(ErrorCorrectionLevel ecLevel)
        {
            return ecBlocks[ecLevel.ordinal()];
        }

        /// <summary> <p>Deduces version information purely from QR Code dimensions.</p>
        /// 
        /// </summary>
        /// <param name="dimension">dimension in modules
        /// </param>
        /// <returns> {@link Version} for a QR Code of that dimension
        /// </returns>
        /// <throws>  ReaderException if dimension is not 1 mod 4 </throws>
        public static Version getProvisionalVersionForDimension(int dimension)
        {
            if (dimension % 2 != 1)
            {
                throw ReaderException.Instance;
            }
            try
            {
                return getVersionForNumber((dimension - 9) / 2);
            }
            catch
            {
                throw ReaderException.Instance;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="versionNumber"></param>
        /// <returns></returns>
        public static Version getVersionForNumber(int versionNumber)
        {
            if (versionNumber < 1 || versionNumber > 4)
            {
                throw new System.ArgumentException();
            }
            return VERSIONS[versionNumber - 1];
        }

        /// <summary> See ISO 18004:2006 Annex E</summary>
        internal BitMatrix buildFunctionPattern()
        {
            int dimension = DimensionForVersion;
            BitMatrix bitMatrix = new BitMatrix(dimension);

            // Top left finder pattern + separator + format
            bitMatrix.setRegion(0, 0, 9, 9);

            // Vertical timing pattern
            bitMatrix.setRegion(9, 0, dimension - 9, 1);
            // Horizontal timing pattern
            bitMatrix.setRegion(0, 9, 1, dimension - 9);

            return bitMatrix;
        }

        /// <summary> <p>Encapsulates a set of error-correction blocks in one symbol version. Most versions will
        /// use blocks of differing sizes within one version, so, this encapsulates the parameters for
        /// each set of blocks. It also holds the number of error-correction codewords per block since it
        /// will be the same across all blocks within one version.</p>
        /// </summary>
        public sealed class ECBlocks
        {
            public int ECCodewordsPerBlock
            {
                get
                {
                    return ecCodewordsPerBlock;
                }
            }
            public int NumBlocks
            {
                get
                {
                    int total = 0;
                    for (int i = 0; i < ecBlocks.Length; i++)
                    {
                        total += ecBlocks[i].Count;
                    }
                    return total;
                }
            }
            public int TotalECCodewords
            {
                get
                {
                    return ecCodewordsPerBlock * NumBlocks;
                }
            }
            //UPGRADE_NOTE: Final was removed from the declaration of 'ecCodewordsPerBlock '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
            private int ecCodewordsPerBlock;
            //UPGRADE_NOTE: Final was removed from the declaration of 'ecBlocks '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
            private ECB[] ecBlocks;

            internal ECBlocks(int ecCodewordsPerBlock, ECB ecBlocks)
            {
                this.ecCodewordsPerBlock = ecCodewordsPerBlock;
                this.ecBlocks = new ECB[] { ecBlocks };
            }

            internal ECBlocks(int ecCodewordsPerBlock, ECB ecBlocks1, ECB ecBlocks2)
            {
                this.ecCodewordsPerBlock = ecCodewordsPerBlock;
                this.ecBlocks = new ECB[] { ecBlocks1, ecBlocks2 };
            }

            public ECB[] getECBlocks()
            {
                return ecBlocks;
            }
        }

        /// <summary> <p>Encapsualtes the parameters for one error-correction block in one symbol version.
        /// This includes the number of data codewords, and the number of times a block with these
        /// parameters is used consecutively in the QR code version's format.</p>
        /// </summary>
        public sealed class ECB
        {
            public int Count
            {
                get
                {
                    return count;
                }
            }
            public int DataCodewords
            {
                get
                {
                    return dataCodewords;
                }
            }
            //UPGRADE_NOTE: Final was removed from the declaration of 'count '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
            private int count;
            //UPGRADE_NOTE: Final was removed from the declaration of 'dataCodewords '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
            private int dataCodewords;

            internal ECB(int count, int dataCodewords)
            {
                this.count = count;
                this.dataCodewords = dataCodewords;
            }
        }

        public override System.String ToString()
        {
            return System.Convert.ToString(versionNumber);
        }

        /// <summary> See ISO 18004:2006 6.5.1 Table 9</summary>
        private static Version[] buildVersions()
        {
            return new Version[]{
                        new Version(1, new ECBlocks(2, new ECB(1, 3)),  //M1 5
                                                    null, 
                                                    null, 
                                                    null), 
                        new Version(2, new ECBlocks(5, new ECB(1, 5)), //M2 10
                                                    new ECBlocks(6, new ECB(1, 4)), 
                                                    null, 
                                                    null), 
                        new Version(3, new ECBlocks(6, new ECB(1, 11)), //M3 17
                                                    new ECBlocks(8, new ECB(1, 9)), 
                                                    null, 
                                                    null), 
                        new Version(4, new ECBlocks(8, new ECB(1, 16)), //M4 24
                                                    new ECBlocks(10, new ECB(1, 14)), 
                                                    new ECBlocks(14, new ECB(1, 10)), 
                                                    null)
            };
        }
    }
}