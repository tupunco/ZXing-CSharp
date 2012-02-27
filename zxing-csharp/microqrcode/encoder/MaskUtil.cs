/*
* Copyright 2008 ZXing authors
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
using ByteMatrix = com.google.zxing.common.ByteMatrix;

namespace com.google.zxing.microqrcode.encoder
{

    /// <author>  satorux@google.com (Satoru Takabayashi) - creator
    /// </author>
    /// <author>  dswitkin@google.com (Daniel Switkin) - ported from C++
    /// </author>
    /// <author>www.Redivivus.in (suraj.supekar@redivivus.in) - Ported from ZXING Java Source 
    /// </author>
    public sealed class MaskUtil
    {
        private MaskUtil()
        {
            // do nothing
        }

        // Apply mask penalty rule 1 and return the penalty. Find repetitive cells with the same color and
        // give penalty to them. Example: 00000 or 11111.
        public static int applyMaskPenaltyRule1(ByteMatrix matrix)
        {
            //For each data mask pattern in turn, count the number of dark modules in the right and lower edges of the 
            //symbol (excluding the final module of the timing pattern). The evaluation score is given by the following 
            //formula: 
            //
            //If SUM1 ¡Ü SUM2 
            //    Evaluation score = SUM1 ¡Á 16 + SUM2 
            //If SUM1 > SUM2 
            //    Evaluation score = SUM2 ¡Á 16 + SUM1 
            //where: 
            //    SUM1 = number of dark modules in right side edge  
            //    SUM2 = number of dark modules in lower side edge   //Horizontal
            var sum1 = applyMaskPenaltyRule1Internal(matrix, true);
            var sum2 = applyMaskPenaltyRule1Internal(matrix, false);
            if (sum1 <= sum2)
                return sum1 * 16 + sum2;
            else
                return sum2 * 16 + sum1;
        }

        // Return the mask bit for "getMaskPattern" at "x" and "y". See 8.8 of JISX0510:2004 for mask
        // pattern conditions.
        public static bool getDataMaskBit(int maskPattern, int x, int y)
        {
            if (!MicroQRCode.isValidMaskPattern(maskPattern))
            {
                throw new System.ArgumentException("Invalid mask pattern");
            }
            int intermediate, temp;
            switch (maskPattern)
            {
                //ISO/IEC 18004:2006(E)  6.8.1 Data mask patterns / Table 10 ¡ª Data mask pattern generation conditions 
                case 0:
                    intermediate = y & 0x1;
                    break;
                case 1:
                    intermediate = ((SupportClass.URShift(y, 1)) + (x / 3)) & 0x1;
                    break;
                case 2:
                    temp = y * x;
                    intermediate = (((temp & 0x1) + (temp % 3)) & 0x1);
                    break;
                case 3:
                    temp = y * x;
                    intermediate = (((temp % 3) + ((y + x) & 0x1)) & 0x1);
                    break;

                default:
                    throw new System.ArgumentException("Invalid mask pattern: " + maskPattern);

            }
            return intermediate == 0;
        }

        // Helper function for applyMaskPenaltyRule1. We need this for doing this calculation in both
        // vertical and horizontal orders respectively.
        private static int applyMaskPenaltyRule1Internal(ByteMatrix matrix, bool isHorizontal)
        {
            int penalty = 0;
            int len = matrix.Width - 1;
            for (int i = 1; i <= len; i++)
            {
                int bit = isHorizontal ? matrix.get_Renamed(i, len) : matrix.get_Renamed(len, i);
                if (bit == 1)
                    penalty++;
            }
            return penalty;
        }
    }
}