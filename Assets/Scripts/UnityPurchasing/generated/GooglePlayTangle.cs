// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("XMgYS9dV59RmUivr6cwepx3vdwDo/tHQRhrZboA2qVgKUgWDzoRlsoOpaFEBKlqYcPQe0cKTluKd0X+o8hv9JZABYuCJsiW3yCh+1homxLQHZez5gWXMI91v5Av4JpvToiDMDMd19tXH+vH+3XG/cQD69vb28vf0Zym18hfCoEHMsy698UcJdkd1Lyoh4QBGRWOILN648cUINsUVLzaMrn2QKBer8yA7Of1Hm/RxZvWpbQbpD5hUgey0n2PNyq02FTnOp/uDDN919vj3x3X2/fV19vb3PWOfm4A7x/b1cJKoeBXPwe+WB5v2qkgy1e6jZI2ZIULcK1FHD0K9SDOEjgSmQ9FQDMh4pVOzUJmgZ9Xh/CSS7PLc779t7sjEAAIKBPX09vf2");
        private static int[] order = new int[] { 13,4,7,7,5,13,8,7,9,9,13,13,12,13,14 };
        private static int key = 247;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
