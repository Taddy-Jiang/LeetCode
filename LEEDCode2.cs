using System.Collections;
using System;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 题号：204. 计数质数
/// 题目：
/// 统计所有小于非负整数 n 的质数的数量。
/// 示例 1：
/// 输入：n = 10
/// 输出：4
/// 解释：小于 10 的质数一共有 4 个, 它们是 2, 3, 5, 7 。
/// 示例 2：
/// 输入：n = 0
/// 输出：0
/// 示例 3：
/// 输入：n = 1
/// 输出：0
/// 提示：
/// 0 <= n <= 5 * 106
/// </summary>
/// 法一：暴力
public class Solution
{
    public int CountPrimes(int n)
    {
        if (n < 2) return 0;

        int result = 0;
        //暴力：每次看当前数i与n的关系，如果n/i也是数，则为合数
        for (int i = 2; i < n; i++)
        {
            result += isPrimes(i) ? 1 : 0;
        }
        return result;
    }

    private bool isPrimes(int n)
    {
        for (int i = 2; i * i <= n; i++)
        {
            //n为i的整数倍
            if (n % i == 0)
            {
                //为合数
                return false;
            }
        }
        return true;
    }
}

/// 法二：埃氏筛
/// 每次遍历一个质数，它的倍数肯定为合数
public class Solution
{
    public int CountPrimes(int n)
    {
        if (n < 2) return 0;

        int result = 0;
        //设定一个数组，设定初始全为质数（false）
        bool[] isNotPrimes = new bool[n];
        for (int i = 2; i < n; i++)
        {
            if (!isNotPrimes[i])
            {
                result++;
                //把所有倍数设定为合数
                //从i*i开始即可
                if ((long)i * i < n)
                {
                    for (int j = i * i; j < n; j += i)
                    {
                        isNotPrimes[j] = true;
                    }
                }
            }
        }
        return result;
    }
}

/// <summary>
/// 题号：290. 单词规律
/// 题目：
/// 给定一种规律 pattern 和一个字符串 str ，判断 str 是否遵循相同的规律。
/// 这里的 遵循 指完全匹配，例如， pattern 里的每个字母和字符串 str 中的每个非空单词之间存在着双向连接的对应规律。
/// 示例1:
/// 输入: pattern = "abba", str = "dog cat cat dog"
/// 输出: true
/// 示例 2:
/// 输入: pattern = "abba", str = "dog cat cat fish"
/// 输出: false
/// 示例 3:
/// 输入: pattern = "aaaa", str = "dog cat cat dog"
/// 输出: false
/// 示例 4:
/// 输入: pattern = "abba", str = "dog dog dog dog"
/// 输出: false
/// 说明:
/// 你可以假设 pattern 只包含小写字母， str 包含了由单个空格分隔的小写字母。 
/// </summary>
public class Solution
{
    public bool WordPattern(string pattern, string s)
    {
        if (s.Length == 0)
        {
            if (pattern.Length == 0) return true;
            else return false;
        }
        string[] temp = s.Split(" ");
        if (pattern.Length != temp.Length) return false;
        Dictionary<string, char> Hash = new Dictionary<string, char>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (Hash.ContainsKey(temp[i]))
            {
                if (pattern[i] != Hash[temp[i]]) return false;
            }
            else
            {
                Hash.Add(temp[i], pattern[i]);
            }
        }
        HashSet<char> Hashtemp = new HashSet<char>();
        foreach (char var in Hash.Values)
        {
            if (Hashtemp.Contains(var)) return false;
            else Hashtemp.Add(var);
        }
        return true;
    }
}

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
/// 题号：321. 拼接最大数
/// 题目：
/// 给定长度分别为 m 和 n 的两个数组，其元素由 0-9 构成，表示两个自然数各位上的数字。现在从这两个数组中选出 k (k <= m + n) 个数字拼接成一个新的数，要求从同一个数组中取出的数字保持其在原数组中的相对顺序。
/// 求满足该条件的最大数。结果返回一个表示该最大数的长度为 k 的数组。
/// 说明: 请尽可能地优化你算法的时间和空间复杂度。
/// 示例 1:
/// 输入:
/// nums1 = [3, 4, 6, 5]
/// nums2 = [9, 1, 2, 5, 8, 3]
/// k = 5
/// 输出:
/// [9, 8, 6, 5, 3]
/// 示例 2:
/// 输入:
/// nums1 = [6, 7]
/// nums2 = [6, 0, 4]
/// k = 5
/// 输出:
/// [6, 7, 6, 0, 4]
/// 示例 3:
/// 输入:
/// nums1 = [3, 9]
/// nums2 = [8, 9]
/// k = 3
/// 输出:
/// [9, 8, 9]
/// </summary>
/// 分别求单调栈
/// 单调栈
public class Solution
{
    public int[] MaxNumber(int[] nums1, int[] nums2, int k)
    {
        if (k == 0) return new int[0];
        int[] result = new int[k];
        int m = nums1.Length;
        int n = nums2.Length;
        int end = Math.Min(m, k);
        for (int i = 0; i <= end; i++)
        {
            if (n >= k - i)
            {
                int[] num1 = MaxNum(nums1, i);
                int[] num2 = MaxNum(nums2, k - i);
                int[] temp = Merge(num1, num2);
                if (Compare(result, temp, 0, 0))
                {
                    result = temp;
                }
            }
        }
        return result;
    }

