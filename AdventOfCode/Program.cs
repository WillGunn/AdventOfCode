using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            day8();

            System.Console.ReadKey();
        }

        static void day8()
        {


        }

        static void day7()
        {
            
            Dictionary<string, UInt16> cache = new Dictionary<string, UInt16>();
            string line;
            string[] delimiter = { " -> " };

            string find = "a";
            int loopnumber = 0;
            while (!cache.ContainsKey(find))
            {
                ++loopnumber;
                int linenumber = 0;
                StreamReader file = new StreamReader(@"7.txt");
                while ((line = file.ReadLine()) != null)
                {
                    ++linenumber;
                    var split = line.Split(delimiter,StringSplitOptions.None);
                    var equation = split[0].Split(' ');
                    if (!cache.ContainsKey(split[1]))
                    {
                        if (equation.Length == 1) //set value
                        {
                            UInt16 outval;

                            if (UInt16.TryParse(equation[0], out outval))
                            {
                                cache.Add(split[1], outval);
                            }
                            else if (cache.ContainsKey(equation[0]))
                            {
                                cache.Add(split[1], cache[equation[0]]);
                            }

                        }
                        else if (equation.Length == 2) //not
                        {
                            ushort outval;

                            if (UInt16.TryParse(equation[1], out outval))
                            {
                                cache.Add(split[1], (UInt16)(~outval));
                            }
                            else if (cache.ContainsKey(equation[1]))
                            {
                                cache.Add(split[1],(UInt16)(~cache[equation[1]]));
                            }
                        }
                        else if(equation.Length == 3)
                        {
                            string val1 = "";
                            string val2 = "";
                            UInt16 outval;
                            if(UInt16.TryParse(equation[0],out outval))
                            {
                                val1 = outval.ToString();
                            }
                            else if(cache.ContainsKey(equation[0]))
                            {
                                val1 = cache[equation[0]].ToString();
                            }

                            if (UInt16.TryParse(equation[2], out outval))
                            {
                                val2 = outval.ToString();
                            }
                            else if (cache.ContainsKey(equation[2]))
                            {
                                val2 = cache[equation[2]].ToString();
                            }

                            if(val1 != "" && val2 != "")
                            {
                                switch(equation[1])
                                {
                                    case "AND":
                                        cache.Add(split[1], (UInt16)(UInt16.Parse(val1) & UInt16.Parse(val2)));
                                        break;
                                    case "OR":
                                        cache.Add(split[1], (UInt16)(UInt16.Parse(val1) | UInt16.Parse(val2)));
                                        break;
                                    case "LSHIFT":
                                        cache.Add(split[1], (UInt16)(UInt16.Parse(val1) << UInt16.Parse(val2)));
                                        break;
                                    case "RSHIFT":
                                        cache.Add(split[1], (UInt16)(UInt16.Parse(val1) >> UInt16.Parse(val2)));
                                        break;
                                }
                            }
                            
                        }
                        
                    }//if cache doesn't contain key

                }
            }
            System.Console.WriteLine($"{find}={cache[find]}");
        }

        static void day6()
        {
            StreamReader file = new StreamReader(@"6.txt");
            string line;
            Day6 day6 = new Day6();
            while ((line = file.ReadLine()) != null)
            {
                var sarray = line.Split(" ".ToArray<char>());
                string coord1 = sarray[sarray.Length - 3];
                var c1 = coord1.Split(',');
                string coord2 = sarray[sarray.Length - 1];
                var c2 = coord2.Split(',');
                if (sarray[0] == "toggle")
                {
                    Console.WriteLine($"Toggle: {coord1},{coord2}");
                    day6.toggle(Int32.Parse(c1[0]), Int32.Parse(c1[1]), Int32.Parse(c2[0]), Int32.Parse(c2[1]));
                }
                else if (sarray[1] == "on")
                {
                    Console.WriteLine($"Turn On: {coord1},{coord2}");
                    day6.turnon(Int32.Parse(c1[0]), Int32.Parse(c1[1]), Int32.Parse(c2[0]), Int32.Parse(c2[1]));
                }
                else if (sarray[1] == "off")
                {
                    Console.WriteLine($"Turn Off: {coord1},{coord2}");
                    day6.turnoff(Int32.Parse(c1[0]), Int32.Parse(c1[1]), Int32.Parse(c2[0]), Int32.Parse(c2[1]));
                }



            }

            System.Console.WriteLine(day6.counton());


        }

        static void day5()
        {
            int count = 0;
            StreamReader file = new StreamReader(@"5.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                if(!line.Contains("ab") && !line.Contains("cd") && !line.Contains("pq") && !line.Contains("xy"))
                {
                    if(line.Count(f => f == 'a')+ line.Count(f => f == 'e')+ line.Count(f => f == 'i')+ line.Count(f => f == 'o')+ line.Count(f => f == 'u')>=3)
                    {
                        if(Day5_CheckIfDoubleLetter(line))
                        {
                            ++count;
                        }

                    }
                }
            }
            System.Console.WriteLine(count);
        }

        static Boolean Day5_CheckIfDoubleLetter(string s)
        {
            char prev = (char)0;
            foreach(char c in s)
            {
                if(c==prev)
                {
                    return true;
                }
                prev = c;
            }
            return false;
        }

        static void day4()
        {
            decimal i = 0;
            MD5Cng md5 = new MD5Cng();
            md5.Initialize();
            string hash;
            while (true)
            {
                hash = ByteArrayToString(md5.ComputeHash(Encoding.ASCII.GetBytes("iwrupvqb"+i.ToString())));

                if(hash.StartsWith("00000"))
                {
                    break;
                }
                ++i;
            }
            System.Console.WriteLine(hash+" | "+i);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        static void day3()
        {
            int x = 0;
            int y = 0;
            
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            var filestream = new FileStream(@"3.txt", FileMode.Open, FileAccess.Read);
            while (filestream.Position < filestream.Length)
            {
                char c = (char)filestream.ReadByte();
                switch (c)
                {
                    case '^':
                        ++y;
                        break;
                    case 'v':
                        --y;
                        break;
                    case '>':
                        ++x;
                        break;
                    case '<':
                        --x;
                        break;
                }
                string location = $"{x},{y}";
                if(dictionary.ContainsKey(location))
                {
                    dictionary[location] += 1;
                }
                else
                {
                    dictionary.Add(location, 1);
                }

            }

            System.Console.WriteLine(dictionary.Count());

        }

        static void day2()
        {
            int sqft = 0;
            StreamReader file = new StreamReader(@"2.txt");
            string line = "";
            List<Day2> day2list = new List<Day2>();
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("x"))
                    day2list.Add(new Day2(line));
            }
            foreach (Day2 box in day2list)
            {
                sqft += box.getPaperNeeds();
            }

            Console.WriteLine(sqft);
        }

        static void day1()
        {
            int floor = 0;
            var filestream = new FileStream(@"1.txt", FileMode.Open, FileAccess.Read);
            while (filestream.Position < filestream.Length)
            {
                char c = (char)filestream.ReadByte();
                if (c == '(')
                    ++floor;
                else if (c == ')')
                    --floor;
            }

            System.Console.WriteLine(floor);
        }
    }

    class Day6
    {
        static int size = 1000;
        bool[,] array = new bool[size, size];

        public Day6()
        {

        }

        public void turnon(int x1, int y1, int x2, int y2)
        {
            int ysave = y1;
            for(;x1 <= x2;++x1)
            {
                for(y1=ysave;y1 <= y2;++y1)
                {
                    array[x1,y1] = true;
                }
            }
        }
        public void turnoff(int x1, int y1, int x2, int y2)
        {
            int ysave = y1;
            for (; x1 <= x2; ++x1)
            {
                for (y1=ysave; y1 <= y2; ++y1)
                {
                    array[x1, y1] = false;
                }
            }

        }
        public void toggle(int x1, int y1, int x2, int y2)
        {
            int ysave = y1;
            for (; x1 <= x2; ++x1)
            {
                for (y1=ysave; y1 <= y2; ++y1)
                {
                    array[x1, y1]=!array[x1,y1];
                }
            }


        }

        public int counton()
        {
            int count = 0;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if(array[i, j])
                    {
                        ++count;
                    }
                }
            }
            return count;
        }

    }

    class Day2
    {
        int length;
        int width;
        int height;

        public Day2(int l, int w, int h)
        {
            length = l;
            width = w;
            height = h;
        }
        public Day2(string s)
        {
            var split = s.Split('x');
            length = Int32.Parse(split[0]);
            width = Int32.Parse(split[1]);
            height = Int32.Parse(split[2]);
        }

        public int getPaperNeeds()
        {
            List<int> list = new List<int>();
            int sqft = 0;

            list.Add(length);
            list.Add(width);
            list.Add(height);
            list.Sort();



            sqft = (2 * length * width) + (2 * length * height) + (2 * width * height);
            sqft += (list[0] * list[1]);
            return sqft;
        }
    }
}
