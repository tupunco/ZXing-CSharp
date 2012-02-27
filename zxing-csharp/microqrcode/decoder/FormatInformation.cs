using System.Collections.Generic;
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
using com.google.zxing.qrcode.decoder;

namespace com.google.zxing.microqrcode.decoder
{

    /// <summary> <p>Encapsulates a QR Code's format information, including the data mask used and
    /// error correction level.</p>
    /// 
    /// </summary>
    /// <author>  Sean Owen
    /// </author>
    /// <author>www.Redivivus.in (suraj.supekar@redivivus.in) - Ported from ZXING Java Source 
    /// </author>
    /// <seealso cref="DataMask">
    /// </seealso>
    /// <seealso cref="ErrorCorrectionLevel">
    /// </seealso>
    sealed class FormatInformation
    {
        /// <summary>
        /// 
        /// </summary>
        internal ErrorCorrectionLevel ErrorCorrectionLevel
        {
            get
            {
                return errorCorrectionLevel;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal Version Version
        {
            get { return version; }
        }
        /// <summary>
        /// 
        /// </summary>
        internal sbyte DataMask
        {
            get
            {
                return dataMask;
            }
        }

        private const int FORMAT_INFO_MASK_QR = 0x4445; //100010001000101

        /// <summary> See ISO 18004:2006, Annex C, Table C.1</summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'FORMAT_INFO_DECODE_LOOKUP'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private static readonly int[][] FORMAT_INFO_DECODE_LOOKUP = new int[][]{ 
                    new int[]{0x4445, 0x00}, new int[]{0x4172, 0x01}, new int[]{0x4E2B, 0x02}, new int[]{0x4B1C, 0x03}, 
                    new int[]{0x55AE, 0x04}, new int[]{0x5099, 0x05}, new int[]{0x5FC0, 0x06}, new int[]{0x5AF7, 0x07}, 
                    new int[]{0x6793, 0x08}, new int[]{0x62A4, 0x09}, new int[]{0x6DFD, 0x0A}, new int[]{0x68CA, 0x0B}, 
                    new int[]{0x7678, 0x0C}, new int[]{0x734F, 0x0D}, new int[]{0x7C16, 0x0E}, new int[]{0x7921, 0x0F}, 
                    new int[]{0x06DE, 0x10}, new int[]{0x03E9, 0x11}, new int[]{0x0CB0, 0x12}, new int[]{0x0987, 0x13}, 
                    new int[]{0x1735, 0x14}, new int[]{0x1202, 0x15}, new int[]{0x1D5B, 0x16}, new int[]{0x186C, 0x17}, 
                    new int[]{0x2508, 0x18}, new int[]{0x203F, 0x19}, new int[]{0x2F66, 0x1A}, new int[]{0x2A51, 0x1B}, 
                    new int[]{0x34E3, 0x1C}, new int[]{0x31D4, 0x1D}, new int[]{0x3E8D, 0x1E}, new int[]{0x3BBA, 0x1F}};

        /// <summary> Offset i holds the number of 1 bits in the binary representation of i</summary>
        //UPGRADE_NOTE: Final was removed from the declaration of 'BITS_SET_IN_HALF_BYTE'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private static readonly int[] BITS_SET_IN_HALF_BYTE = new int[] { 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4 };
        /// <summary>
        /// Micro QR Code Format information  Symbol numbers
        /// 0 M1 1 M2 2 M2 3 M3 4 M3 5 M4 6 M4 7 M4 
        /// ISO/IEC 18004:2006(E) Table 13 ¡ª Symbol numbers for Micro QR Code symbols 
        /// </summary>
        private static readonly KeyValuePair<int, ErrorCorrectionLevel>[] SYMBOL_NUMBERS_INFO = new KeyValuePair<int, ErrorCorrectionLevel>[] {
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(1, ErrorCorrectionLevel.L),  //0 {version, ECL}
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(2, ErrorCorrectionLevel.L),  //1
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(2, ErrorCorrectionLevel.M),  //2
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(3, ErrorCorrectionLevel.L),  //3
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(3, ErrorCorrectionLevel.M),  //4
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(4, ErrorCorrectionLevel.L),  //5
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(4, ErrorCorrectionLevel.M),  //6
                                                                            new KeyValuePair<int, ErrorCorrectionLevel>(4, ErrorCorrectionLevel.Q),  //7
                                                                        };
        //UPGRADE_NOTE: Final was removed from the declaration of 'errorCorrectionLevel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private ErrorCorrectionLevel errorCorrectionLevel;
        //UPGRADE_NOTE: Final was removed from the declaration of 'dataMask '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        private sbyte dataMask;
        private Version version;

        private FormatInformation(int formatInfo)
        {
            //ISO/IEC 18004:2006(E)  6.9 Format information 
            //Symbol number 0:         000 
            //Data mask pattern reference:       11 
            //Data bits (symbol number, data mask pattern reference):  00011 

            var p = SYMBOL_NUMBERS_INFO[(formatInfo >> 2) & 0x07];
            errorCorrectionLevel = p.Value;
            version = Version.getVersionForNumber(p.Key);
            dataMask = (sbyte)(formatInfo & 0x03);
        }

        internal static int numBitsDiffering(int a, int b)
        {
            a ^= b; // a now has a 1 bit exactly where its bit differs with b's
            // Count bits set quickly with a series of lookups:
            return BITS_SET_IN_HALF_BYTE[a & 0x0F]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 4) & 0x0F)]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 8) & 0x0F)]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 12) & 0x0F)]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 16) & 0x0F)]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 20) & 0x0F)]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 24) & 0x0F)]
                + BITS_SET_IN_HALF_BYTE[(SupportClass.URShift(a, 28) & 0x0F)];
        }

        /// <param name="maskedFormatInfo">format info indicator, with mask still applied
        /// </param>
        /// <returns> information about the format it specifies, or <code>null</code>
        /// if doesn't seem to match any known pattern
        /// </returns>
        internal static FormatInformation decodeFormatInformation(int maskedFormatInfo)
        {
            FormatInformation formatInfo = doDecodeFormatInformation(maskedFormatInfo);
            if (formatInfo != null)
            {
                return formatInfo;
            }
            // Should return null, but, some QR codes apparently
            // do not mask this info. Try again by actually masking the pattern
            // first
            return doDecodeFormatInformation(maskedFormatInfo ^ FORMAT_INFO_MASK_QR);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maskedFormatInfo"></param>
        /// <returns></returns>
        private static FormatInformation doDecodeFormatInformation(int maskedFormatInfo)
        {
            // Find the int in FORMAT_INFO_DECODE_LOOKUP with fewest bits differing
            int bestDifference = System.Int32.MaxValue;
            int bestFormatInfo = 0;

            for (int i = 0; i < FORMAT_INFO_DECODE_LOOKUP.Length; i++)
            {
                int[] decodeInfo = FORMAT_INFO_DECODE_LOOKUP[i];
                int targetInfo = decodeInfo[0];
                if (targetInfo == maskedFormatInfo)
                {
                    // Found an exact match
                    return new FormatInformation(decodeInfo[1]);
                }
                int bitsDifference = numBitsDiffering(maskedFormatInfo, targetInfo);
                if (bitsDifference < bestDifference)
                {
                    bestFormatInfo = decodeInfo[1];
                    bestDifference = bitsDifference;
                }
            }
            // Hamming distance of the 32 masked codes is 7, by construction, so <= 3 bits
            // differing means we found a match
            if (bestDifference <= 3)
            {
                return new FormatInformation(bestFormatInfo);
            }
            return null;
        }

        public override int GetHashCode()
        {
            return (errorCorrectionLevel.ordinal() << 3) | (int)dataMask;
        }

        public override bool Equals(System.Object o)
        {
            if (!(o is FormatInformation))
            {
                return false;
            }
            FormatInformation other = (FormatInformation)o;
            return this.errorCorrectionLevel == other.errorCorrectionLevel && this.dataMask == other.dataMask;
        }
    }
}