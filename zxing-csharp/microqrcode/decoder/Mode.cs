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
using System;

namespace com.google.zxing.microqrcode.decoder
{

    /// <summary> <p>
    /// See ISO 18004:2006, 6.4.1, Tables 2 and 3. This enum encapsulates the various modes in which
    /// data can be encoded to bits in the QR code standard.</p>
    /// </summary>
    /// <author>  Sean Owen
    /// </author>
    /// <author>www.Redivivus.in (suraj.supekar@redivivus.in) - Ported from ZXING Java Source 
    /// </author>
    public sealed class Mode
    {
        public int Bits
        {
            get
            {
                return bits;
            }
        }
        public System.String Name
        {
            get
            {
                return name;
            }
        }

        // No, we can't use an enum here. J2ME doesn't support it.

        //UPGRADE_NOTE: Final was removed from the declaration of 'TERMINATOR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        /// <summary>
        /// Tables 2
        /// M1  M2    M3      M4
        /// 000 00000 0000000 000000000 
        /// </summary>
        public static readonly Mode TERMINATOR = new Mode(new int[] { 0, 0, 0, 0 }, 0x00, new int[] { 3, 5, 7, 9 }, "TERMINATOR"); // Not really a mode...
        //UPGRADE_NOTE: Final was removed from the declaration of 'NUMERIC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        public static readonly Mode NUMERIC = new Mode(new int[] { 3, 4, 5, 6 }, 0x00, new int[] { 0, 1, 2, 3 }, "NUMERIC");
        //UPGRADE_NOTE: Final was removed from the declaration of 'ALPHANUMERIC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        public static readonly Mode ALPHANUMERIC = new Mode(new int[] { 0, 3, 4, 5 }, 0x01, new int[] { 0, 1, 2, 3 }, "ALPHANUMERIC");
        //UPGRADE_NOTE: Final was removed from the declaration of 'BYTE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        public static readonly Mode BYTE = new Mode(new int[] { 0, 0, 4, 5 }, 0x02, new int[] { 0, 1, 2, 3 }, "BYTE");
        //UPGRADE_NOTE: Final was removed from the declaration of 'KANJI '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        public static readonly Mode KANJI = new Mode(new int[] { 0, 0, 3, 4 }, 0x03, new int[] { 0, 1, 2, 3 }, "KANJI");

        //UPGRADE_NOTE: Final was removed from the declaration of 'characterCountBitsForVersions '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int[] characterCountBitsForVersions;
        //UPGRADE_NOTE: Final was removed from the declaration of 'bits '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private int bits;
        /// <summary>
        /// Micro QR Code 位长度
        /// </summary>
        /// <remarks>
        /// Micro QR Code 每种版本位长度一样
        /// Table 2 — Mode indicators for QR Code 2005 
        /// </remarks>
        private int[] bitsLength;
        //UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private System.String name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterCountBitsForVersions">
        /// Table 3 — Number of bits in character count indicator for QR Code 2005 
        /// </param>
        /// <param name="bits">
        /// Table 2 — Mode indicators for QR Code 2005 
        /// </param>
        /// <param name="name"></param>
        private Mode(int[] characterCountBitsForVersions, int bits, int[] bitsLength, System.String name)
        {
            this.characterCountBitsForVersions = characterCountBitsForVersions;
            this.bits = bits;
            this.bitsLength = bitsLength;
            this.name = name;
        }

        /// <param name="bits">
        /// four bits encoding a Micro QR Code data mode
        /// </param>
        /// <returns> {@link Mode} encoded by these bits
        /// </returns>
        /// <throws>  IllegalArgumentException if bits do not correspond to a known mode </throws>
        public static Mode forBits(int bits)
        {
            switch (bits)
            {
                //case 0x0: 
                //    return TERMINATOR;
                case 0x0:
                    return NUMERIC;
                case 0x1:
                    return ALPHANUMERIC;
                case 0x2:
                    return BYTE;
                case 0x3:
                    return KANJI;
                default:
                    throw new System.ArgumentException();
            }
        }

        /// <param name="version">
        /// version in question
        /// </param>
        /// <returns> 
        /// number of bits used, in this Micro QR Code symbol {@link Version}, to encode the
        /// count of characters that will follow encoded in this {@link Mode}
        /// </returns>
        public int getCharacterCountBits(Version version)
        {
            if (characterCountBitsForVersions == null)
                throw new System.ArgumentException("Character count doesn't apply to this mode");

            return characterCountBitsForVersions[version.VersionNumber - 1];
        }

        /// <param name="version">
        /// Mode Bits length
        /// </param>
        /// <returns> 
        /// current Mode Bits length
        /// </returns>
        /// <remarks>
        /// Table 2 — Mode indicators for QR Code 2005 
        /// </remarks>
        public int getBitsLength(Version version)
        {
            if (bitsLength == null)
                throw new System.ArgumentException("BitsLength Arrays is null");

            return bitsLength[version.VersionNumber - 1];
        }

        /// <param name="version">
        /// Mode Bits length
        /// </param>
        /// <returns> 
        /// current Mode Bits length
        /// </returns>
        /// <remarks>
        /// Table 2 — Mode indicators for QR Code 2005 
        /// </remarks>
        public int getBitsLength(int versionNum)
        {
            if (bitsLength == null)
                throw new System.ArgumentException("BitsLength Arrays is null");
            if (versionNum < 1 || versionNum > 4)
                throw new ArgumentOutOfRangeException("versionNum", "versionNum [1, 4]");

            return bitsLength[versionNum - 1];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override System.String ToString()
        {
            return name;
        }
    }
}