using System;
namespace FO {
    class Encoding {
        
        public static string encode(string text) {
            int start = 0, Length = text.Length;
            string ans = "";
            for (int counter = 0; counter < Length; counter++) {
                if (text[start] != text[counter]) {
                    ans += (counter - start > 1) ? "*" + (counter - start).ToString() + text[start] : text[start];
                    start = counter;
                }
            }
            ans += (Length - start > 1) ? "*" + (Length - start).ToString() + text[start] : text[start];
            return ans;
        }

        public static string decode(string text) {
            string ans = "";
            for (int i = 0; i < text.Length; i++) {
                if (text[i] == '*') {
                    ans += new string(text[i + 2], text[i + 1] - '0');
                    i += 2;
                } else {
                    ans += text[i];
                }
            }
            return ans;
        }

        public static void Main(string[] args) {
            Console.Write("Enter text to be encoded: ");
            String text = Console.ReadLine();
            String encoded = encode(text);
            String decoded = decode(encoded);
            Console.WriteLine("Encoded: " + encoded);
            Console.WriteLine("Decoded: " + decoded); 
        }
    }
}