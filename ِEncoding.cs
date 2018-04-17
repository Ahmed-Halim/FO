using System;
namespace e
{
    class Program
    {
        public static string encode(string text)
        {
            int start = 0, Length = text.Length;
            string ans = "";
            for (int counter = 0; counter < Length; counter++)
            {
                if (text[start] != text[counter])
                {
                    ans += (counter - start > 1) ? "*" + text[start].ToString() + (counter - start).ToString() : text[start].ToString();
                    start = counter;
                }
            }

            ans += (Length - start > 1) ? "*" + text[start].ToString() + (Length - start).ToString(): text[start].ToString();
            
            return ans;
        }

        public static string decode(string text)
        {
            string ans = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '*')
                {
                    ans += new string(text[i + 1], text[i + 2] - '0');
                    i += 2;
                }
                else
                {
                    ans += text[i];
                }
            }
            return ans;
        }

        public static void Main(string[] args)
        {
            Console.Write("Enter text to be encoded: ");
            String text = Console.ReadLine();
            String encoded = encode(text);
            String decoded = decode(encoded);
            Console.WriteLine("Encoded: " + encoded);
            Console.WriteLine("Decoded: " + decoded);
        }
    }
}