    //求数组中k个最大数
    //单调栈
    public int[] MaxNum(int[] num, int k)
    {
        if (k == 0) return new int[0];
        int n = num.Length;
        //去除的个数
        int nlen = n - k;
        Stack<int> result = new Stack<int>();
        for (int i = 0; i < n; i++)
        {
            while (result.Count != 0 && result.Peek() < num[i] && nlen > 0)
            {
                result.Pop();
                nlen--;
            }
            result.Push(num[i]);
        }
        while (nlen > 0)
        {
            result.Pop();
            nlen--;
        }
        int[] temp = new int[k];
        for (int i = k - 1; i >= 0; i--)
        {
            temp[i] = result.Pop();
        }
        return temp;
    }

    //合并数组
    public int[] Merge(int[] nums1, int[] nums2)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        int[] result = new int[m + n];
        int i = 0;
        int j = 0;
        int index = 0;
        while (i < m && j < n)
        {
            if (nums1[i] < nums2[j])
            {
                result[index] = nums2[j];
                j++;
            }
            else if (nums1[i] > nums2[j])
            {
                result[index] = nums1[i];
                i++;
            }
            else
            {
                //相等时要分情况
                //不能只比较自身，还要看剩下的情况
                if (Compare(nums1, nums2, i, j))
                {
                    result[index] = nums2[j];
                    j++;
                }
                else
                {
                    result[index] = nums1[i];
                    i++;
                }
            }
            index++;
        }
        while (i < m)
        {
            result[index++] = nums1[i++];
        }
        while (j < n)
        {
            result[index++] = nums2[j++];
        }
        return result;
    }

    //比较两个数组大小
    public bool Compare(int[] nums1, int[] nums2, int i, int j)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        while (i < m && j < n)
        {
            if (nums1[i] > nums2[j])
            {
                return false;
            }
            else if (nums1[i] < nums2[j])
            {
                return true;
            }
            else
            {
                i++;
                j++;
            }
        }
        return (m - i) - (n - j) > 0 ? false : true;
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

/// 题号：454. 四数相加 II
/// 题目：
/// 给定四个包含整数的数组列表 A , B , C , D ,计算有多少个元组 (i, j, k, l) ，使得 A[i] + B[j] + C[k] + D[l] = 0。
/// 为了使问题简单化，所有的 A, B, C, D 具有相同的长度 N，且 0 ≤ N ≤ 500 。所有整数的范围在 -228 到 228 - 1 之间，最终结果不会超过 231 - 1 。
/// 例如:
/// 输入:
/// A = [1, 2]
/// B = [-2, -1]
/// C = [-1, 2]
/// D = [0, 2]
/// 输出:
/// 2
/// 解释:
/// 两个元组如下:
/// 1. (0, 0, 0, 1)->A[0] + B[0] + C[0] + D[1] = 1 + (-2) + (-1) + 2 = 0
/// 2. (1, 1, 0, 0)->A[1] + B[1] + C[0] + D[0] = 2 + (-1) + (-1) + 0 = 0
/// </summary>
public class Solution
{
    public int FourSumCount(int[] A, int[] B, int[] C, int[] D)
    {
        int N = A.Length;
        if (N == 0) return 0;
        Dictionary<int, int> sum1 = new Dictionary<int, int>();
        Dictionary<int, int> sum2 = new Dictionary<int, int>();

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                int num1 = A[i] + B[j];
                int num2 = C[i] + D[j];
                if (!sum1.ContainsKey(num1)) sum1.Add(num1, 1);
                else sum1[num1]++;
                if (!sum2.ContainsKey(num2)) sum2.Add(num2, 1);
                else sum2[num2]++;
            }
        }
        int result = 0;
        foreach (int num in sum1.Keys)
        {
            if (sum2.ContainsKey(0 - num)) result += sum1[num] * sum2[0 - num];
        }
        return result;
    }
}