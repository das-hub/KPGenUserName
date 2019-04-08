using System;
using System.Collections.Generic;
using System.Drawing.Text;

namespace Core
{
    public static class Transliterations
    {
        private static readonly Dictionary<string, Transliteration> _transliterations;

        static Transliterations()
        {
            _transliterations = new Dictionary<string, Transliteration>
            {
                {Constants.CommandGostName, new GostTransliteration()},
                {Constants.CommandIsoName, new IsoTransliteration()}
            };
        }

        public static Transliteration Get(string name)
        {
            return _transliterations.ContainsKey(name) ? _transliterations[name] : null;
        }
    }

    public class Transliteration
    {
        protected Dictionary<char, string> FrontRule;
        protected Dictionary<string, char> BackRule;

        public string Front(string text)
        {
            string output = string.Empty;
            Dictionary<char, string> dict = FrontRule;
            foreach (char c in text) output += dict.ContainsKey(c) ? dict[c] : c.ToString();
            return output;
        }

        public string Back(string text)
        {
            int l = text.Length;
            string output = string.Empty;
            Dictionary<string, char> dict = BackRule;
            int i = 0;
            while (i < l)
            {
                string s = text.Substring(i, Math.Min(3, l - i));
                do
                {
                    if (dict.ContainsKey(s))
                    {
                        output += dict[s];
                        i += s.Length;
                        break;
                    }
                    s = s.Remove(s.Length - 1);
                } while (s.Length > 0);
                i += s.Length == 0 ? 3 : 0;
            }
            return output;
        }
    }

    //ГОСТ 16876-71
    public class GostTransliteration : Transliteration
    {
        public GostTransliteration()
        {
            FrontRule = new Dictionary<char, string>()
            {
                {'Є', "Eh"}, {'І', "I"}, {'і', "i"}, {'№', "#"}, {'є', "eh"}, {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"}, {'Е', "E"}, {'Ё', "Jo"},
                {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"}, {'Й', "JJ"}, {'К', "K"}, {'Л', "L"}, {'М', "M"}, {'Н', "N"}, {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"},
                {'Т', "T"}, {'У', "U"}, {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "C"}, {'Ч', "Ch"}, {'Ш', "Sh"}, {'Щ', "Shh"}, {'Ъ', "'"}, {'Ы', "Y"}, {'Ь', ""}, {'Э', "Eh"},
                {'Ю', "Yu"}, {'Я', "Ya"}, {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "jo"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
                {'й', "jj"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"}, {'ф', "f"},
                {'х', "kh"}, {'ц', "c"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shh"}, {'ъ', ""}, {'ы', "y"}, {'ь', ""}, {'э', "eh"}, {'ю', "yu"}, {'я', "ya"}, {'«', ""},
                { '»', ""}, {'—', "-"}, {' ', "_"}
            };
            BackRule = new Dictionary<string, char>();

            foreach (KeyValuePair<char, string> pair in FrontRule) BackRule[pair.Value] = pair.Key;
        }
    }

    //ISO 9-95
    public class IsoTransliteration : Transliteration
    {
        public IsoTransliteration()
        {
            FrontRule = new Dictionary<char, string>() {
            { 'Є', "Ye" }, { 'І', "I" }, { 'Ѓ', "G" }, { 'і', "i" }, { '№', "#" }, { 'є', "ye" }, { 'ѓ', "g" }, { 'А', "A" }, { 'Б', "B" }, { 'В', "V" }, { 'Г', "G" },
            { 'Д', "D" }, { 'Е', "E" }, { 'Ё', "Yo" }, { 'Ж', "Zh" }, { 'З', "Z" }, { 'И', "I" }, { 'Й', "J" }, { 'К', "K" }, { 'Л', "L" }, { 'М', "M" }, { 'Н', "N" },
            { 'О', "O" }, { 'П', "P" }, { 'Р', "R" }, { 'С', "S" }, { 'Т', "T" }, { 'У', "U" }, { 'Ф', "F" }, { 'Х', "X" }, { 'Ц', "C" }, { 'Ч', "Ch" }, { 'Ш', "Sh" },
            { 'Щ', "Shh" }, { 'Ъ', "'" }, { 'Ы', "Y" }, { 'Ь', "" }, { 'Э', "E" }, { 'Ю', "YU" }, { 'Я', "YA" }, { 'а', "a" }, { 'б', "b" }, { 'в', "v" }, { 'г', "g" },
            { 'д', "d" }, { 'е', "e" }, { 'ё', "yo" }, { 'ж', "zh" }, { 'з', "z" }, { 'и', "i" }, { 'й', "j" }, { 'к', "k" }, { 'л', "l" }, { 'м', "m" }, { 'н', "n" },
            { 'о', "o" }, { 'п', "p" }, { 'р', "r" }, { 'с', "s" }, { 'т', "t" }, { 'у', "u" }, { 'ф', "f" }, { 'х', "x" }, { 'ц', "c" }, { 'ч', "ch" }, { 'ш', "sh" },
            { 'щ', "shh" }, { 'ъ', "" }, { 'ы', "y" }, { 'ь', "" }, { 'э', "e" }, { 'ю', "yu" }, { 'я', "ya" }, { '«', "" }, { '»', "" }, { '—', "-" }, { ' ', "_" }};

            BackRule = new Dictionary<string, char>();

            foreach (KeyValuePair<char, string> pair in FrontRule) BackRule[pair.Value] = pair.Key;
        }
    }
}
