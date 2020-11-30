using System.Collections;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 题号：316. 去除重复字母
/// 题目：
/// 给你一个字符串 s ，请你去除字符串中重复的字母，使得每个字母只出现一次。需保证 返回结果的字典序最小（要求不能打乱其他字符的相对位置）。
/// 输入：s = "bcabc"
/// 输出："abc"
/// 示例 2：
/// 输入：s = "cbacdcbc"
/// 输出："acdb"
/// 提示：
/// 1 <= s.length <= 104
/// s 由小写英文字母组成
/// </summary>
/// 单调栈
public class Solution
{
    public string RemoveDuplicateLetters(string s)
    {
        int n = s.Length;
        if (n <= 1) return s;

        Dictionary<char, int> Hash = new Dictionary<char, int>();
        Stack<char> temp = new Stack<char>();
        for (int i = 0; i < n; i++)
        {
            if (!Hash.ContainsKey(s[i]))
            {
                Hash.Add(s[i],1);
            }
            else
            {
                Hash[s[i]]++;
            }
        }
        bool[] used = new bool[26];
        
        for (int i = 0; i < n; i++)
        {
            if (!used[s[i] - 'a'])
            {
                //当后面还有这个字符，且这个字符比后一个大的时候就能丢掉
                while (temp.Count != 0 && temp.Peek() >= s[i] && Hash[temp.Peek()] > 0)
                {
                    used[temp.Peek() - 'a'] = false;
                    temp.Pop();
                }
                temp.Push(s[i]);
                Hash[s[i]]--;
                used[s[i] - 'a'] = true;
            }
            else
            {
                Hash[s[i]]--;
            }
        }

        char[] result = new char[temp.Count];
        for (int i = temp.Count - 1; i >= 0; i--)
        {
            result[i] = temp.Pop();
        }
        return String.Join("", result);
    }
}

/// <summary>
/// 题号：452. 用最少数量的箭引爆气球
/// 题目：
/// 在二维空间中有许多球形的气球。对于每个气球，提供的输入是水平方向上，气球直径的开始和结束坐标。由于它是水平的，所以纵坐标并不重要，因此只要知道开始和结束的横坐标就足够了。开始坐标总是小于结束坐标。
/// 一支弓箭可以沿着 x 轴从不同点完全垂直地射出。在坐标 x 处射出一支箭，若有一个气球的直径的开始和结束坐标为 xstart，xend， 且满足  xstart ≤ x ≤ xend，则该气球会被引爆。可以射出的弓箭的数量没有限制。 弓箭一旦被射出之后，可以无限地前进。我们想找到使得所有气球全部被引爆，所需的弓箭的最小数量。
/// 给你一个数组 points ，其中 points [i] = [xstart, xend] ，返回引爆所有气球所必须射出的最小弓箭数。
/// 示例 1：
/// 输入：points = [[10, 16],[2,8],[1,6],[7,12]]
/// 输出：2
/// 解释：对于该样例，x = 6 可以射爆[2, 8],[1,6] 两个气球，以及 x = 11 射爆另外两个气球
/// 示例 2：
/// 输入：points = [[1,2],[3,4],[5,6],[7,8]]
/// 输出：4
/// 示例 3：
/// 输入：points = [[1, 2],[2,3],[3,4],[4,5]]
/// 输出：2
/// 示例 4：
/// 输入：points = [[1, 2]]
/// 输出：1
/// 示例 5：
/// 输入：points = [[2, 3],[2,3]]
/// 输出：1
/// 提示：
/// 0 <= points.length <= 104
/// points[i].length == 2
/// - 231 <= xstart < xend <= 231 - 1
/// </summary>
public class Solution
{
    public int FindMinArrowShots(int[][] points)
    {
        int n = points.Length;
        if (n <= 1) return n;
        Array.Sort(points, (a, b) => a[0] > b[0] ? 1 : -1);
        List<int[]> result = new List<int[]>();
        int[] temp = new int[2];
        temp = points[0];
        for (int i = 1; i < n; i++)
        {
            //后一个无交集
            if (points[i][0] > temp[1])
            {
                result.Add(temp);
                temp = points[i];
                if (i == n - 1)
                {
                    result.Add(points[i]);
                }
            }
            else
            {
                //更新左边界，右边界，取交集
                temp[0] = points[i][0];
                temp[1] = Math.Min(points[i][1], temp[1]);
                if (i == n - 1)
                {
                    result.Add(temp);
                }
            }
        }
        return result.Count();
    }
}

/// <summary>
/// 题号：767. 重构字符串
/// 题目：
/// 给定一个字符串S，检查是否能重新排布其中的字母，使得两相邻的字符不同。
/// 若可行，输出任意可行的结果。若不可行，返回空字符串。
/// 示例 1:
/// 输入: S = "aab"
/// 输出: "aba"
/// 示例 2:
/// 输入: S = "aaab"
/// 输出: ""
/// 注意:
/// S 只包含小写字母并且长度在[1, 500]区间内。
/// </summary>
public class Solution
public class Solution
{
    public string ReorganizeString(string S)
    {
        int n = S.Length;
        if (n < 2) return S;
        Dictionary<char, int> Hash = new Dictionary<char, int>();
        int MaxNum = 0;
        for (int i = 0; i < n; i++)
        {
            if (!Hash.ContainsKey(S[i])) Hash.Add(S[i], 1);
            else Hash[S[i]]++;
            MaxNum = Math.Max(MaxNum, Hash[S[i]]);
        }

        //剪枝
        //超过一半肯定不对
        if (MaxNum > (n + 1) / 2) return "";

        int[][] temp = new int[Hash.Count][];

        int index = 0;
        foreach (char c in Hash.Keys)
        {
            temp[index] = new int[2];
            temp[index][0] = c - 'a';
            temp[index][1] = Hash[c];
            index++;
        }
        string result = "";

        //模拟最大堆
        Array.Sort(temp, (a, b) => b[1] - a[1]);
        while (temp[0][1] > 0)
        {
            Array.Sort(temp, (a, b) => b[1] - a[1]);
            if (temp[0][1] > 0 && temp[1][1] > 0)
            {
                result += (char)(temp[0][0] + 'a');
                result += (char)(temp[1][0] + 'a');
                temp[0][1]--;
                temp[1][1]--;
            }
            else if (temp[0][1] > 0)
            {
                result += (char)(temp[0][0] + 'a');
                temp[0][1]--;
            }
            else
            {
                continue;
            }
            Array.Sort(temp, (a, b) => b[1] - a[1]);
        }
        return result;
    }
}