#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("hmmYkzI4ca32roIXxGelT7BlSuQunKqVh9QoE56mmVRRYm7mVDfkaYMxspGDvrW6mTX7NUS+srKytrOwmmyBAEOHxwmi61nRqXRT3ZNu8y8xsryzgzGyubExsrKzHfTQm11NVxbJln5OdeSpJoIVulsfWgRiFasXczxAPG0Wj0tN30NrPcgHYjYb87Nfi1+enuloxdhYYGYj2TPzMkFjVNLkW//ADPWHOK425CnvSTyBd1XIEL+6mubE18cVlnCfACRgLn+JZuvqND7ApbFofSqk6BaE95oEUv7JYDRQeqE85wzVrqJZwzU9EDZ6DQURDOSfj0Zi7tk0mkrLmlM2vPJjVNJqMOBNmSWVRabSQQ9CfpTbYaWCs5tZH1FA0PrxkrGwsrOy");
        private static int[] order = new int[] { 8,5,8,7,5,8,13,8,11,12,11,13,12,13,14 };
        private static int key = 179;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
