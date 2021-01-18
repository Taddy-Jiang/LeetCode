using System;
using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 题号：2
/// 题目：
/// 给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。
/// 如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。
/// 您可以假设除了数字 0 之外，这两个数都不会以 0 开头。
/// 输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
/// 输出：7-> 0-> 8
/// 原因：342 + 465 = 807
/// 
/// 链表
/// 注意点：如果链表长度不一样，需要补全
/// 每一项需要加上前一项的进位
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        int val = 0;   //进位
        ListNode resultNode = new ListNode(0);   //结果的链表首相赋值为0防止意外
        ListNode tempNode = resultNode;         //临时借用一个链表代替原链表，因为如果直接用源链表的话，最后返回结果应该为源链表后续的字段，不全。
        while (l1 != null || l2 != null || val != 0)
        {
            //当l1，l2不为空链表时，且进位不为空时运行
            val = val + (l1 == null ? 0 : l1.val) + (l2 == null ? 0 : l2.val);
            //当前位的值为进位值+l1当前位值+l2当前位值
            tempNode.next = new ListNode(val % 10);   //当前位值应该为此时计算的值的余数
            tempNode = tempNode.next;   //向后移一位
            val = val / 10;     //确定进位为多少
            l1 = (l1 == null ? null : l1.next);    //l1链表为空时赋值为空，否则移下一位
            l2 = (l2 == null ? null : l2.next);
        }
        return resultNode.next;     //虽然使用tempNode代替，但指向的地址没有变化，所以resultNode开始为第一项0，但后续地址都有
    }
}

/// <summary>
/// 题号：3
/// 题目：
/// 给定一个字符串，请你找出其中不含有重复字符的 最长子串 的长度。
/// 输入: "pwwkew"
/// 输出: 3
/// 解释: 因为无重复字符的最长子串是 "wke"，所以其长度为 3。
/// 请注意，你的答案必须是 子串 的长度，"pwke" 是一个子序列，不是子串。
/// 
/// 滑动窗口的方法
/// 例如abca
/// 开始以a为头，abc，
/// 以b为头，bca
/// 每次都是头一个去掉后一个插进去
/// 依次往后滑动
/// 
/// C#考虑队列的方法
/// </summary>
public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        int max = 0;
        Queue<char> queue = new Queue<char>();
        //使用队列的方法模拟滑动窗口
        foreach(char c in s)
        {
            while (queue.Contains(c))
            {
                queue.Dequeue();//如果存在了，则先把首相去掉
            }
            queue.Enqueue(c);  //没有重复就加进去
            if (queue.Count > max)
            {
                max = queue.Count;   //如果长度更长则更新
            }
        }
        return max;
    }
}

/// <summary>
/// 题号：4
/// 题目：
/// 给定两个大小为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。
/// 请你找出这两个正序数组的中位数，并且要求算法的时间复杂度为 O(log(m + n))。
/// 你可以假设 nums1 和 nums2 不会同时为空。
/// 输入：nums1 = [1, 2]     nums2 = [3, 4]
/// 则中位数是(2 + 3) / 2 = 2.5
/// 
/// 求中位数
/// 方法：第k小数
/// 每次比较每组数前k/2，小的那个去掉
/// 具体见leedcode
/// 注：如果其中有一组全去掉了，则选另一组第k个
/// </summary>
public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        //取第k小数，k为中间
        int m = nums1.Length;
        int n = nums2.Length;
        int length = m + n;   //总长度
        //总长为奇
        int k = (length+1) / 2;    //例5个数，中间数为3  此时k = 3，即num[3-1]为中间数
        //总长度为偶  
        //则为第k个数和k+1数之和
        //例 6个数，此时 k = (6+1)/2=3，即(num[3-1]+num[4-1])/2为中位数

        //分奇偶情况讨论
        if (length%2 == 0)
        {
            //为偶时
            return (GetKth(nums1, 0, m - 1, nums2, 0, n - 1, k) + GetKth(nums1, 0, m - 1, nums2, 0, n - 1, k + 1)) * 0.5;   //如果只是/2的话会整，必须为*0.5
        }
        else
        {
            //为奇时
            return GetKth(nums1, 0, m - 1, nums2, 0, n - 1, k);
        }
    }

    //取第k个数
    private int GetKth(int[] nums1, int start1, int end1, int[] nums2, int start2, int end2, int k)
    {
        //不能使用nums.length 因为长度一直在变
        int m = end1 - start1 + 1;
        int n = end2 - start2 + 1;

        //保证nums1永远最长，所以即使2没有了肯定从1取
        if (m < n)
        {
            return GetKth(nums2,start2,end2,nums1,start1,end1,k);    //交换两者位置
        }

        if (n == 0)
        {
            return nums1[start1 + k - 1];    //即使为偶数，返回第k个数，上面在判断奇偶时会另外计算k+1的。
        }

        if (k == 1)
        {
            return Math.Min(nums1[start1],nums2[start2]);
        }

        //取前k/2进行比较 如果有一个数组没有这么多了，就有多少用多少
        //所以start开始变化
        int i = start1 + Math.Min(m, k / 2) - 1;
        int j = start2 + Math.Min(n, k / 2) - 1;
        if (nums1[i] > nums2[j])
        {
            return GetKth(nums1, start1, end1, nums2, j+1, end2, k - Math.Min(n, k / 2));
        }
        else
        {
            return GetKth(nums1, i+1, end1, nums2, start2, end2, k - Math.Min(m, k / 2));
        }
    }
}

/// 题号：5
/// 题目：
/// 给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。
/// 输入: "cbbd"
/// 输出: "bb"
/// 
/// 回文子串
/// 对于每一个首相都判断，当中间一个相等时，如果左边与右边都相等时，必然是一个回文子串
/// 需要判断s的长度为奇还是偶
/// 
/// 补：马拉车算法
/// 对于处理后的字符串 t , 我们用数组 p 来保存对应的回文半径。
/// 例如下图中，p[5]=6,即当 c=5时，p=6 , 对应的字符串为 t[0,10], 即 "#c#b#c#b#c#" . 分析可得，去掉#后的回文串 "cbcbc" 长度为5，正好等于p[5]-1。
/// 所以根据 p数组第 i 项 p[i] 的值 , 我们可以得到以 i 为中心的最长回文串 在去掉#后 的 长度为 p[i]-1 ；
/// 再进一步，我们不仅可以求得长度，还可以求得它在原字符串中 的 起始index值为 ( i - ( p[i] - 1 ) ) / 2 ,这个可以用下图中的p[5]=6 与 p[7]=4 来验证。
/// </summary>
public class Solution
{
    string result = "";


    //字符串每个空格都含#则肯定变为奇数，详见leedcode对应题目
    private string PreProcess(string s)
    {
        string t = "";  //保存含#号的字符串
        int n = s.Length;
        if (n==0)
        {
            return "";
        }
        for (int i = 0; i < n; i++)
        {
            t = t + "#" + s[i];
        }
        t = t + "#";
        return t;
    }

    //计算回文子串
    public void LongestString(string s,string t, int start, int end,int mid)
    {
        int len = 0;
        //注意此时是含#号的字符串
        while (start >= 0 && end < t.Length && t[start] == t[end])
        {
            start--;
            end++;
        }
        len = end - start - 1;  //长度为这个
        //因为当1-3是回文时，会继续移动，此时start=0，edn=4，长度为4-0-1=3

        int sub = (len + 1) / 2 - 1;  //在含#号的字符串中，原回文串长度为现回文半径-1
                                      //回文半径为（len+1）/2
        if (sub > result.Length)
        {
            //在含#号的字符串中，原回文串长度为现回文半径-1
            //回文半径为（len+1）/2
            //首相为 ( i - ( p[i] - 1 ) ) / 2 
            //即(i-sub)/2
            result = s.Substring((mid-sub)/2 , sub);
        }
    }

    public string LongestPalindrome(string s)
    {
        string t = PreProcess(s);
        for (int i = 0; i < t.Length; i++)
        {
            LongestString(s, t, i, i, i);    //含#号的字符串肯定是奇数
        }
        return result;
    }
}

/// 题号：6
/// 题目：
/// 将一个给定字符串根据给定的行数，以从上往下、从左到右进行 Z 字形排列。
/// 比如输入字符串为 "LEETCODEISHIRING" 行数为 3 时，排列如下：
/// 
/// L   C   I   R
/// E T O E S I I G
/// E   D   H   N
/// 
/// 之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："LCIRETOESIIGEDHN"。
/// 
/// 输入: s = "LEETCODEISHIRING", numRows = 3
/// 输出: "LCIRETOESIIGEDHN"
/// 
/// 模拟z字变形
/// 用一个数组来表示行数
/// </summary>
public class Solution
{
    public string Convert(string s, int numRows)
    {
        int num = Math.Min(numRows , s.Length);  //如果s的长度比numRows小，则返回长度

        //如果就1行，则直接返回
        if (num == 1)
        {
            return s;
        }

        string[] numZ = new string[num];         //建立Z字形数组
        int m = 0;                               //Z字形数组对应序号
        bool goingdown = false;                   //移动方向  初始向上

        string result = "";

        //每次遇到0,2n-3时向下移动    例如3行时，第1个数（0）向下，第5个数（4）向下  因为一个周期为n+n-2
        //遇n-1向上
        //所以只要m每次遇到0向下，遇到n-1向上，在途中改变m加减即可

        for (int i = 0; i < s.Length; i++)
        {
            numZ[m] += s[i];
            if (m == 0 || m == num - 1) goingdown = !goingdown;   //方向改变

            m += goingdown ? 1 : -1;     //序号改变
        }

        for (int i = 0; i < num; i++)
        {
            result += numZ[i];
        }
        return result;
    }
}

/// <summary>
/// 题号：7  整数反转
/// 题目：
/// 给出一个 32 位的有符号整数，你需要将这个整数中每位上的数字进行反转。
/// 输入: 123
/// 输出: 321
/// 注意:
/// 假设我们的环境只能存储得下 32 位的有符号整数，则其数值范围为[−231, 231 − 1]。请根据这个假设，如果反转后整数溢出那么就返回 0。
/// 
/// 防止溢出
/// 不使用字符串
/// </summary>
public class Solution
{
    public int Reverse(int x)
    {
        int result = 0;
        while (x != 0)
        {
            int pop = x % 10;   //确定最后一位
            //注意：取余只是取最后一位，即使为负数
            //例：-123%10 = -3
            x /= 10;
            if (result > Int32.MaxValue/10 || (result == Int32.MaxValue / 10 && pop > 7))
            {
                return 0;
            }
            if (result < Int32.MinValue/10 || (result == Int32.MinValue / 10 && pop < -8))
            {
                return 0;
            }

            result = result * 10 + pop;
        }

        return result;
    }
}

/// <summary>
/// 题号：10
/// 题目：
/// 给你一个字符串 s 和一个字符规律 p，请你来实现一个支持 '.' 和 '*' 的正则表达式匹配。
/// '.' 匹配任意单个字符
/// '*' 匹配零个或多个前面的那一个元素
/// 所谓匹配，是要涵盖 整个 字符串 s的，而不是部分字符串。
/// 
/// 说明:
/// s 可能为空，且只包含从 a-z 的小写字母。
/// p 可能为空，且只包含从 a-z 的小写字母，以及字符 . 和 *。
/// 输入:s = "aab"
/// p = "c*a*b"
/// 输出: true
/// 解释: 因为 '*' 表示零个或多个，这里 'c' 为 0 个, 'a' 被重复一次。因此可以匹配字符串 "aab"。
/// 
/// 动态规划
/// 从最后一位向前匹配
/// 结果见官方解答
/// </summary>
public class Solution
{
    public bool IsMatch(string s, string p)
    {
        int m = s.Length;
        int n = p.Length;
        
        return(Match(" " + s, " " + p, m, n));  //将s与p前多加一个空格，来比较原来的首相间的关系

    }

    private bool Match(string s, string p, int i, int j )
    {
        if (p[j] == ' ' && s[i] == ' ')
        {
            //s、p都为空串，肯定匹配。
            return true;
        }
        else if (p[j] == ' ' && s[i] != ' ')
        {
            //p为空串，s不为空串，肯定不匹配。
            return false;
        }
        else if (p[j] != ' ' && s[i] == ' ')
        {
            //s为空串，但p不为空串，要想匹配，只可能是右端是星号，它干掉一个字符后，把 p 变为空串。
            if (p[j] == '*' && p[j-1] != '*')
            {
                //此时不一定首项，需要继续判断
                return (Match(s,p,i,j-2));
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (p[1] == '*')
            {
                //注意前面加了个空格
                return false;
            }
            else
            {
                if (p[j] != '*')
                {
                    if (s[i] == p[j] || p[j] == '.')
                    {
                        return(Match(s, p, i - 1, j - 1));
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (s[i] == p[j - 1] || p[j - 1] == '.')
                    {
                        return(Match(s, p, i - 1, j) || Match(s, p, i, j - 2));
                    }
                    else
                    {
                        return(Match(s, p, i, j - 2));
                    }
                }
            }
        }
    }
}

/// <summary>
/// 题号：9. 回文数
/// 题目：
/// 判断一个整数是否是回文数。回文数是指正序（从左向右）和倒序（从右向左）读都是一样的整数。
/// 输入: 121
/// 输出: true
/// 输入: -121
/// 输出: false
/// 解释: 从左向右读, 为 -121 。 从右向左读, 为 121- 。因此它不是一个回文数。
/// 
/// 
/// </summary>
public class Solution
{
    public bool IsPalindrome(int x)
    {
        if (x < 0)
        {
            return false;
        }

        int temp1 = x;
        int temp2 = 0;

        while (temp1 / 10 != 0)
        {
            int n = temp1 % 10;
            temp2 = temp2 * 10 + n;
            temp1 = temp1 / 10;
        }
        
        temp2 = temp2 * 10 + temp1;

        return temp2 == x;
    }
}

/// <summary>
/// 题号：11
/// 题目：
/// 给你 n 个非负整数 a1，a2，...，an，每个数代表坐标中的一个点 (i, ai) 。
/// 在坐标内画 n 条垂直线，垂直线 i 的两个端点分别为 (i, ai) 和 (i, 0)。找出其中的两条线，使得它们与 x 轴共同构成的容器可以容纳最多的水。
/// 说明：你不能倾斜容器，且 n 的值至少为 2。
/// 图中垂直线代表输入数组 [1,8,6,2,5,4,8,3,7]。在此情况下，容器能够容纳水（表示为蓝色部分）的最大值为 49。
/// 输入:输入：[1,8,6,2,5,4,8,3,7]
/// 输出：49
/// 
/// 双指针方法，左右向中间移动
/// </summary>
public class Solution
{
    public int MaxArea(int[] height)
    {
        int result = 0;  //容量

        //双指针分别指向两端
        int left = 0;
        int right = height.Length - 1;

        while (left < right)
        {
            //向中间移动，直到两个指针重合    
            int temp = (right - left) * Math.Min(height[left], height[right]);   //长*高

            //把较小的高去掉
            //因为向中间推进的时候，长变短了，只有短的去掉才可能大
            result = Math.Max(result, temp);

            if (height[left] <= height[right])
            {
                ++left;
            }
            else
            {
                --right;
            }
        }

        return result;
    }
}

/// <summary>
/// 题号：14. 最长公共前缀
/// 题目：
/// 编写一个函数来查找字符串数组中的最长公共前缀。
/// 如果不存在公共前缀，返回空字符串 ""。
/// 示例 1:
/// 输入:["flower","flow","flight"]
/// 输出: "fl"
/// 示例 2:
/// 输入:["dog","racecar","car"]
/// 输出: ""
/// 解释: 输入不存在公共前缀。
/// </summary>
public class Solution
{
    public string LongestCommonPrefix(string[] strs)
    {
        string result = "";
        if (strs.Length == 0)
        {
            return result;
        }
        int num = strs[0].Length; //找出最短字符串
        for (int i = 1; i < strs.Length; i++)
        {
            num = Math.Min(num, strs[i].Length);
        }
        char temp;
        int numtemp = 0;
        while (numtemp < num)
        {
            temp = strs[0][numtemp];
            for (int i = 1; i < strs.Length; i++)
            {
                if (strs[i][numtemp] != temp)
                {
                    return result;
                }
            }
            result += temp;
            numtemp++;
        }
        return result;
    }
}

/// <summary>
/// 题号：15
/// 题目：
/// 给你一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，使得 a + b + c = 0 ？请你找出所有满足条件且不重复的三元组。
/// 注意：答案中不可以包含重复的三元组。
/// 给定数组 nums = [-1, 0, 1, 2, -1, -4]，
/// 满足要求的三元组集合为：[ [-1, 0, 1],[-1, -1, 2]]
/// 
/// 双指针方法
/// 确定一个，另外两个左右指针
/// 先排序，如果和>0则去掉右边……
/// </summary>
/// 
public class Solution
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        IList<IList<int>> result = new List<IList<int>>();

        int len = nums.Length;

        if (len < 3)
        {
            //return result;
        }
        else
        {
            Array.Sort(nums);   //从小到大排

            for (int i = 0; i < len - 2; i++)
            {
                //确定最左边的一个数
                //找另外两个

                if (nums[i] > 0)
                {
                    //如果最左边大于0的话，则和肯定大于0
                    break;
                }

                if (i > 0 && nums[i] == nums[i - 1])
                {
                    //去重
                    //如果首项已经出现过了则跳过
                    continue;
                }

                //左右指针
                int left = i + 1;
                int right = len - 1;

                while (left < right)
                {
                    int sum = nums[left] + nums[i] + nums[right];

                    if (sum == 0)
                    {
                        result.Add(new List<int>() { nums[i], nums[left], nums[right] });
                        while (left < right && nums[left] == nums[left + 1])
                        {
                            //去重
                            left++;
                        }
                        while (left < right && nums[right] == nums[right - 1])
                        {
                            //去重
                            right--;
                        }
                        left++;
                        right--;
                    }
                    else if (sum > 0)
                    {
                        right--;
                    }
                    else if (sum < 0)
                    {
                        left++;
                    }

                }
                //return result;
            }
        }
        return result;
    }
}

/// <summary>
/// 题号：17
/// 题目：
/// 给定一个仅包含数字 2-9 的字符串，返回所有它能表示的字母组合。
/// 给出数字到字母的映射如下（与电话按键相同）。注意 1 不对应任何字母。
/// 输入："23"
/// 输出：["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
/// 
/// 递归
/// </summary>
public class Solution
{
    public IList<string> LetterCombinations(string digits)
    {
        IList<string> result = new List<string>();

        int i = 0;

        //从前往后加
        while (i < digits.Length)
        {
            result = Add(result, Choose(digits[i]));
            i++;
        }

        return result;
    }

    //根据字符返回对应的值
    private IList<string> Choose(char c)
    {
        IList<string> temp = new List<string>();

        switch (c)
        {
            case '2': 
                temp.Add("a");
                temp.Add("b");
                temp.Add("c");
                break;
            case '3':
                temp.Add("d");
                temp.Add("e");
                temp.Add("f");
                break;
            case '4':
                temp.Add("g");
                temp.Add("h");
                temp.Add("i");
                break;
            case '5':
                temp.Add("j");
                temp.Add("k");
                temp.Add("l");
                break;
            case '6':
                temp.Add("m");
                temp.Add("n");
                temp.Add("o");
                break;
            case '7':
                temp.Add("p");
                temp.Add("q");
                temp.Add("r");
                temp.Add("s");
                break;
            case '8':
                temp.Add("t");
                temp.Add("u");
                temp.Add("v");
                break;
            case '9':
                temp.Add("w");
                temp.Add("x");
                temp.Add("y");
                temp.Add("z");
                break;
            default:
                break;
        }

        return temp;
    }

    //将两组值进行合并
    private IList<string> Add(IList<string> string1 , IList<string> string2)
    {
        IList<string> temp = new List<string>();

        if (string1.Count == 0)
        {
            return string2;
        }

        if (string2.Count == 0)
        {
            return string1;
        }

        foreach (string i in string1)
        {
            foreach (string j in string2)
            {
                temp.Add(i+j);
            }
        }

        return temp;
    }
}

/// <summary>
/// 题号：18. 四数之和
/// 题目：
/// 给定一个包含 n 个整数的数组 nums 和一个目标值 target，判断 nums 中是否存在四个元素 a，b，c 和 d ，使得 a + b + c + d 的值与 target 相等？找出所有满足条件且不重复的四元组。
/// 注意：
/// 答案中不可以包含重复的四元组。
/// 示例：
/// 给定数组 nums = [1, 0, -1, 0, -2, 2]，和 target = 0。
/// 满足要求的四元组集合为：
/// [
/// [-1,  0, 0, 1],
/// [-2, -1, 1, 2],
/// [-2,  0, 0, 2]
/// ]
/// 
/// 递归
/// 剪枝
/// </summary>
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        int n = nums.Length;
        if (n == 0) return result;
        Array.Sort(nums);
        List<int> temp = new List<int>();
        Sum(nums, target, temp, 0);
        return result;
    }

    public void Sum(int[] nums, int target, List<int> temp, int n)
    {
        int m = nums.Length;

        if (temp.Count == 4 && target == 0)
        {
            result.Add(new List<int>(temp));
            return;
        }

        if (n == m) return;

        for (int i = n; i < m; i++)
        {
            //剪枝
            if ((m - i) < (4 - temp.Count)) return;
            if (i < m - 1 && nums[i] + (3 - temp.Count) * nums[i + 1] > target) //剪枝
                return;
            if (i < m - 1 && nums[i] + (3 - temp.Count) * nums[m - 1] < target) //剪枝
                continue;
            if (i > n && nums[i] == nums[i - 1])
            {
                continue;
            }
            else
            {
                //去重
                temp.Add(nums[i]);
                Sum(nums, target - nums[i], temp, i + 1);
                temp.RemoveAt(temp.Count - 1);
            }
        }
    }
}

/// <summary>
/// 题号：19
/// 题目：
/// 给定一个链表，删除链表的倒数第 n 个节点，并且返回链表的头结点。
/// 示例：
/// 给定一个链表: 1->2->3->4->5, 和 n = 2.
/// 当删除了倒数第二个节点后，链表变为 1->2->3->5.
/// 说明：
/// 给定的 n 保证是有效的。
/// 进阶：你能尝试使用一趟扫描实现吗？
/// 
/// 双指针：辅助判断位置
/// 在头节点前添加一个哑节点可以有效帮助链表删除
/// 防止删除头节点
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        //在头节点前加一个哑节点
        ListNode first = new ListNode(0);
        first.next = head;

        //双指针
        ListNode p = first;
        ListNode q = first;

        for (int i = 0; i <= n; i++)
        {
            q = q.next;
        }

        while (q != null)
        {
            p = p.next;
            q = q.next;
        }

        p.next = p.next.next;

        return first.next;
    }
}

/// <summary>
/// 题号：20
/// 题目：
/// 给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串，判断字符串是否有效。
/// 有效字符串需满足：
/// 左括号必须用相同类型的右括号闭合。
/// 左括号必须以正确的顺序闭合。
/// 注意空字符串可被认为是有效字符串。
///
/// 输入: "()[]{}"
/// 输出: true
/// 输入: "([)]"
/// 输出: false
/// 
/// 栈：先入后出
/// </summary>
public class Solution
{
    public bool IsValid(string s)
    {
        int len = s.Length;

        if (len % 2 == 1)
        {
            //如果是奇数不需要计算
            return false;
        }

        //新建字典，使左右括号对应
        Dictionary<char, char> valuePairs = new Dictionary<char, char>();
        valuePairs.Add(')', '(');
        valuePairs.Add('}', '{');
        valuePairs.Add(']', '[');

        //使用栈来计算
        Stack<char> value = new Stack<char>();

        for (int i = 0; i < len; i++)
        {
            if (s[i] == '(' || s[i] == '[' || s[i] == '{')
            {
                //左边括号就添加
                value.Push(s[i]);
            }
            else
            {
                //如果是右边括号的话
                //如果栈里面有值，且含这个有括号，且对应的左边括号相等，则去掉栈最上面一个
                if (value.Count != 0 && valuePairs.ContainsKey(s[i]) && valuePairs[s[i]] == value.Peek())
                {
                    value.Pop();
                }
                else
                {
                    return false;
                }
            }
        }

        if (value.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

/// <summary>
/// 题号：21 合并两个有序链表
/// 题目：
/// 将两个升序链表合并为一个新的 升序 链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 
/// 示例：
/// 输入：1->2->4, 1->3->4
/// 输出：1->1->2->3->4->4
/// 
/// 双指针
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        ListNode result = new ListNode(0);
        ListNode temp = result;   //辅助链表，保证result永远在最前面的位置

        //当两者都不为0时
        while(l1 != null && l2 != null)
        {
            if (l1.val < l2.val)
            {
                temp.next = new ListNode(l1.val);
                temp = temp.next;
                l1 = l1.next;
            }
            else
            {
                temp.next = new ListNode(l2.val);
                temp = temp.next;
                l2 = l2.next;
            }
        }

        temp.next = l1 == null ? l2 : l1;

        return result.next;
    }
}

/// <summary>
/// 题号：22  括号生成
/// 题目：
/// 数字 n 代表生成括号的对数，请你设计一个函数，用于能够生成所有可能的并且 有效的 括号组合。
/// 示例：
/// 输入：n = 3
/// 输出：[
/// "((()))",
/// "(()())",
/// "(())()",
/// "()(())",
/// "()()()"
/// ]
/// 
/// dfs 深度优先搜索加回溯
/// 当左括号与右括号数都小于n时，可以添加左括号或右括号
/// 但当左括号数小于右括号时不能加右括号
/// 因为有一个左括号必须有一个右括号
/// </summary>
public class Solution
{
    IList<string> result = new List<string>();

    public IList<string> GenerateParenthesis(int n)
    {
        if (n == 0)
        {
            return result;
        }

        Add("", result, 0, 0, n);
        return result;
    }

    ///left:左括号有多少
    ///right：右括号有多少
    ///resultTemp：当前字符串
    private void Add(string resultTemp, IList<string> result, int left, int right, int n)
    {
        if (left == n && right == n)
        {
            result.Add(resultTemp);
            return;
        }

        //使用剪枝
        //当右括号比左括号多时就剪枝
        if (left < right)
        {
            return;
        }
        
        //其他情况下正常
        if (left < n)
        {
            Add(resultTemp + "(", result, left + 1, right, n);
        }
        if (right < n)
        {
            Add(resultTemp + ")", result, left, right + 1, n);
        }
    }
}

/// <summary>
/// 题号：23. 合并K个升序链表
/// 题目：
/// 给你一个链表数组，每个链表都已经按升序排列。
/// 请你将所有链表合并到一个升序链表中，返回合并后的链表。
/// 输入：lists = [[1,4,5],[1,3,4],[2,6]]
/// 输出：[1,1,2,3,4,4,5,6]
/// 
/// k指针
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode MergeKLists(ListNode[] lists)
    {
        //不使用分治的话
        /*ListNode result = null;

        for (int i = 0; i < lists.Length; i++)
        {
            result = Merge(result, lists[i]);
        }

        return result;*/

        //使用分治
        return Fen(lists, 0, lists.Length-1);
    }

    //合并
    private ListNode Merge(ListNode list1, ListNode list2)
    {
        ListNode tempresult = new ListNode(0);
        ListNode temp = tempresult;

        while (list1 != null && list2 != null)
        {
            if (list1.val <= list2.val)
            {
                temp.next = list1;
                temp = temp.next;
                list1 = list1.next;
            }
            else
            {
                temp.next = list2;
                temp = temp.next;
                list2 = list2.next;
            }
        }
        
        temp.next = list1 == null ? list2 : list1;

        return tempresult.next;
    }

    //分治
    private ListNode Fen(ListNode[] lists, int i, int j)
    {
        if (i == j)
        {
            return lists[i];
        }

        if (i > j)
        {
            return null;
        }

        int mid = (i + j) / 2;

        return Merge(Fen(lists, i, mid), Fen(lists, mid + 1, j));
    }
}

/// <summary>
/// 题号：24. 两两交换链表中的节点
/// 题目：
/// 给定一个链表，两两交换其中相邻的节点，并返回交换后的链表。
/// 你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
/// 示例:
/// 给定 1->2->3->4, 你应该返回 2->1->4->3.
/// 
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode SwapPairs(ListNode head)
    {
        ListNode result = new ListNode(0);
        ListNode temp = result;

        while (head != null)
        {
            if (head.next == null)
            {
                temp.next = head;
                head = head.next;
            }
            else
            {
                ListNode temp2 = head.next.next;
                temp.next = head.next;
                temp = temp.next;
                head.next = null;
                temp.next = head;
                temp = temp.next;
                head = temp2;
            }
        }
        return result.next;
    }
}

public class Solution
{
    public ListNode SwapPairs(ListNode head)
    {
        ListNode result = new ListNode(0);
        ListNode temp = result;

        while (head != null)
        {
            if (head.next == null)
            {
                temp.next = head;
                head = head.next;
            }
            else
            {
                ListNode temp2 = temp.next;
                temp.next = head;
                head = head.next;
                temp.next.next = temp2;
                temp2 = temp.next;
                temp.next = head;
                head = head.next;
                temp.next.next = temp2;
                temp = temp.next.next;
            }
        }
        return result.next;
    }
}

/// <summary>
/// 25.K 个一组翻转链表
/// 题目：
/// 给你一个链表，每 k 个节点一组进行翻转，请你返回翻转后的链表。
/// k 是一个正整数，它的值小于或等于链表的长度。
/// 如果节点总数不是 k 的整数倍，那么请将最后剩余的节点保持原有顺序。
/// 示例：
/// 给你这个链表：1->2->3->4->5
/// 当 k = 2 时，应当返回: 2->1->4->3->5
/// 当 k = 3 时，应当返回: 3->2->1->4->5
/// 说明：
/// 你的算法只能使用常数的额外空间。
/// 你不能只是单纯的改变节点内部的值，而是需要实际进行节点交换。
/// 
/// 链表
/// 双指针
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null) return head;
        ListNode result = new ListNode(0);
        ListNode temp = result;
        ListNode p = head;   //每k个首位置值
        ListNode q = head;   //交换位置值

        while (p != null)
        {
            for (int i = 0; i < k; i++)
            {
                if (p == null)
                {
                    temp.next = q;
                    return result.next;
                }
                p = p.next;
            }
            //每k项进行交换
            //依次插入
            for (int i = 0; i < k; i++)
            {
                ListNode temp2 = temp.next;
                temp.next = q;
                q = q.next;
                temp.next.next = temp2;
            }
            for (int i = 0; i < k; i++)
            {
                temp = temp.next;
            }
            temp.next = q;
        }

        return result.next;
    }
}

/// <summary>
/// 27. 移除元素
/// 题目：
/// 给你一个数组 nums 和一个值 val，你需要 原地 移除所有数值等于 val 的元素，并返回移除后数组的新长度。
/// 不要使用额外的数组空间，你必须仅使用 O(1) 额外空间并 原地 修改输入数组。
/// 元素的顺序可以改变。你不需要考虑数组中超出新长度后面的元素。
/// 给定 nums = [0,1,2,2,3,0,4,2], val = 2,
/// 函数应该返回新的长度 5, 并且 nums 中的前五个元素为 0, 1, 3, 0, 4。
/// 注意这五个元素可为任意顺序。
/// 你不需要考虑数组中超出新长度后面的元素。
/// 
/// 双指针
/// </summary>
public class Solution
{
    public int RemoveElement(int[] nums, int val)
    {
        int n = nums.Length;
        if (n == 0)
        {
            return 0;
        }

        Array.Sort(nums);
        int i = 0;
        int j = 0;
        while (nums[i] != val && i < n)
        {
            if (i == n - 1)
            {
                return n;
            }
            i++;
            j++;
        }

        while (nums[j] == val && j < n)
        {
            if (j == n - 1)
            {
                return i;
            }
            j++;
        }

        int m = j - i;
        while (j < n)
        {
            nums[i] = nums[j];
            i++;
            j++;
        }

        return n - m;
    }
}

//官方大佬
public int removeElement(int[] nums, int val)
{
    int i = 0;
    for (int j = 0; j < nums.Length; j++)
    {
        if (nums[j] != val)
        {
            nums[i] = nums[j];
            i++;
        }
    }
    return i;
}

/// <summary>
/// 30. 串联所有单词的子串
/// 题目：
/// 给定一个字符串 s 和一些长度相同的单词 words。找出 s 中恰好可以由 words 中所有单词串联形成的子串的起始位置。
/// 注意子串要与 words 中的单词完全匹配，中间不能有其他字符，但不需要考虑 words 中单词串联的顺序。
/// 示例 1：
/// 输入：
/// s = "barfoothefoobarman",
/// words = ["foo", "bar"]
/// 输出：[0,9]
/// 解释：
/// 从索引 0 和 9 开始的子串分别是 "barfoo" 和 "foobar" 。
/// 输出的顺序不重要, [9,0] 也是有效答案。
/// 示例 2：
/// 输入：
/// s = "wordgoodgoodgoodbestword",
/// words = ["word", "good", "best", "word"]
/// 输出：[]
/// </summary>
public class Solution
{
    public IList<int> FindSubstring(string s, string[] words)
    {
        IList<int> result = new List<int>();
        int slen = s.Length;
        if (slen == 0) return result;
        //总个数
        int wordslen = words.Length;
        if (wordslen == 0) return result;
        //单位单词长度
        int wordlen = words[0].Length;
        //总长度
        int wordsumlen = wordslen * wordlen;

        //原始的HashSet
        Dictionary<string, int> origin = new Dictionary<string, int>();
        //比较的HashSet
        Dictionary<string, int> temp = new Dictionary<string, int>();

        foreach (string word in words)
        {
            if (origin.ContainsKey(word))
            {
                origin[word]++;
            }
            else
            {
                origin.Add(word, 1);
            }
        }

        //最少要wordsumlen长度才行
        for (int i = 0; i <= slen - wordsumlen; i++)
        {
            //初始化
            temp = new Dictionary<string, int>(origin);
            //已经使用的个数
            int m = 0;
            int j = i;
            string stemp;
            //先取出对应几个
            while (m < wordslen)
            {

                stemp = s.Substring(j, wordlen);
                //如果不含对应值肯定不对
                if (!temp.ContainsKey(stemp))
                {
                    break;
                }
                //已经全部使用光了，肯定不对
                if (temp[stemp] == 0)
                {
                    break;
                }
                temp[stemp]--;
                j += wordlen;
                m++;
            }
            if (m == wordslen)
            {
                result.Add(i);
            }
        }

        return result;
    }
}

/// <summary>
/// 31. 下一个排列
/// 题目：
/// 实现获取下一个排列的函数，算法需要将给定数字序列重新排列成字典序中下一个更大的排列。
/// 如果不存在下一个更大的排列，则将数字重新排列成最小的排列（即升序排列）。
/// 必须原地修改，只允许使用额外常数空间。
/// 以下是一些例子，输入位于左侧列，其相应输出位于右侧列。
/// 1,2,3 → 1,3,2
/// 3,2,1 → 1,2,3
/// 1,1,5 → 1,5,1
/// 
/// 从后往前递推
/// </summary>
public class Solution
{
    public void NextPermutation(int[] nums)
    {
        int i = nums.Length - 2;  //倒数第二个数
        int j = nums.Length - 1;  //判断移动哪一位

        while (i>=0 && nums[i] >= nums[i+1])
        {
            i--;
        }

        //可能只有一个数字
        if (i < 0)
        {
            //nums = Reverse(nums, 0, nums.Length-1);
            Array.Sort(nums, 0, nums.Length);
        }
        else
        {
            while (j > i && nums[j] <= nums[i])
            {
                j--;
            }

            nums = Swap(nums, i, j);
            //nums = Reverse(nums, i + 1, nums.Length - 1);
            Array.Sort(nums, i + 1, nums.Length - i - 1);
        }  
    }

    //逆序排列
    private int[] Reverse(int[] nums, int start, int end)
    {
        while (start < end)
        {
            nums = Swap(nums, start, end);
            start++;
            end--;
        }
        return nums;
    }

    //交换第i位与第j位
    private int[] Swap(int[] nums, int i, int j)
    {
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
        return nums;
    }
}

/// <summary>
/// 题号：32. 最长有效括号
/// 题目：
/// 给定一个只包含 '(' 和 ')' 的字符串，找出最长的包含有效括号的子串的长度。
/// 示例 1:
/// 输入: "(()"
/// 输出: 2
/// 解释: 最长有效括号子串为 "()"
/// 
/// 双向遍历
/// </summary>

public int LongestValidParentheses(string s)
{
    //正反各进行一次遍历
    //取两次比较的最大值就是正常的最大值
    //只正向遍历的话可能用到（（）这种情况。
    int Parentheses = 0;
    int result = 0;
    int temp = 0;
    int zheng = 0;
    int fan = 0;

    for (int i = 0; i < s.Length; i++)
    {
        if (s[i] == '(')
        {
            Parentheses++;
        }
        if (s[i] == ')')
        {
            if (Parentheses > 0)
            {
                Parentheses--;
                temp += 2;
            }
            else
            {
                temp = 0;
            }
        }

        //只有左右相等时才更新，否则不更新
        //缺的部分反向遍历解决
        if (Parentheses == 0)
        {
            zheng = Math.Max(zheng, temp);
        }

    }

    Parentheses = 0;
    temp = 0;

    for (int i = s.Length - 1; i >= 0; i--)
    {
        if (s[i] == ')')
        {
            Parentheses++;
        }
        if (s[i] == '(')
        {
            if (Parentheses > 0)
            {
                Parentheses--;
                temp += 2;
            }
            else
            {
                temp = 0;
            }
        }
        if (Parentheses == 0)
        {
            fan = Math.Max(fan, temp);
        }
    }

    result = Math.Max(zheng, fan);

    return result;
}

/// <summary>
/// 题号：33. 搜索旋转排序数组
/// 题目：
/// 假设按照升序排序的数组在预先未知的某个点上进行了旋转。
/// (例如，数组[0, 1, 2, 4, 5, 6, 7] 可能变为[4, 5, 6, 7, 0, 1, 2] )。
/// 搜索一个给定的目标值，如果数组中存在这个目标值，则返回它的索引，否则返回 - 1 。
/// 你可以假设数组中不存在重复的元素。
/// 你的算法时间复杂度必须是 O(log n) 级别。
/// 示例 1:
/// 输入: nums = [4, 5, 6, 7, 0, 1, 2], target = 0
/// 输出: 4
/// 
/// 分治法
/// 直接判断是否处于顺序的阶段
/// </summary>
public class Solution
{
    public int Search(int[] nums, int target)
    {
        if (nums.Length == 0) return -1;
        return Fen(nums, 0, nums.Length - 1, target);
    }

    private int Fen(int[] nums, int i, int j, int target)
    {
        if (i > j)
        {
            return -1;
        }

        int mid = (i + j) / 2;
        if (nums[mid] == target)
        {
            return mid;
        }
        
        if (nums[i] <= nums[mid])
        {
            if (target >= nums[i] && target < nums[mid])
            {
                return Fen(nums, i, mid, target);
            }
            else
            {
                return Fen(nums, mid + 1, j, target);
            }
        }
        else
        {
            if (nums[mid] < target && target <= nums[j])
            {
                return Fen(nums, mid + 1, j, target);
            }
            else
            {
                return Fen(nums, i, mid, target);
            }
        }
    }
}

/// <summary>
/// 题号：34. 在排序数组中查找元素的第一个和最后一个位置
/// 题目：
/// 给定一个按照升序排列的整数数组 nums，和一个目标值 target。找出给定目标值在数组中的开始位置和结束位置。
/// 你的算法时间复杂度必须是 O(log n) 级别。
/// 如果数组中不存在目标值，返回[-1, -1]。
/// 输入: nums = [5,7,7,8,8,10], target = 8
/// 输出: [3,4]
/// 
/// 分治法
/// 双指针
/// </summary>
public class Solution
{
    public int[] SearchRange(int[] nums, int target)
    {
        int[] result = new int[2] { -1, -1 };
        if (nums.Length == 0) return result;

        result[0] = Fen_left(nums, target, 0, nums.Length - 1);
        result[1] = Fen_right(nums, target, 0, nums.Length - 1);

        return result;
    }

    private int Fen_left(int[] nums, int target, int left, int right)
    {
        if (left > right) return -1;

        int mid = (left + right) / 2;

        if (nums[mid] == target)
        {
            if (mid == 0 || nums[mid - 1] != target)
            {
                return mid;
            }
            else
            {
                return Fen_left(nums, target, left, mid - 1);
            }
        }
        else if (nums[mid] > target)
        {
            return Fen_left(nums, target, left, mid - 1);
        }
        else
        {
            return Fen_left(nums, target, mid + 1, right);
        }
    }

    private int Fen_right(int[] nums, int target, int left, int right)
    {
        if (left > right) return -1;

        int mid = (left + right) / 2;

        if (nums[mid] == target)
        {
            if (mid == nums.Length - 1 || nums[mid + 1] != target)
            {
                return mid;
            }
            else
            {
                return  Fen_right(nums, target, mid + 1, right);
            }
        }
        else if (nums[mid] > target)
        {
            return Fen_right(nums, target, left, mid - 1);
        }
        else
        {
            return Fen_right(nums, target, mid + 1, right);
        }
    }
}

/// <summary>
/// 题号：37. 解数独
/// 题目：
/// 编写一个程序，通过已填充的空格来解决数独问题。
/// 一个数独的解法需遵循如下规则：
/// 数字 1 - 9 在每一行只能出现一次。
/// 数字 1 - 9 在每一列只能出现一次。
/// 数字 1 - 9 在每一个以粗实线分隔的 3x3 宫内只能出现一次。
/// 空白格用 '.' 表示。
/// 
/// 递归
/// 试错法
/// </summary>
public class Solution
{
    //建立三个试错数组
    //对应行、列、每一格
    bool[,] row = new bool[9, 9];  //9行每行9个数是否用到
    bool[,] column = new bool[9, 9];//9列
    bool[,,] lattice = new bool[3, 3, 9]; //大数格中是否出现
    bool rea = false;    //判断是否完成，完成也跳出

    List<int[]> temp = new List<int[]>();  //辅助列表，放入空的格子

    public void SolveSudoku(char[][] board)
    {
        //初始化
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] >= '1' && board[i][j] <= '9')
                {
                    row[i, board[i][j] - '0' - 1] = true;
                    column[j, board[i][j] - '0' - 1] = true;
                    lattice[i / 3, j / 3, board[i][j] - '0' - 1] = true;
                }
                else
                {
                    //将未填的数字列出来
                    temp.Add(new int[2] { i, j });
                }
            }
        }

        DFS(board, 0);
    }

    private void DFS(char[][] board, int numcount)
    {
        //指针指向最后的话即为正确解
        if (temp.Count == numcount)
        {
            rea = true;
            return;
        }

        int[] num = temp[numcount];
        int i = num[0];
        int j = num[1];

        //每个数字都填一遍，且未完成
        for (int m = 0; m < 9 && !rea; m++)
        {
            if (!row[i,m] && !column[j,m] && !lattice[i / 3, j / 3, m])
            {
                row[i, m] = column[j, m] = lattice[i / 3, j / 3, m] = true;
                board[i][j] = (char)(m + '0' + 1);
                DFS(board, numcount + 1);
                row[i, m] = column[j, m] = lattice[i / 3, j / 3, m] = false;
            }
        }
    }
}

/// <summary>
/// 题号：39. 组合总和
/// 题目：
/// 给定一个无重复元素的数组 candidates 和一个目标数 target ，找出 candidates 中所有可以使数字和为 target 的组合。
/// candidates 中的数字可以无限制重复被选取。
/// 说明：
/// 所有数字（包括 target）都是正整数。
/// 解集不能包含重复的组合。 
/// 输入：candidates = [2,3,6,7], target = 7,
/// 所求解集为：[ [7],  [2,2,3]]
/// 
/// dfs
/// 回溯
/// 剪枝
/// </summary>
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        int n = candidates.Length;

        if (n == 0) return result;

        Array.Sort(candidates);

        Stack<int> temp = new Stack<int>();  //使用栈来回溯
        Solve(0, target, candidates, temp);

        return result;
    }

    private void Solve(int begin, int remain, int[] candidates, Stack<int> temp)
    {
        if (remain == 0)
        {
            result.Add(temp.ToList<int>());
            return;
        }

        for (int i = begin; i < candidates.Length; i++)
        {
            if (remain - candidates[i] < 0) break;

            //使用栈进行回溯时
            temp.Push(candidates[i]);
            Solve(i, remain - candidates[i], candidates, temp);
            //需要去掉最后添加的再回溯
            temp.Pop();
        }
    }
}

/// <summary>
/// 题号：40. 组合总和 II
/// 题目：
/// 给定一个数组 candidates 和一个目标数 target ，找出 candidates 中所有可以使数字和为 target 的组合。
/// candidates 中的每个数字在每个组合中只能使用一次。
/// 说明：
/// 所有数字（包括目标数）都是正整数。
/// 解集不能包含重复的组合。 
/// 
/// 输入: candidates = [10,1,2,7,6,1,5], target = 8,
/// 所求解集为:
/// [[1, 7],  [1, 2, 5],  [2, 6],  [1, 1, 6]]
/// 
/// dfs
/// 回溯
/// 剪枝
/// </summary>
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        int n = candidates.Length;

        if (n == 0) return result;

        Array.Sort(candidates);

        Stack<int> dfs = new Stack<int>();

        DFS(candidates, target, 0, dfs);

        return result;
    }

    private void DFS(int[] candidates, int target, int i, Stack<int> dfs) 
    {
        if (target == 0)
        {
            result.Add(dfs.ToList<int>());
            return;
        }

        if (i > candidates.Length - 1)
        {
            return;
        }

        for (int m = i; m < candidates.Length; m++)
        {
            if (target - candidates[m] < 0) break;

            if (m == i || candidates[m] != candidates[m - 1])
            {
                dfs.Push(candidates[m]);
                DFS(candidates, target - candidates[m], m + 1, dfs);
                dfs.Pop();
            }
            else
            {
                continue;
            }
        }
    }
}

/// <summary>
/// 题号：42. 接雨水
/// 题目：
/// 给定 n 个非负整数表示每个宽度为 1 的柱子的高度图，计算按此排列的柱子，下雨之后能接多少雨水。
/// 上面是由数组[0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1] 表示的高度图，在这种情况下，可以接 6 个单位的雨水（蓝色部分表示雨水）。 
/// 图见leedcode
/// 输入: [0,1,0,2,1,0,1,3,2,1,2,1]
/// 输出: 6
/// 
/// 
/// </summary>
//两次遍历 左右
//左右各选出合适水位，及最后一位最高位置
public class Solution
{
    public int Trap(int[] height)
    {
        int n = height.Length;
        //先从左往右遍历
        int i = 0;
        int j = 0;
        int water_left = 0;
        int high_left = 0;
        while (j < n)
        {
            if (height[j] >= height[i])
            {
                for (int m = i + 1; m < j; m++)
                {
                    water_left += (height[i] - height[m]);
                }
                i = j;
                high_left = j;
            }
            j++;
        }

        //从右往左遍历
        i = n - 1;
        j = n - 1;
        int water_right = 0;
        int high_right = 0;
        while (i >= 0)
        {
            if (height[i] >= height[j])
            {
                for (int m = i + 1; m < j; m++)
                {
                    water_right += (height[j] - height[m]);
                }
                j = i;
                high_right = i;
            }
            i--;
        }

        int result = 0;
        if (high_left == high_right)
        {
            result = water_left + water_right;
        }
        else
        {
            int temp = 0;
            for (int m = high_right + 1; m < high_left; m++)
            {
                temp += (height[high_right] - height[m]);
            }
            result = water_left + water_right - temp;
        }

        return result;
    }
}

//官方大佬双指针
public class Solution
{
    public int Trap(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int result = 0;
        int high_left = 0;
        int high_right = 0;
        while (left < right)
        {
            if (height[left] < height[right])
            {
                if (height[left] >= high_left)
                {
                    high_left = height[left];
                }
                else
                {
                    result += (high_left - height[left]);
                }
                left++;
            }
            else
            {
                if (height[right] >= high_right)
                {
                    high_right = height[right];
                }
                else
                {
                    result += (high_right - height[right]);
                }
                right--;
            }
        }

        return result;
    }
}

/// <summary>
/// 题号：45. 跳跃游戏 II
/// 题目：
/// 给定一个非负整数数组，你最初位于数组的第一个位置。
/// 数组中的每个元素代表你在该位置可以跳跃的最大长度。
/// 你的目标是使用最少的跳跃次数到达数组的最后一个位置。
/// 示例:
/// 输入:[2,3,1,1,4]
/// 输出: 2
/// 解释: 跳到最后一个位置的最小跳跃数是 2。
/// 从下标为 0 跳到下标为 1 的位置，跳 1 步，然后跳 3 步到达数组的最后一个位置。
/// 
/// 贪心，暴力解法
/// 每次更新最大位置
/// 当到达边界就更新
/// </summary>
public class Solution
{
    public int Jump(int[] nums)
    {
        int n = nums.Length;
        //最大可达距离
        int most = 0;
        //边界
        int end = 0;
        //步数
        int step = 0;
        //不需要判断最后一位
        for (int i = 0; i < n - 1; i++)
        {
            if (i <= most)
            {
                most = Math.Max(most, i + nums[i]);
                if (i == end)
                {
                    //更新边界，步数加1
                    end = most;
                    step++;
                }
            }
        }
        return step;
    }
}

/// <summary>
/// 题号：46. 全排列
/// 题目：
/// 给定一个 没有重复 数字的序列，返回其所有可能的全排列。
/// 示例:
/// 输入:[1,2,3]
/// 输出:
/// [
/// [1,2,3],
/// [1,3,2],
/// [2,1,3],
/// [2,3,1],
/// [3,1,2],
/// [3,2,1]
/// ]
/// 
/// 深度优先搜索加回溯
/// 1+ [2,3]的排列
/// </summary>
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();
    public IList<IList<int>> Permute(int[] nums)
    {
        int n = nums.Length;
        if (n == 0) return result;
        bool[] used = new bool[n];
        IList<int> temp = new List<int>();
        DFS(nums, used, temp);
        return result;
    }

    private void DFS(int[] nums, bool[] used, IList<int> temp)
    {
        if (temp.Count == nums.Length)
        {
            //需要新创建一个队列，因为是值传递，如果不创建则返回空
            result.Add(new List<int>(temp));
            return;
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (!used[i])
            {
                temp.Add(nums[i]);
                used[i] = true;
                DFS(nums, used, temp);
                used[i] = false;
                temp.RemoveAt(temp.Count - 1);
            }
        }
    }
}


/// <summary>
/// 题号：47. 全排列 II
/// 题目：
/// 给定一个可包含重复数字的序列，返回所有不重复的全排列。
/// 输入:[1,1,2]
/// 输出:
/// [
/// [1,1,2],  [1,2,1],  [2,1,1]
/// ]
/// 
/// 深度优先搜索加回溯
/// </summary>
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        int n = nums.Length;
        if (n == 0) return result;
        Array.Sort(nums);
        bool[] used = new bool[n];
        IList<int> temp = new List<int>();
        DFS(nums, used, temp);
        return result;
    }

    private void DFS(int[] nums, bool[] used, IList<int> temp)
    {
        if (temp.Count == nums.Length)
        {
            result.Add(new List<int>(temp));
            return;
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1] && used[i - 1] == false) continue;
            if (!used[i])
            {
                temp.Add(nums[i]);
                used[i] = true;
                DFS(nums, used, temp);
                used[i] = false;
                temp.RemoveAt(temp.Count - 1);
            }
        }
    }
}

/// <summary>
/// 题号：49. 字母异位词分组
/// 题目：
/// 给定一个字符串数组，将字母异位词组合在一起。字母异位词指字母相同，但排列不同的字符串。
/// 示例:
/// 输入:["eat", "tea", "tan", "ate", "nat", "bat"]
/// 输出:
/// [
/// ["ate","eat","tea"],
/// ["nat","tan"],
/// ["bat"]
/// ]
/// 
/// 哈希表
/// 以排列好的字符串为键
/// </summary>
public class Solution
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        IList<IList<string>> result = new List<IList<string>>();
        int n = strs.Length;
        if (n == 0) return result;
        //建立哈希表
        Dictionary<string, List<string>> HashMap = new Dictionary<string, List<string>>();
        for (int i = 0; i < n; i++)
        {
            char[] m = strs[i].ToArray();
            Array.Sort(m);
            string stringtemp = String.Join("", m);
            if (HashMap.ContainsKey(stringtemp))
            {
                HashMap[stringtemp].Add(strs[i]);
            }
            else
            {
                HashMap.Add(stringtemp, new List<string>() { strs[i] });
            }
        }
        return new List<IList<string>>(HashMap.Values);
    }
}

/// <summary>
/// 题号：51. N 皇后
/// 题目：
/// n 皇后问题研究的是如何将 n 个皇后放置在 n×n 的棋盘上，并且使皇后彼此之间不能相互攻击。
/// 上图为 8 皇后问题的一种解法。
/// 给定一个整数 n，返回所有不同的 n 皇后问题的解决方案。
/// 每一种解法包含一个明确的 n 皇后问题的棋子放置方案，该方案中 'Q' 和 '.' 分别代表了皇后和空位。
/// 示例:
/// 输入: 4
/// 输出: 
/// [
/// [".Q..",  // 解法 1
/// "...Q",
/// "Q...",
/// "..Q."],
/// 
/// ["..Q.",  // 解法 2
/// "Q...",
/// "...Q",
/// ".Q.."]
/// ]
/// 
/// 回溯
/// </summary>
public class Solution
{
    IList<IList<string>> result = new List<IList<string>>();
    public IList<IList<string>> SolveNQueens(int n)
    {
        if (n == 0) return result;

        //记录每行对应位置数组
        int[] Queen = new int[n];
        for (int i = 0; i < n; i++)
        {
            Queen[i] = -1;
        }

        //行是遍历的
        //列数组
        bool[] used_line = new bool[n];
        //左斜数组
        bool[] used_left = new bool[2 * n - 1];
        //右斜数组
        bool[] used_right = new bool[2 * n - 1];

        Total(n, 0, Queen, used_line, used_left, used_right);
        return result;
    }

    private void Total(int n, int row, int[] Queen, bool[] used_line, bool[] used_left, bool[] used_right)
    {
        if (row == n)
        {
            result.Add(new List<string>(Write(Queen, n)));
            return;
        }

        for (int i = 0; i < n; i++)
        {
            //used_right需要额外加上n-1防止下标出错
            //如果三个数组中有任一一个已经被使用了，则不在对应位置添加
            if (used_line[i] == true) continue;
            int left = row + i;
            if (used_left[left] == true) continue;
            int right = row - i + n - 1;
            if (used_right[right] == true) continue;
            Queen[row] = i;
            used_line[i] = true;
            used_left[left] = true;
            used_right[right] = true;
            Total(n, row + 1, Queen, used_line, used_left, used_right);
            Queen[row] = -1;
            used_line[i] = false;
            used_left[left] = false;
            used_right[right] = false;
        }
    }

    private IList<string> Write(int[] Queen, int n)
    {
        IList<string> temp = new List<string>();
        for (int i = 0; i < n; i++)
        {
            char[] row = new char[n];
            for (int j = 0; j < n; j++) row[j] = '.';
            row[Queen[i]] = 'Q';
            temp.Add(new string(row));
        }
        return temp;
    }
}

/// <summary>
/// 题号：52. N皇后 II
/// 题目：
/// n 皇后问题研究的是如何将 n 个皇后放置在 n×n 的棋盘上，并且使皇后彼此之间不能相互攻击。
/// 上图为 8 皇后问题的一种解法。
/// 给定一个整数 n，返回 n 皇后不同的解决方案的数量。
/// 示例:
/// 输入: 4
/// 输出: 2
/// 解释: 4 皇后问题存在如下两个不同的解法。
/// [
/// [".Q..",  // 解法 1
/// "...Q",
/// "Q...",
/// "..Q."],
/// 
/// ["..Q.",  // 解法 2
/// "Q...",
/// "...Q",
/// ".Q.."]
/// ]
/// 
/// 回溯
/// </summary>
public class Solution
{
    int result = 0;
    public int TotalNQueens(int n)
    {
        if (n == 0) return 0;

        //行是遍历的
        //列数组
        bool[] used_line = new bool[n];
        //左斜数组
        bool[] used_left = new bool[2 * n - 1];
        //右斜数组
        bool[] used_right = new bool[2 * n - 1];
        Total(n, 0, used_line, used_left, used_right);
        return result;
    }

    private void Total(int n, int row, bool[] used_line, bool[] used_left, bool[] used_right)
    {
        if (row == n)
        {
            result++;
            return;
        }

        for (int i = 0; i < n; i++)
        {
            //used_right需要额外加上n-1防止下标出错
            //如果三个数组中有任一一个已经被使用了，则不在对应位置添加
            if (used_line[i] == true) continue;
            int left = row + i;
            if (used_left[left] == true) continue;
            int right = row - i + n - 1;
            if (used_right[right] == true) continue;
            used_line[i] = true;
            used_left[left] = true;
            used_right[right] = true;
            Total(n, row + 1, used_line, used_left, used_right);
            used_line[i] = false;
            used_left[left] = false;
            used_right[right] = false;
        }
    }
}

/// <summary>
/// 题号：53. 最大子序和
/// 题目：
/// 给定一个整数数组 nums ，找到一个具有最大和的连续子数组（子数组最少包含一个元素），返回其最大和。
/// 示例:
/// 输入:[-2,1,-3,4,-1,2,1,-5,4]
/// 输出: 6
/// 解释: 连续子数组[4, -1, 2, 1] 的和最大，为 6。
/// 进阶:
/// 
/// 如果你已经实现复杂度为 O(n) 的解法，尝试使用更为精妙的分治法求解。
/// 
/// 递归
/// f(i)=max{f(i−1)+ai,ai}
/// 找最大的
/// </summary>
public class Solution
{
    public int MaxSubArray(int[] nums)
    {
        int n = nums.Length;
        int[] dp = new int[n];

        dp[0] = nums[0];
        int result = dp[0];

        for (int i = 1; i < n; i++)
        {
            dp[i] = Math.Max(dp[i - 1] + nums[i], nums[i]);
            result = Math.Max(dp[i], result);
        }

        return result;
    }
}

/// <summary>
/// 题号：55. 跳跃游戏
/// 题目：
/// 给定一个非负整数数组，你最初位于数组的第一个位置。
/// 数组中的每个元素代表你在该位置可以跳跃的最大长度。
/// 判断你是否能够到达最后一个位置。
/// 示例 1:
/// 输入:[2,3,1,1,4]
/// 输出: true
/// 解释: 我们可以先跳 1 步，从位置 0 到达 位置 1, 然后再从位置 1 跳 3 步到达最后一个位置。
/// 
/// 贪心，暴力解法
/// 每次更新最大位置，如果比最后位置长或等就返回true
/// </summary>
public class Solution
{
    public bool CanJump(int[] nums)
    {
        int n = nums.Length;
        //最大可达距离
        int most = 0;
        for (int i = 0; i < n; i++)
        {
            if (i <= most)
            {
                most = Math.Max(most, i + nums[i]);
                if (most >= n - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

/// <summary>
/// 题号：56. 合并区间
/// 题目：
/// 给出一个区间的集合，请合并所有重叠的区间。
/// 示例 1:
/// 输入: intervals = [[1, 3],[2,6],[8,10],[15,18]]
/// 输出:[[1,6],[8,10],[15,18]]
/// 解释: 区间[1, 3] 和[2, 6] 重叠, 将它们合并为[1, 6].
/// 示例 2:
/// 输入: intervals = [[1, 4],[4,5]]
/// 输出:[[1,5]]
/// 解释: 区间[1, 4] 和[4, 5] 可被视为重叠区间。
/// 
/// 重点
/// 二维数组排序，需要重写比较函数
/// 
/// 把数组顺序排序后，肯定的相邻的可以合并，当最后一个合并数组的最大值小于后面的最小值
/// 后面的肯定为单独数组
/// </summary>
public class Solution
{
    public int[][] Merge(int[][] intervals)
    {
        int n = intervals.Length;
        if (n == 0) return new int[0][];
        // x[0].CompareTo(y[0]) 表示我以二维数组每个一维数组的第一个数为比较的基准
        // 还可以写为(a, b) => a[0] - b[0] 比较第一个数
        Array.Sort(intervals, (a, b) => a[0] - b[0]);

        //合并的列表
        List<int[]> merge = new List<int[]>();
        for (int i = 0; i < n; i++)
        {
            //每次的左右边界
            int L = intervals[i][0];
            int R = intervals[i][1];

            int mergeLength = merge.Count;
            //列表中最后一次
            if (mergeLength == 0 || merge[mergeLength - 1][1] < L)
            {
                merge.Add(new int[] { L, R });
            }
            else
            {
                //合并后的数组取最大值
                merge[mergeLength - 1][1] = Math.Max(merge[mergeLength - 1][1], R);
            }
        }
        return merge.ToArray();
    }
}

/// <summary>
/// 题号：57. 插入区间
/// 题目：
/// 给出一个无重叠的 ，按照区间起始端点排序的区间列表。
/// 在列表中插入一个新的区间，你需要确保列表中的区间仍然有序且不重叠（如果有必要的话，可以合并区间）。
/// 示例 1：
/// 输入：intervals = [[1, 3],[6,9]], newInterval = [2, 5]
/// 输出：[[1,5],[6,9]]
/// 输入：intervals = [[1, 2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4, 8]
/// 输出：[[1,2],[3,10],[12,16]]
/// 解释：这是因为新的区间[4, 8] 与[3, 5],[6,7],[8,10] 重叠。
/// </summary>
public class Solution
{
    public int[][] Insert(int[][] intervals, int[] newInterval)
    {
        List<int[]> result = new List<int[]>();

        if (newInterval.Length == 0) return intervals;
        int n = intervals.Length;
        if (n == 0) return new int[][] { newInterval };

        //最差情况为添加数在两侧
        int index = n;

        int left_num = newInterval[0];
        int right_num = newInterval[1];

        //找最左边
        for (int i = 0; i < n; i++)
        {
            int right_temp = intervals[i][1];

            if (right_temp < left_num)
            {
                result.Add(intervals[i]);
            }
            else
            {
                index = i;
                break;
            }
        }

        //前面的都小，直接添加最后
        if (index == n)
        {
            result.Add(newInterval);
            return result.ToArray();
        }

        //正好插在中间
        if (right_num < intervals[index][0])
        {
            result.Add(newInterval);
            for (int i = index; i < n; i++)
            {
                result.Add(intervals[i]);
            }
            return result.ToArray();
        }

        //中间的值
        int[] temp = new int[2];
        temp[0] = Math.Min(left_num, intervals[index][0]);

        //右边界
        for (int i = index; i < n; i++)
        {
            int left_temp = intervals[i][0];
            if (left_temp > right_num) break;
            index++;
        }

        temp[1] = Math.Max(right_num, intervals[index - 1][1]);

        result.Add(temp);

        if (index == n) return result.ToArray();

        for (int i = index; i < n; i++)
        {
            result.Add(intervals[i]);
        }
        return result.ToArray();
    }
}

/// <summary>
/// 题号：62. 不同路径
/// 题目：
/// 一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。
/// 机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。
/// 问总共有多少条不同的路径？
/// 例如，上图是一个7 x 3 的网格。有多少可能的路径？
/// 示例 1:
/// 输入: m = 3, n = 2
/// 输出: 3
/// 解释:
/// 从左上角开始，总共有 3 条路径可以到达右下角。
/// 1.向右->向右->向下
/// 2.向右->向下->向右
/// 3.向下->向右->向右
/// 示例 2:
/// 输入: m = 7, n = 3
/// 输出: 28
/// 
/// 动态规划
/// 求最优解时用动规
/// 求所有解时用DFS
/// </summary>
public class Solution
{
    public int UniquePaths(int m, int n)
    {
        if (m == 0 || n == 0) return 0;
        int[,] dp = new int[m, n];
        //初始化
        dp[0, 0] = 1;
        //每行，每列都只能有一种可能性
        for (int i = 1; i < m; i++)
        {
            dp[i, 0] = dp[i - 1, 0];
        }
        for (int i = 1; i < n; i++)
        {
            dp[0, i] = dp[0, i - 1];
        }
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
            }
        }
        return dp[m - 1, n - 1];
    }
}

/// <summary>
/// 题号：63. 不同路径 II
/// 题目：
/// 一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。
/// 机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。
/// 现在考虑网格中有障碍物。那么从左上角到右下角将会有多少条不同的路径？
/// 网格中的障碍物和空位置分别用 1 和 0 来表示。
/// 输入：obstacleGrid = [[0,0,0],[0,1,0],[0,0,0]]
/// 输出：2
/// 解释：
/// 3x3 网格的正中间有一个障碍物。
/// 从左上角到右下角一共有 2 条不同的路径：
/// 1. 向右 -> 向右 -> 向下 -> 向下
/// 2. 向下 -> 向下 -> 向右 -> 向右
/// 
/// 动态规划
/// 求最优解时用动规
/// 求所有解时用DFS
/// </summary>
public class Solution
{
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        if (obstacleGrid == null || obstacleGrid.Length == 0 || obstacleGrid[0].Length == 0)
        {
            return 0;
        }
        int m = obstacleGrid.Length;
        int n = obstacleGrid[0].Length;

        if (obstacleGrid[0][0] == 1) return 0;

        int[,] dp = new int[m, n];

        dp[0, 0] = 1;

        for (int i = 1; i < m; i++)
        {
            if (obstacleGrid[i][0] == 1) break;
            //只能从上边过来
            dp[i, 0] = dp[i - 1, 0];
        }
        for (int j = 1; j < n; j++)
        {
            if (obstacleGrid[0][j] == 1) break;
            //只能从左边过来
            dp[0, j] = dp[0, j - 1];
        }
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (obstacleGrid[i][j] != 1)
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }
        return dp[m - 1, n - 1];
    }
}

/// <summary>
/// 题号：64. 最小路径和
/// 题目：
/// 给定一个包含非负整数的 m x n 网格，请找出一条从左上角到右下角的路径，使得路径上的数字总和为最小。
/// 说明：每次只能向下或者向右移动一步。
/// 示例:
/// 输入:
/// [
/// [1,3,1],
/// [1,5,1],
/// [4,2,1]
/// ]
/// 输出: 7
/// 解释: 因为路径 1→3→1→1→1 的总和最小。
/// 
/// 动态规划
/// 求最优解时用动规
/// 求所有解时用DFS
/// </summary>
public class Solution
{
    public int MinPathSum(int[][] grid)
    {
        int m = grid.Length;
        if (m == 0) return 0;
        int n = grid[0].Length;

        int[,] dp = new int[m, n];

        //初始条件
        dp[0, 0] = grid[0][0];
        //在第0行与第0列的数只能从左或从上加
        for (int i = 1; i < m; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + grid[i][0];
        }
        for (int i = 1; i < n; i++)
        {
            dp[0, i] = dp[0, i - 1] + grid[0][i];
        }
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i][j];
            }
        }
        return dp[m - 1, n - 1];
    }
}

/// <summary>
/// 题号：72. 编辑距离
/// 题目：
/// 给你两个单词 word1 和 word2，请你计算出将 word1 转换成 word2 所使用的最少操作数 。
/// 你可以对一个单词进行如下三种操作：
/// 插入一个字符
/// 删除一个字符
/// 替换一个字符
/// 示例 1：
/// 输入：word1 = "horse", word2 = "ros"
/// 输出：3
/// 解释：
/// horse -> rorse (将 'h' 替换为 'r')
/// rorse->rose(删除 'r')
/// rose->ros(删除 'e')
/// 示例 2：
/// 输入：word1 = "intention", word2 = "execution"
/// 输出：5
/// 解释：
/// intention->inention(删除 't')
/// inention->enention(将 'i' 替换为 'e')
/// enention->exention(将 'n' 替换为 'x')
/// exention->exection(将 'n' 替换为 'c')
/// exection->execution(插入 'u')
/// 
/// 动态规划
/// 单词A的插入等同于单词B的删除
/// 单词A的修改等同于单词B的修改
/// dp[i][j]指将单词A的前i位修改为和单词B的前j位相等的编辑距离
/// 如果单词A或B为0，则dp[i][0] = i
/// 否则的话直接修改A或B
/// dp[i][j-1] + 1 单词B直接加一位，形成dp[i][j]
/// dp[i-1][j] + 1 单词A直接加一位，形成dp[i][j]
/// dp[i][j] + 1 单词A或B修改最后一位，形成dp[i][j]
/// 当第i位和第j位相同时
/// dp[i][j] = min(dp[i][j-1] + 1,dp[i-1][j]+1,dp[i-1][j-1])
/// 当第i位和第j位不同时
/// dp[i][j] = min(dp[i][j-1] + 1,dp[i-1][j]+1,dp[i-1][j-1] +1)
/// </summary>
public class Solution
{
    public int MinDistance(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;

        //包含空字符串，所以长度为m+1与n+1
        int[,] dp = new int[m + 1, n + 1];
        //初始化
        //空字符串匹配
        for (int i = 0; i < m + 1; i++) dp[i, 0] = i;
        for (int j = 0; j < n + 1; j++) dp[0, j] = j;

        if (m == 0 || n == 0)
        {
            return dp[m, n];
        }

        for (int i = 1; i < m + 1; i++)
        {
            for (int j = 1; j < n + 1; j++)
            {
                //最后一位相同时
                if (word1[i - 1] == word2[j - 1])
                {
                    dp[i, j] = Math.Min(dp[i - 1, j] + 1, Math.Min(dp[i, j - 1] + 1, dp[i - 1, j - 1])); 
                }
                else
                {
                    dp[i, j] = 1 + Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1]));
                }
            }
        }
        return dp[m, n];
    }
}

/// <summary>
/// 题号：73. 矩阵置零
/// 题目：
/// 给定一个 m x n 的矩阵，如果一个元素为 0，则将其所在行和列的所有元素都设为 0。请使用原地算法。
/// 示例 1:
/// 输入:
/// [
/// [1,1,1],
/// [1,0,1],
/// [1,1,1]
/// ]
/// 输出:
/// [
/// [1,0,1],
/// [0,0,0],
/// [1,0,1]
/// ]
/// 示例 2:
/// 输入:
/// [
/// [0,1,2,0],
/// [3,4,5,2],
/// [1,3,1,5]
/// ]
/// 输出:
/// [
/// [0,0,0,0],
/// [0,4,5,0],
/// [0,3,1,0]
/// ]
/// 进阶:
/// 一个直接的解决方案是使用 O(mn) 的额外空间，但这并不是一个好的解决方案。
/// 一个简单的改进方案是使用 O(m + n) 的额外空间，但这仍然不是最好的解决方案。
/// 你能想出一个常数空间的解决方案吗？
/// </summary>
/// 法一：储存对应的行数与列数
public class Solution
{
    public void SetZeroes(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return;
        int m = matrix.Length;
        int n = matrix[0].Length;
        HashSet<int> row = new HashSet<int>();
        HashSet<int> line = new HashSet<int>();

        //找到0值对应的行数与列数
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 0)
                {
                    if (!row.Contains(i)) row.Add(i);
                    if (!line.Contains(j)) line.Add(j);
                }
            }
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (row.Contains(i) || line.Contains(j))
                {
                    matrix[i][j] = 0;
                }
            }
        }
        return;
    }
}

/// 法二：将每次的第一行与第一列置0；
/// if(matrix[i][j]==0) matrix[i][0]=0;matrix[0][j]=0;
/// 额外判定本来的第一行与第一列是否存在0
public class Solution
{
    public void SetZeroes(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return;
        int m = matrix.Length;
        int n = matrix[0].Length;
        bool line0 = false;

        for (int i = 0; i < m; i++)
        {
            //第一列是否为0
            if (matrix[i][0] == 0) line0 = true;
            for (int j = 1; j < n; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0;
                    matrix[0][j] = 0;
                }
            }
        }

        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (matrix[i][0] == 0 || matrix[0][j] == 0)
                {
                    matrix[i][j] = 0;
                }
            }
        }

        if (matrix[0][0] == 0)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[0][j] = 0;
            }
        }

        if (line0)
        {
            for (int i = 0; i < m; i++)
            {
                matrix[i][0] = 0;
            }
        }

        return;
    }
}

/// <summary>
/// 题号：76. 最小覆盖子串
/// 题目：
/// 给你一个字符串 S、一个字符串 T 。请你设计一种算法，可以在 O(n) 的时间复杂度内，从字符串 S 里面找出：包含 T 所有字符的最小子串。
/// 示例：
/// 输入：S = "ADOBECODEBANC", T = "ABC"
/// 输出："BANC"
/// 提示：
/// 如果 S 中不存这样的子串，则返回空字符串 ""。
/// 如果 S 中存在这样的子串，我们保证它是唯一的答案。
/// 
/// 滑动窗口
/// 右扩，包含所有需要的
/// 左缩，取最小
/// </summary>
public class Solution
{
    public string MinWindow(string s, string t)
    {
        //截取字符串左右边界
        int L = -1;
        int R = int.MaxValue - 1;
        //判定（滑动窗口）字符串的左右边界
        int amL = 0;
        int amR = 0;

        int tnum = t.Length;
        //最短字符串长度
        int minLength = int.MaxValue;

        if (t.Length == 0) return "";

        //储存需要字符串个数的哈希表
        Dictionary<char, int> need1 = new Dictionary<char, int>();
        for (int i = 0; i < tnum; i++)
        {
            if (need1.ContainsKey(t[i]))
            {
                need1[t[i]]++;
            }
            else
            {
                need1.Add(t[i], 1);
            }
        }

        //判定位置
        while (amR < s.Length)
        {
            //判定右边界
            //只要有值没被取到
            while (!Check(need1))
            {
                if (amR == s.Length)
                {
                    return L == -1 ? "" : s.Substring(L, R - L);
                }
                if (need1.ContainsKey(s[amR]))
                {
                    need1[s[amR]]--;
                }
                amR++;
            }
            //确定左边界
            while (Check(need1))
            {
                if (need1.ContainsKey(s[amL]))
                {
                    need1[s[amL]]++;
                }
                amL++;
            }
            if ((R - L) >= (amR - amL + 1))
            {
                L = amL - 1;
                R = amR;
            }
        }

        return L == -1 ? "" : s.Substring(L, R - L);
    }

    private bool Check(Dictionary<char, int> need1)
    {
        foreach (int value in need1.Values)
        {
            if (value > 0)
            {
                return false;
            }
        }
        return true;
    }
}

/// <summary>
/// 题号：78. 子集
/// 题目：
/// 给定一组不含重复元素的整数数组 nums，返回该数组所有可能的子集（幂集）。
/// 说明：解集不能包含重复的子集。
/// 示例:
/// 输入: nums = [1, 2, 3]
/// 输出:
/// [
/// [3],  [1],  [2],  [1,2,3],  [1,3],  [2,3],  [1,2],  []
/// ]
/// 
/// 法一：按位与，000-111间的数取值
/// 法二：回溯
/// </summary>
//法一：按位计算
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();
    public IList<IList<int>> Subsets(int[] nums)
    {
        int n = nums.Length;
        //位移操作
        for (int i = 0; i < (1 << n); i++)
        {
            IList<int> temp = new List<int>();
            for (int j = 0; j < n; j++)
            {
                //按位与
                if ((i & (1 << j)) != 0)
                {
                    temp.Add(nums[j]);
                }
            }
            result.Add(new List<int>(temp));
        }
        return result;
    }
}

//法二：回溯
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();
    public IList<IList<int>> Subsets(int[] nums)
    {
        if (nums.Length == 0) return result;
        IList<int> temp = new List<int>();
        DFS(nums, 0, temp);
        return result;
    }

    private void DFS(int[] nums, int i, IList<int> temp)
    {
        //先放入值
        result.Add(new List<int>(temp));
        if (i == nums.Length) return;
        for (int j = i; j < nums.Length; j++)
        {
            temp.Add(nums[j]);
            DFS(nums, j + 1, temp);
            temp.RemoveAt(temp.Count - 1);
        }
    }
}

/// <summary>
/// 题号：79. 单词搜索
/// 题目：
/// 给定一个二维网格和一个单词，找出该单词是否存在于网格中。
/// 单词必须按照字母顺序，通过相邻的单元格内的字母构成，其中“相邻”单元格是那些水平相邻或垂直相邻的单元格。同一个单元格内的字母不允许被重复使用。
/// 示例:
/// board =
/// [
/// ['A', 'B', 'C', 'E'],
/// ['S','F','C','S'],
/// ['A','D','E','E']
/// ]
/// 给定 word = "ABCCED", 返回 true
/// 给定 word = "SEE", 返回 true
/// 给定 word = "ABCB", 返回 false
/// 
/// 深度优先搜索
/// 二维数组
/// 东南西北方向判断
/// </summary>
public class Solution
{
    public bool Exist(char[][] board, string word)
    {
        bool[,] temp = new bool[board.GetLength(0), board[0].GetLength(0)];
        List<int>[] dir = new List<int>[4];
        dir[0] = new List<int>() { 0, 1 };
        dir[1] = new List<int>() { 0, -1 };
        dir[2] = new List<int>() { 1, 0 };
        dir[3] = new List<int>() { -1, 0 };

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (Pan(board, word, i, j, 0, temp, dir))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //i，j为行数与列数
    //m为字符串指针
    //temp辅助判断是否经过的指针
    //dir方向判断
    private bool Pan(char[][] board, string word, int i, int j, int m, bool[,] temp, List<int>[] dir)
    {
        if (temp[i, j] == false)
        {
            if (board[i][j] == word[m])
            {
                if (m == word.Length - 1) return true;
                temp[i, j] = true;
                for (int x = 0; x < dir.Length; x++)
                {
                    int newX = i + dir[x][0];
                    int newY = j + dir[x][1];
                    if (newX >= 0 && newX < board.Length && newY >= 0 && newY < board[0].Length)
                    {
                        if (Pan(board, word, newX, newY, m + 1, temp, dir))
                        {
                            return true;
                        }
                    }
                }
                temp[i, j] = false;
                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

/// <summary>
/// 题号：84. 柱状图中最大的矩形
/// 题目：
/// 给定 n 个非负整数，用来表示柱状图中各个柱子的高度。每个柱子彼此相邻，且宽度为 1 。
/// 求在该柱状图中，能够勾勒出来的矩形的最大面积。
/// 以上是柱状图的示例，其中每个柱子的宽度为 1，给定的高度为[2, 1, 5, 6, 2, 3]。
/// 图中阴影部分为所能勾勒出的最大矩形面积，其面积为 10 个单位。
/// 示例:
/// 输入:[2,1,5,6,2,3]
/// 输出: 10
/// 
/// 单调栈
/// 遍历高，找最低
/// </summary>
public class Solution
{
    public int LargestRectangleArea(int[] heights)
    {
        int n = heights.Length;
        if (n == 0) return 0;

        //使用单调栈
        //始终保持栈内单调递增
        //遇到矮的不断出，肯定有最低
        //边界最低时添加-1
        //栈内放的为下标
        //需要左右两个边界
        Stack<int> height = new Stack<int>();
        int[] left = new int[n];
        int[] right = new int[n];

        //从左遍历，找左边界
        for (int i = 0; i < n; i++)
        {
            //先找更低的
            //比较当前最高的和现在的i值大小
            while (height.Count != 0 && heights[height.Peek()] >= heights[i])
            {
                //高了就去掉
                height.Pop();
            }
            //没有更低的,取当前值
            left[i] = (height.Count == 0) ? - 1 : height.Peek();
            //插入当前值
            height.Push(i);
        }

        height.Clear();
        //从右遍历，找右边界
        for (int i = n - 1; i >= 0; i--)
        {
            //先找更低的
            //比较当前最高的和现在的i值大小
            while (height.Count != 0 && heights[height.Peek()] >= heights[i])
            {
                //高了就去掉
                height.Pop();
            }
            //没有更低的,取当前值
            //没有边界的取最大值
            right[i] = (height.Count == 0) ? n : height.Peek();
            //插入当前值
            height.Push(i);
        }

        //计算面积
        int result = 0;
        for (int i = 0; i < n; i++)
        {
            result = Math.Max(result, (right[i] - left[i] - 1) * heights[i]);
        }
        return result;
    }
}

/// <summary>
/// 题号：85. 最大矩形
/// 题目：
/// 给定一个仅包含 0 和 1 的二维二进制矩阵，找出只包含 1 的最大矩形，并返回其面积。
/// 示例:
/// 输入:
/// [
/// ["1","0","1","0","0"],
/// ["1","1","1","1","1"],
/// ["1","0","0","1","0"]
/// ]
/// 输出: 6
/// 
/// 法一：对于每一行用84题的解法算最大矩形
/// 法二：动态规划
/// </summary>
/// 动态规划
/// 确定每一列最高位置，然后左右扩展
public class Solution
{
    public int MaximalRectangle(char[][] matrix)
    {
        int m = matrix.Length;
        if (m == 0) return 0;
        int n = matrix[0].Length;
        if (n == 0) return 0;

        //对应的高度
        //每列对应高度
        int[] height = new int[n];
        //左边界
        int[] left = new int[n];
        //右边界
        int[] right = new int[n];
        for (int i = 0; i < right.Length; i++)
        {
            right[i] = n;
        }

        int result = 0;
        //遍历每行
        for (int i = 0; i < m; i++)
        {
            //当前的行的左右边界，更新值
            int cur_left = 0, cur_right = n;
            //更新高
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == '1') height[j]++;
                else height[j] = 0;
            }
            //更新左边界
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == '1') left[j] = Math.Max(left[j], cur_left);
                else
                {
                    left[j] = 0;
                    cur_left = j + 1;
                }
            }

            //更新右边界
            for (int j = n - 1; j >= 0; j--)
            {
                if (matrix[i][j] == '1') right[j] = Math.Min(right[j], cur_right);
                else
                {
                    right[j] = n;
                    cur_right = j;
                }
            }

            //更新最大面积
            for (int j = 0; j < n; j++)
            {
                result = Math.Max(result, (right[j] - left[j]) * height[j]);
            }
        }
        return result;
    }
}

//法二，求对应每行的最大面积
public class Solution
{
    public int MaximalRectangle(char[][] matrix)
    {
        int m = matrix.Length;
        if (m == 0) return 0;
        int n = matrix[0].Length;
        if (n == 0) return 0;

        //对应的高度
        //每列对应高度
        int[] height = new int[n];

        int result = 0;
        //遍历每行
        for (int i = 0; i < m; i++)
        {
            //更新高
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == '1') height[j]++;
                else height[j] = 0;
            }
            
            //调用84最大矩形函数
            result = Math.Max(result, LargestRectangleArea(height));
        }
        return result;
    }

    public int LargestRectangleArea(int[] heights)
    {
        int n = heights.Length;
        if (n == 0) return 0;

        //使用单调栈
        //始终保持栈内单调递增
        //遇到矮的不断出，肯定有最低
        //边界最低时添加-1
        //栈内放的为下标
        //需要左右两个边界
        Stack<int> height = new Stack<int>();
        int[] left = new int[n];
        int[] right = new int[n];

        //从左遍历，找左边界
        for (int i = 0; i < n; i++)
        {
            //先找更低的
            //比较当前最高的和现在的i值大小
            while (height.Count != 0 && heights[height.Peek()] >= heights[i])
            {
                //高了就去掉
                height.Pop();
            }
            //没有更低的,取当前值
            left[i] = (height.Count == 0) ? -1 : height.Peek();
            //插入当前值
            height.Push(i);
        }

        height.Clear();
        //从右遍历，找右边界
        for (int i = n - 1; i >= 0; i--)
        {
            //先找更低的
            //比较当前最高的和现在的i值大小
            while (height.Count != 0 && heights[height.Peek()] >= heights[i])
            {
                //高了就去掉
                height.Pop();
            }
            //没有更低的,取当前值
            //没有边界的取最大值
            right[i] = (height.Count == 0) ? n : height.Peek();
            //插入当前值
            height.Push(i);
        }

        //计算面积
        int result = 0;
        for (int i = 0; i < n; i++)
        {
            result = Math.Max(result, (right[i] - left[i] - 1) * heights[i]);
        }
        return result;
    }
}

/// <summary>
/// 题号：86. 分隔链表
/// 题目：
/// 给定一个链表和一个特定值 x，对链表进行分隔，使得所有小于 x 的节点都在大于或等于 x 的节点之前。
/// 你应当保留两个分区中每个节点的初始相对位置。
/// 示例:
/// 输入: head = 1->4->3->2->5->2, x = 3
/// 输出: 1->2->2->4->3->5
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode Partition(ListNode head, int x)
    {
        if (head == null) return null;
        ListNode minfirst = new ListNode(0);
        ListNode minindex = minfirst;
        ListNode maxfirst = new ListNode(0);
        ListNode maxindex = maxfirst;

        while (head != null)
        {
            if (head.val < x)
            {
                minindex.next = head;
                minindex = minindex.next;
            }
            else
            {
                maxindex.next = head;
                maxindex = maxindex.next;
            }
            head = head.next;
        }

        //末尾清空
        maxindex.next = null;

        minindex.next = maxfirst.next;

        return minfirst.next;
    }
}

/// <summary>
/// 题号：89. 格雷编码
/// 题目：
/// 格雷编码是一个二进制数字系统，在该系统中，两个连续的数值仅有一个位数的差异。
/// 给定一个代表编码总位数的非负整数 n，打印其格雷编码序列。即使有多个不同答案，你也只需要返回其中一种。
/// 格雷编码序列必须以 0 开头。
/// 示例 1:
/// 输入: 2
/// 输出:[0,1,3,2]
/// 解释:
/// 00 - 0
/// 01 - 1
/// 11 - 3
/// 10 - 2
/// 对于给定的 n，其格雷编码序列并不唯一。
/// 例如，[0,2,3,1] 也是一个有效的格雷编码序列。
/// 00 - 0
/// 10 - 2
/// 11 - 3
/// 01 - 1
/// 示例 2:
/// 输入: 0
/// 输出:[0]
/// 解释: 我们定义格雷编码序列必须以 0 开头。
/// 给定编码总位数为 n 的格雷编码序列，其长度为 2n。当 n = 0 时，长度为 20 = 1。
/// 因此，当 n = 0 时，其格雷编码序列为 [0]。
/// </summary>
public class Solution
{
    public IList<int> GrayCode(int n)
    {
        IList<int> result = new List<int>();
        int len = 1 << n;
        for (int i = 0; i < len; i++)
        {
            result.Add(i ^ i >> 1);
        }
        return result;
    }
}

/// <summary>
/// 题号：94. 二叉树的中序遍历
/// 题目：
/// 给定一个二叉树，返回它的中序 遍历。
/// 进阶: 递归算法很简单，你可以通过迭代算法完成吗？
/// 
/// 递归
/// 自遍历
/// 迭代
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
//递归
//自遍历
//中序：左遍历返回时输出，右遍历正常输出
public class Solution
{
    IList<int> result = new List<int>();

    public IList<int> InorderTraversal(TreeNode root)
    {
        DFS(root);
        return result;
    }

    //中序遍历模板
    private void DFS(TreeNode root)
    {
        if (root == null) return;
        DFS(root.left);
        result.Add(root.val);
        DFS(root.right);
    }
}

//灰白标记法
//使用颜色标记节点的状态，新节点为白色，已访问的节点为灰色。
//如果遇到的节点为白色，则将其标记为灰色，然后将其右子节点、自身、左子节点依次入栈。（栈先入后出）
//如果遇到的节点为灰色，则将节点的值输出。
public class Solution
{
    IList<int> result = new List<int>();

    public IList<int> InorderTraversal(TreeNode root)
    {
        int white = 0;  //白色表示未访问
        int gray = 1;   //灰色表示已访问
        Stack<int> Colortemp = new Stack<int>();
        Stack<TreeNode> Roottemp = new Stack<TreeNode>();
        Colortemp.Push(white);
        Roottemp.Push(root);
        while (Colortemp.Count != 0 && Roottemp.Count != 0)
        {
            int color = Colortemp.Pop();
            TreeNode node = Roottemp.Pop();
            if (node == null) continue;
            if (color == white)
            {
                //中序遍历就是右中左的插入
                Colortemp.Push(white);
                Roottemp.Push(node.right);
                Colortemp.Push(gray);
                Roottemp.Push(node);
                Colortemp.Push(white);
                Roottemp.Push(node.left);
            }
            else
            {
                result.Add(node.val);
            }
        }

        return result;
    }
}

//栈
public class Solution
{
    IList<int> result = new List<int>();

    public IList<int> InorderTraversal(TreeNode root)
    {
        Stack<TreeNode> temp = new Stack<TreeNode>();
        while (root != null || temp.Count != 0)
        {
            //中序先一直取左边到底
            while (root != null)
            {
                temp.Push(root);
                root = root.left;
            }
            //到底就出
            root = temp.Pop();
            result.Add(root.val);
            //再找右边的
            root = root.right;
        }
        return result;
    }
}

/// <summary>
/// 题号：96. 不同的二叉搜索树
/// 题目：
/// 给定一个整数 n，求以 1 ... n 为节点组成的二叉搜索树有多少种？
/// 示例:
/// 输入: 3
/// 输出: 5
/// 解释:
/// 给定 n = 3, 一共有 5 种不同结构的二叉搜索树:
/// 1         3     3      2      1
///  \       /     /      / \      \
///   3     2     1      1   3      2
///  /     /       \                 \
/// 2     1         2                 3
/// 
/// 动态规划
/// </summary>
public class Solution
{
    public int NumTrees(int n)
    {
        if (n == 0) return 0;

        int[] dp = new int[n + 1];

        //初始化
        dp[0] = 1;  //分配需要乘法
        dp[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            //以任意一点 j 为根节点等于将原长度 i 分解为长度为 j-1 与 i-j 的两部分
            for (int j = 1; j <= i; j++)
            {
                //分配需要乘法
                dp[i] += (dp[j - 1] * dp[i - j]);
            }
        }

        return dp[n];
    }
}

/// <summary>
/// 题号：101
/// 题目：
/// 给定一个二叉树，检查它是否是镜像对称的。
/// 例如，二叉树[1, 2, 2, 3, 4, 4, 3] 是对称的。
///     1
///     / \
///    2   2
///   / \ / \
///  3  4 4  3
/// 
/// 双指针
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public bool IsSymmetric(TreeNode root)
    {
        return Check(root, root);
    }

    private bool Check(TreeNode p, TreeNode q)
    {
        if (p == null && q == null)
        {
            return true;
        }

        if (p == null || q == null)
        {
            return false;
        }

        return p.val == q.val && Check(p.left, q.right) && Check(p.right, q.left);
    }
}

/// <summary>
/// 题号：104. 二叉树的最大深度
/// 题目：
/// 给定一个二叉树，找出其最大深度。
/// 二叉树的深度为根节点到最远叶子节点的最长路径上的节点数。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例：
/// 给定二叉树[3, 9, 20, null, null, 15, 7]，
///     3
///    / \
///   9  20
///     /  \
///    15   7
/// 返回它的最大深度 3 。
/// 
/// DFS
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public int MaxDepth(TreeNode root)
    {
        if (root == null) return 0;
        return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
    }
}

/// <summary>
/// 题号：106. 从中序与后序遍历序列构造二叉树
/// 题目：
/// 根据一棵树的中序遍历与后序遍历构造二叉树。
/// 注意:
/// 你可以假设树中没有重复的元素。
/// 例如，给出
/// 中序遍历 inorder = [9,3,15,20,7]
/// 后序遍历 postorder = [9, 15, 7, 20, 3]
/// 
/// 返回如下的二叉树：
///   3
///  / \
/// 9  20
///   /  \
///  15   7
/// 
/// 哈希表
/// 二叉树
/// 层序遍历
/// 
/// 后序遍历 + 中序遍历
/// 后序遍历从后往前是根节点，在中序遍历中找到对应的根节点，左边的就是左子树，右边的就是右子树
/// 因为为后序遍历，所以先构建右子树再构建左子树
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    //哈希表
    Hashtable idx_map = new Hashtable();
    int post_idx;

    public TreeNode BuildTree(int[] inorder, int[] postorder)
    {
        int idx = 0;
        for (int i = 0; i < inorder.Length; i++)
        {
            //先执行再自加
            idx_map.Add(inorder[i], idx++);
        }
        post_idx = postorder.Length - 1;
        return Build(inorder, postorder, 0, inorder.Length - 1);
    }

    /// <summary>
    /// 构建二叉树
    /// </summary>
    /// <param name="inorder">前序遍历</param>
    /// <param name="postorder">后序遍历</param>
    /// <param name="post_idx">后续遍历最后一位序号：即对应根节点</param>
    /// <param name="in_left">前序左右子树左起序号</param>
    /// <param name="in_right">前序左右子树右起序号</param>
    /// <returns></returns>
    private TreeNode Build(int[] inorder, int[] postorder, int in_left, int in_right)
    {
        if (in_left > in_right)
        {
            //已经没有子树了
            return null;
        }

        //取后序遍历最后一位根节点
        int root_val = postorder[post_idx];
        //找到中序遍历对应序号
        int in_idx = (int)idx_map[root_val];

        //建立对应节点
        TreeNode root = new TreeNode(root_val);

        post_idx--;

        //构建右子树
        root.right = Build(inorder, postorder, in_idx + 1, in_right);

        //构建左子树
        root.left = Build(inorder, postorder, in_left, in_idx - 1);
        return root;
    }
}

/// <summary>
/// 题号：107. 二叉树的层次遍历 II
/// 题目：
/// 给定一个二叉树，返回其节点值自底向上的层次遍历。 （即按从叶子节点所在层到根节点所在的层，逐层从左向右遍历）
/// 例如：给定二叉树[3, 9, 20, null, null, 15, 7],
/// 3
/// / \
/// 9  20
/// /  \
/// 15   7
/// 返回其自底向上的层次遍历为：
/// [
/// [15,7],
/// [9,20],
/// [3]
/// ]
/// 
/// 广度优先搜索 BFS
/// BFS使用队列逐行搜索
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        IList<IList<int>> result = new List<IList<int>>();

        if (root == null)
        {
            return result;
        }

        //创建队列
        Queue<TreeNode> temp = new Queue<TreeNode>();
        temp.Enqueue(root);

        while (temp.Count != 0)
        {
            int size = temp.Count;

            //创建列表储存结果
            IList<int> res = new List<int>();

            //把当前层的所有东西都输出
            for (int i = 0; i < size; i++)
            {
                TreeNode roottemp = temp.Dequeue();
                res.Add(roottemp.val);
                if (roottemp.left != null)
                {
                    temp.Enqueue(roottemp.left);
                }
                if (roottemp.right != null)
                {
                    temp.Enqueue(roottemp.right);
                }
            }
            result.Add(res);
        }

        //翻转输出
        return result.Reverse().ToList();
    }
}

/// <summary>
/// 题号：111
/// 题目：
/// 给定一个二叉树，找出其最小深度。
/// 最小深度是从根节点到最近叶子节点的最短路径上的节点数量。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例:
/// 给定二叉树[3, 9, 20, null, null, 15, 7],
/// 返回它的最小深度  2.
/// 
/// 递归
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public int MinDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        else if (root.left == null && root.right == null)
        {
            //左右都没有节点
            return 1;
        }
        else if (root.left == null || root.right == null)
        {
            //当有一个节点为空时
            return (MinDepth(root.left) + MinDepth(root.right) + 1);  //只有一个有节点，所以另一个一定为0
        }
        else
        {
            return Math.Min(MinDepth(root.left) , MinDepth(root.right)) + 1;//返回较小的一个
        }
    }
}

/// <summary>
/// 题号：112. 路径总和
/// 题目：
/// 给定一个二叉树和一个目标和，判断该树中是否存在根节点到叶子节点的路径，这条路径上所有节点值相加等于目标和。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例:给定如下二叉树，以及目标和 sum = 22，
///           5
///          / \
///         4   8
///        /   / \
///       11  13  4
///      /  \    / \
///     7    2  5   1
/// 返回:true:5,4,11,2
/// 
/// 二叉树
/// 深度优先搜索
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public bool HasPathSum(TreeNode root, int sum)
    {
        if (root == null) return false;
        return DFS(root, sum);
    }

    private bool DFS(TreeNode root, int sum)
    {
        if (root.left == null && root.right == null)
        {
            if (root.val == sum)
            {
                return true;
            }
            return false;
        }
        sum -= root.val;
        if (root.right == null)
        {
            return DFS(root.left, sum);
        }
        if (root.left == null)
        {
            return DFS(root.right, sum);
        }
        return DFS(root.left, sum) || DFS(root.right, sum);
    }
}

/// <summary>
/// 题号：113. 路径总和 II
/// 题目：
/// 给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例:
/// 给定如下二叉树，以及目标和 sum = 22，
///           5
///          / \
///         4   8
///        /   / \
///       11  13  4
///      /  \    / \
///     7    2  5   1
/// 返回:
/// [   [5,4,11,2],   [5,8,4,5]]
/// 
/// 二叉树
/// DFS
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> PathSum(TreeNode root, int sum)
    {
        if (root == null) return result;
        IList<int> temp = new List<int>();
        DFS(root, sum, temp);
        return result;
    }

    private void DFS(TreeNode root, int sum, IList<int> temp)
    {
        //注意一般都用自身为null为终止条件
        if (root == null)
        {
            return;
        }

        temp.Add(root.val);
        sum -= root.val;

        if (root.left == null && root.right == null)
        {
            if (sum == 0)
            {
                result.Add(temp.ToList<int>());
            }
        }

        if (root.left != null)
        {
            DFS(root.left, sum, temp);
        }
        if (root.right != null)
        {
            DFS(root.right, sum, temp);
        }

        temp.RemoveAt(temp.Count - 1);
    }
}

/// <summary>
/// 题号：116. 填充每个节点的下一个右侧节点指针
/// 题目：
/// 给定一个完美二叉树，其所有叶子节点都在同一层，每个父节点都有两个子节点。二叉树定义如下：
/// struct Node
/// {
/// int val;
/// Node* left;
/// Node* right;
/// Node* next;
/// }
/// 填充它的每个 next 指针，让这个指针指向其下一个右侧节点。如果找不到下一个右侧节点，则将 next 指针设置为 NULL。
/// 初始状态下，所有 next 指针都被设置为 NULL。
/// 进阶：
/// 你只能使用常量级额外空间。
/// 使用递归解题也符合要求，本题中递归程序占用的栈空间不算做额外的空间复杂度。
/// 
/// 二叉树
/// 层次遍历
/// 链表
/// 
/// 法二：使用自身的next指针
/// </summary>
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;
    public Node next;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next) {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}
*/

//法一
public class Solution
{
    public Node Connect(Node root)
    {
        if (root == null) return null;

        Queue<Node> temp = new Queue<Node>();
        temp.Enqueue(root);
        while (temp.Count != 0)
        {
            int size = temp.Count;
            Node last = null;   //最后一位

            for (int i = 0; i < size; i++)
            {
                Node m = temp.Dequeue();
                if (m.left != null) temp.Enqueue(m.left);
                if (m.right != null) temp.Enqueue(m.right);
                //如果pre为空就表示node节点是这一行的第一个，
                //没有前一个节点指向他，否则就让前一个节点指向他
                if (last != null)
                {
                    last.next = m;
                }
                //然后再让当前节点成为前一个节点
                last = m;
            }
        }
        return root;
    }
}

//法二
public class Solution
{
    public Node Connect(Node root)
    {
        if (root == null) return null;

        Node temp = root;
        //左边有节点时
        while (temp.left != null)
        {
            Node temp2 = temp;
            //自身有节点时
            while (temp2 != null)
            {
                //左连右
                temp2.left.next = temp2.right;
                //右连后续
                if (temp2.next != null)
                {
                    temp2.right.next = temp2.next.left;
                }

                //后移
                temp2 = temp2.next;
            }

            //去下一层
            temp = temp.left;
        }
        return root;
    }
}

/// <summary>
/// 题号：117. 填充每个节点的下一个右侧节点指针 II
/// 题目：
/// 给定一个二叉树
/// struct Node
/// {
/// int val;
/// Node* left;
/// Node* right;
/// Node* next;
/// }
/// 填充它的每个 next 指针，让这个指针指向其下一个右侧节点。如果找不到下一个右侧节点，则将 next 指针设置为 NULL。
/// 初始状态下，所有 next 指针都被设置为 NULL。
/// 进阶：
/// 你只能使用常量级额外空间。
/// 使用递归解题也符合要求，本题中递归程序占用的栈空间不算做额外的空间复杂度。
/// 示例：
/// 输入：root = [1,2,3,4,5,null,7]
/// 输出：[1,#,2,3,#,4,5,7,#]
/// 解释：给定二叉树如图 A 所示，你的函数应该填充它的每个 next 指针，以指向其下一个右侧节点，如图 B 所示。
/// 提示：
/// 树中的节点数小于 6000
/// -100 <= node.val <= 100
/// 
/// 二叉树
///层次遍历
///链表
/// </summary>
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;
    public Node next;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next) {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}
*/

public class Solution
{
    public Node Connect(Node root)
    {
        if (root == null) return null;

        Queue<Node> temp = new Queue<Node>();
        temp.Enqueue(root);
        while (temp.Count != 0)
        {
            int size = temp.Count;
            Node last = null;   //最后一位

            for (int i = 0; i < size; i++)
            {
                Node m = temp.Dequeue();
                if (m.left != null) temp.Enqueue(m.left);
                if (m.right != null) temp.Enqueue(m.right);
                //如果pre为空就表示node节点是这一行的第一个，
                //没有前一个节点指向他，否则就让前一个节点指向他
                if (last != null)
                {
                    last.next = m;
                }
                //然后再让当前节点成为前一个节点
                last = m;
            }
        }
        return root;
    }
}

/// <summary>
/// 题号：118. 杨辉三角
/// 题目：
/// 给定一个非负整数 numRows，生成杨辉三角的前 numRows 行。
/// 在杨辉三角中，每个数是它左上方和右上方的数的和。
/// 示例:
/// 输入: 5
/// 输出:
/// [
/// [1],
/// [1,1],
/// [1,2,1],
/// [1,3,3,1],
/// [1,4,6,4,1]
/// ]
/// </summary>
public class Solution
{
    public IList<IList<int>> Generate(int numRows)
    {
        IList<IList<int>> result = new List<IList<int>>();

        if (numRows == 0) return result;

        int[][] dp = new int[numRows + 1][];

        dp[0] = new int[0];
        for (int i = 1; i <= numRows; i++)
        {
            dp[i] = All(i, dp[i - 1]);
            result.Add(dp[i]);
        }
        return result;
    }

    private int[] All(int num, int[] temp)
    {
        int[] result = new int[num];
        result[0] = 1;
        result[num - 1] = 1;
        if (num <= 2) return result;

        int index = 1;
        while (index <= num / 2)
        {
            int sum = temp[index - 1] + temp[index];
            result[index] = sum;
            result[num - 1 - index] = sum;
            index++;
        }
        return result;
    }
}

/// <summary>
/// 题号：119. 杨辉三角 II
/// 题目：
/// 给定一个非负索引 k，其中 k ≤ 33，返回杨辉三角的第 k 行。
/// 在杨辉三角中，每个数是它左上方和右上方的数的和。
/// </summary>
public class Solution
{
    public IList<int> GetRow(int rowIndex)
    {
        IList<int> result = new List<int>();

        int[][] dp = new int[rowIndex + 1][];

        dp[0] = All(1, new int[0]);
        for (int i = 1; i <= rowIndex; i++)
        {
            dp[i] = All(i + 1, dp[i - 1]);
        }
        return dp[rowIndex].ToList();
    }

    private int[] All(int num, int[] temp)
    {
        int[] result = new int[num];
        result[0] = 1;
        result[num - 1] = 1;
        if (num <= 2) return result;

        int index = 1;
        while (index <= num / 2)
        {
            int sum = temp[index - 1] + temp[index];
            result[index] = sum;
            result[num - 1 - index] = sum;
            index++;
        }
        return result;
    }
}

/// <summary>
/// 题号：121. 买卖股票的最佳时机
/// 题目：
/// 给定一个数组，它的第 i 个元素是一支给定股票第 i 天的价格。
/// 如果你最多只允许完成一笔交易（即买入和卖出一支股票一次），设计一个算法来计算你所能获取的最大利润。
/// 注意：你不能在买入股票前卖出股票。
/// 示例 1:
/// 输入:[7,1,5,3,6,4]
/// 输出: 5
/// 解释: 在第 2 天（股票价格 = 1）的时候买入，在第 5 天（股票价格 = 6）的时候卖出，最大利润 = 6 - 1 = 5 。
/// 注意利润不能是 7 - 1 = 6, 因为卖出价格需要大于买入价格；同时，你不能在买入前卖出股票。
/// 示例 2:
/// 
/// 输入:[7,6,4,3,1]
/// 输出: 0
/// 解释: 在这种情况下, 没有交易完成, 所以最大利润为 0。
/// 
/// 历史最低点
/// 利润比较
/// </summary>
public class Solution
{
    public int MaxProfit(int[] prices)
    {
        //求取每次历史最低点时对应的最大利润
        int minprice = int.MaxValue;
        int maxprofit = 0;
        for (int i = 0; i < prices.Length; i++)
        {
            //如果当前价格比历史最低点还低，就更新
            if (prices[i] < minprice)
            {
                minprice = prices[i];
            }
            else
            {
                //在当前历史最低点情况下不断更新最大利润
                if (prices[i] - minprice > maxprofit)
                {
                    maxprofit = prices[i] - minprice;
                }
            }
        }
        return maxprofit;
    }
}

/// <summary>
/// 题号：127. 单词接龙
/// 题目：
/// 给定两个单词（beginWord 和 endWord）和一个字典，找到从 beginWord 到 endWord 的最短转换序列的长度。转换需遵循如下规则：
/// 每次转换只能改变一个字母。
/// 转换过程中的中间单词必须是字典中的单词。
/// 说明:
/// 如果不存在这样的转换序列，返回 0。
/// 所有单词具有相同的长度。
/// 所有单词只由小写字母组成。
/// 字典中不存在重复的单词。
/// 你可以假设 beginWord 和 endWord 是非空的，且二者不相同。
/// 示例 1:
/// 输入:
/// beginWord = "hit",
/// endWord = "cog",
/// wordList = ["hot", "dot", "dog", "lot", "log", "cog"]
/// 输出: 5
/// 解释: 一个最短转换序列是 "hit"-> "hot"-> "dot"-> "dog"-> "cog",
/// 返回它的长度 5。
/// 示例 2:
/// 输入:
/// beginWord = "hit"
/// endWord = "cog"
/// wordList = ["hot", "dot", "dog", "lot", "log"]
/// 输出: 0
/// 解释: endWord "cog" 不在字典中，所以无法进行转换。
/// </summary>
/// 结果超时了，需要双向BFS才行，看官网代码
public class Solution
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        int n = wordList.Count;
        if (n == 0) return 0;
        if (beginWord.Length != endWord.Length) return 0;

        //使用过的肯定不再使用了
        bool[] used = new bool[n];
        Dictionary<string, HashSet<int>> wordHash = new Dictionary<string, HashSet<int>>();
        //原来的对应可变化情况，对应变化序号
        wordHash.Add(beginWord, new HashSet<int>());
        for (int i = 0; i < n; i++)
        {
            if (wordHash.ContainsKey(wordList[i])) continue;
            wordHash.Add(wordList[i], new HashSet<int>());
        }

        if (!wordHash.ContainsKey(endWord)) return 0;

        //原来的对应可变化情况，对应变化序号
        for (int i = 0; i < n; i++)
        {
            if (Match(beginWord, wordList[i])) wordHash[beginWord].Add(i);
        }

        //字典内部变化情况，对应变化序号
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                if (wordHash[wordList[i]].Contains(j)) continue;
                if (wordHash[wordList[j]].Contains(i)) continue;
                if (Match(wordList[i], wordList[j]))
                {
                    wordHash[wordList[i]].Add(j);
                    wordHash[wordList[j]].Add(i);
                }
            }
        }

        //如果有从1开始
        return BFS(beginWord, endWord, wordList, used, wordHash, 1);
    }

    private bool Match(string str1, string str2)
    {
        int n = str1.Length;
        int num = 0;
        for (int i = 0; i < n; i++)
        {
            if (str1[i] != str2[i]) num++;
            if (num == 2) return false;
        }
        return num == 1 ? true : false;
    }

    //使用广搜确定最小值
    private int BFS(string matchWord, string targetWord, IList<string> wordList, bool[] used, Dictionary<string, HashSet<int>> wordHash, int result)
    {
        Queue<string> que = new Queue<string>();
        que.Enqueue(matchWord);
        while (que.Count != 0)
        {
            int size = que.Count;
            for (int i = 0; i < size; i++)
            {
                string temp = que.Dequeue();
                if (temp == targetWord) return result;
                foreach (int index in wordHash[temp])
                {
                    if (used[index]) continue;
                    used[index] = true;
                    que.Enqueue(wordList[index]);
                }
            }
            result++;
        }

        //全部使用过，肯定没结果
        return 0;
    }
}

/// <summary>
/// 题号：129. 求根到叶子节点数字之和
/// 题目：
/// 给定一个二叉树，它的每个结点都存放一个 0-9 的数字，每条从根到叶子节点的路径都代表一个数字。
/// 例如，从根到叶子节点路径 1->2->3 代表数字 123。
/// 计算从根到叶子节点生成的所有数字之和。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例 1:
/// 输入:[1,2,3]
/// 1
/// / \
/// 2   3
/// 输出: 25
/// 解释:
/// 从根到叶子节点路径 1->2 代表数字 12.
/// 从根到叶子节点路径 1->3 代表数字 13.
/// 因此，数字总和 = 12 + 13 = 25.
/// 示例 2:
/// 输入:[4,9,0,5,1]
/// 4
/// / \
/// 9   0
/// / \
/// 5   1
/// 输出: 1026
/// 解释:
/// 从根到叶子节点路径 4->9->5 代表数字 495.
/// 从根到叶子节点路径 4->9->1 代表数字 491.
/// 从根到叶子节点路径 4->0 代表数字 40.
/// 因此，数字总和 = 495 + 491 + 40 = 1026.
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public int SumNumbers(TreeNode root)
    {
        if (root == null) return 0;
        return DFS(root, 0);
    }

    private int DFS(TreeNode root, int val)
    {
        //先赋值
        int temp = val * 10 + root.val;

        //左右子树均空，才为最终的
        if (root.left == null && root.right == null) return temp;

        if (root.left == null) return DFS(root.right, temp);
        if (root.right == null) return DFS(root.left, temp);
        return DFS(root.left, temp) + DFS(root.right, temp);
    }
}

/// <summary>
/// 题号：134. 加油站
/// 题目：
/// 在一条环路上有 N 个加油站，其中第 i 个加油站有汽油 gas[i] 升。
/// 你有一辆油箱容量无限的的汽车，从第 i 个加油站开往第 i+1 个加油站需要消耗汽油 cost[i] 升。你从其中的一个加油站出发，开始时油箱为空。
/// 如果你可以绕环路行驶一周，则返回出发时加油站的编号，否则返回 - 1。
/// 说明:
/// 如果题目有解，该答案即为唯一答案。
/// 输入数组均为非空数组，且长度相同。
/// 输入数组中的元素均为非负数。
/// 示例 1:
/// 输入:
/// gas = [1, 2, 3, 4, 5]
/// cost = [3, 4, 5, 1, 2]
/// 输出: 3
/// 解释:
/// 从 3 号加油站(索引为 3 处)出发，可获得 4 升汽油。此时油箱有 = 0 + 4 = 4 升汽油
/// 开往 4 号加油站，此时油箱有 4 - 1 + 5 = 8 升汽油
/// 开往 0 号加油站，此时油箱有 8 - 2 + 1 = 7 升汽油
/// 开往 1 号加油站，此时油箱有 7 - 3 + 2 = 6 升汽油
/// 开往 2 号加油站，此时油箱有 6 - 4 + 3 = 5 升汽油
/// 开往 3 号加油站，你需要消耗 5 升汽油，正好足够你返回到 3 号加油站。
/// 因此，3 可为起始索引。
/// 示例 2:
/// 输入:
/// gas = [2, 3, 4]
/// cost = [3, 4, 3]
/// 输出: -1
/// 解释:
/// 你不能从 0 号或 1 号加油站出发，因为没有足够的汽油可以让你行驶到下一个加油站。
/// 我们从 2 号加油站出发，可以获得 4 升汽油。 此时油箱有 = 0 + 4 = 4 升汽油
/// 开往 0 号加油站，此时油箱有 4 - 3 + 2 = 3 升汽油
/// 开往 1 号加油站，此时油箱有 3 - 3 + 3 = 3 升汽油
/// 你无法返回 2 号加油站，因为返程需要消耗 4 升汽油，但是你的油箱只有 3 升汽油。
/// 因此，无论怎样，你都不可能绕环路行驶一周。
/// </summary>
/// 每次判定是否不够下一点
public class Solution
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int n = gas.Length;
        if (n == 0) return -1;
        int[] remain = new int[n];

        //判断是不是不够
        int sum_gas = 0;
        int sum_cost = 0;
        for (int i = 0; i < n; i++)
        {
            remain[i] = gas[i] - cost[i];
            sum_gas += gas[i];
            sum_cost += cost[i];
        }
        if (sum_gas < sum_cost) return -1;

        if (n == 1) return 0;

        for (int i = 0; i < n; i++)
        {
            if (remain[i] > 0)
            {
                if (Can(remain, i, remain[i]))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private bool Can(int[] remain, int index, int sum)
    {
        int temp = (index + 1) % remain.Length;
        while (temp != index)
        {
            if (sum < 0) return false;
            if (temp == remain.Length) temp = 0;
            sum += remain[temp];
            temp = (temp + 1) % remain.Length;
        }
        return true;
    }
}

/// 找出最低点
public class Solution
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int n = gas.Length;
        if (n == 0) return -1;

        int minnum = int.MaxValue;
        int sum = 0;
        int minindex = -1;

        for (int i = 0; i < n; i++)
        {
            sum += gas[i] - cost[i];
            if (sum < minnum)
            {
                minnum = sum;
                minindex = i;
            }
        }
        return sum < 0 ? -1 : (minindex + 1) % n;
    }
}

/// <summary>
/// 题号：136. 只出现一次的数字
/// 题目：
/// 给定一个非空整数数组，除了某个元素只出现一次以外，其余每个元素均出现两次。找出那个只出现了一次的元素。
/// 说明：
/// 你的算法应该具有线性时间复杂度。 你可以不使用额外空间来实现吗？
/// 示例 1:
/// 输入:[2,2,1]
/// 输出: 1
/// 示例 2:
/// 输入:[4,1,2,1,2]
/// 输出: 4
/// 
/// 哈希表
/// </summary>
public class Solution
{
    public int SingleNumber(int[] nums)
    {
        int n = nums.Length;
        if (n == 0) return 0;
        Dictionary<int, int> Hash = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            if (Hash.ContainsKey(nums[i]))
            {
                Hash[nums[i]]++;
            }
            else
            {
                Hash.Add(nums[i], 1);
            }
        }
        foreach (int key in Hash.Keys)
        {
            if (Hash[key] == 1)
            {
                return key;
            }
        }
        return 0;
    }
}

//法二 ： 无额外空间
//异或
//任何一个数与自身异或等于0
//与0异或的任意一个数等于自身
//所以将所有数字都异或起来，因为其他的都是两个数，所以剩下的为单独那个
public class Solution
{
    public int SingleNumber(int[] nums)
    {
        int result = 0;
        foreach (int num in nums)
        {
            //异或符号
            result ^= num;
        }
        return result;
    }
}

/// <summary>
/// 题号：139. 单词拆分
/// 题目：
/// 给定一个非空字符串 s 和一个包含非空单词的列表 wordDict，判定 s 是否可以被空格拆分为一个或多个在字典中出现的单词。
/// 说明：
/// 拆分时可以重复使用字典中的单词。
/// 你可以假设字典中没有重复的单词。
/// 示例 1：
/// 输入: s = "leetcode", wordDict = ["leet", "code"]
/// 输出: true
/// 解释: 返回 true 因为 "leetcode" 可以被拆分成 "leet code"。
/// 示例 2：
/// 输入: s = "applepenapple", wordDict = ["apple", "pen"]
/// 输出: true
/// 解释: 返回 true 因为 "applepenapple" 可以被拆分成 "apple pen apple"。
/// 注意你可以重复使用字典中的单词。
/// 示例 3：
/// 输入: s = "catsandog", wordDict = ["cats", "dog", "sand", "and", "cat"]
/// 输出: false
/// </summary>
public class Solution
{
    public bool WordBreak(string s, IList<string> wordDict)
    {
        int n = s.Length;
        if (n == 0) return true;
        if (wordDict.Count == 0) return false;

        HashSet<string> wordHash = new HashSet<string>();
        foreach (string str in wordDict) wordHash.Add(str);

        bool[] dp = new bool[n + 1];
        //空集肯定对
        dp[0] = true;
        //dp[i] = dp[j] + Check(j+1,i)
        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (dp[j] && wordHash.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    break;
                }
            }
        }

        return dp[n];
    }
}

/// <summary>
/// 题号：140. 单词拆分 II
/// 题目：
/// 给定一个非空字符串 s 和一个包含非空单词列表的字典 wordDict，在字符串中增加空格来构建一个句子，使得句子中所有的单词都在词典中。返回所有这些可能的句子。
/// 说明：
/// 分隔时可以重复使用字典中的单词。
/// 示例 1：
/// 输入:
/// s = "catsanddog"
/// wordDict = ["cat", "cats", "and", "sand", "dog"]
/// 输出:
/// [
/// "cats and dog",
/// "cat sand dog"
/// ]
/// 示例 2：
/// 输入:
/// s = "pineapplepenapple"
/// wordDict = ["apple", "pen", "applepen", "pine", "pineapple"]
/// 输出:
/// [
/// "pine apple pen apple",
/// "pineapple pen apple",
/// "pine applepen apple"
/// ]
/// 解释: 注意你可以重复使用字典中的单词。
/// 示例 3：
/// 输入:
/// s = "catsandog"
/// wordDict = ["cats", "dog", "sand", "and", "cat"]
/// 输出:
/// []
/// </summary>
public class Solution
{
    IList<string> result = new List<string>();
    List<string> temp = new List<string>();

    public IList<string> WordBreak(string s, IList<string> wordDict)
    {
        HashSet<string> words = new HashSet<string>();
        for (int i = 0; i < wordDict.Count; i++)
        {
            words.Add(wordDict[i]);
        }

        //动态规划判定是否存在
        bool[] dp = new bool[s.Length + 1];
        //空字符串肯定匹配
        dp[0] = true;

        //dp[i] = dp[j]+Check(j+1,i);
        //cheack()检查是否在hash里
        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (dp[j] && words.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    break;
                }
            }
        }

        if (dp[s.Length])
        {
            Match(s, words, 0);
            return result;
        }
        return result;
    }

    private void Match(string s, HashSet<string> words, int index)
    {
        int n = s.Length;
        string wordtemp = "";

        if (index == n)
        {
            result.Add(string.Join(" ", temp.ToArray()));
            return;
        }

        //从指定位置往后数，看是否再字典中
        for (int i = index; i < n; i++)
        {
            wordtemp += s[i];
            if (words.Contains(wordtemp))
            {
                temp.Add(wordtemp);
                Match(s, words, i + 1);
                //撤销
                temp.RemoveAt(temp.Count - 1);
            }
        }
        return;
    }
}

/// <summary>
/// 题号：141. 环形链表
/// 题目：
/// 翻转一棵二叉树。
/// 给定一个链表，判断链表中是否有环。
/// 如果链表中有某个节点，可以通过连续跟踪 next 指针再次到达，则链表中存在环。 为了表示给定链表中的环，我们使用整数 pos 来表示链表尾连接到链表中的位置（索引从 0 开始）。 
/// 如果 pos 是 -1，则在该链表中没有环。注意：pos 不作为参数进行传递，仅仅是为了标识链表的实际情况。
/// 如果链表中存在环，则返回 true 。 否则，返回 false 。
/// 进阶：
/// 你能用 O(1)（即，常量）内存解决此问题吗？
/// 示例 1：
/// 输入：head = [3, 2, 0, -4], pos = 1
/// 输出：true
/// 解释：链表中有一个环，其尾部连接到第二个节点。
/// 示例 2：
/// 输入：head = [1, 2], pos = 0
/// 输出：true
/// 解释：链表中有一个环，其尾部连接到第一个节点。
/// 示例 3：
/// 输入：head = [1], pos = -1
/// 输出：false
/// 解释：链表中没有环。
/// 提示：
/// 链表中节点的数目范围是[0, 104]
/// - 105 <= Node.val <= 105
/// pos 为 -1 或者链表中的一个 有效索引 。
/// 
/// 快慢指针
/// 快指针每次走两步，慢指针每次走一步
/// 如果有环，肯定在某一时刻相遇
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution
{
    public bool HasCycle(ListNode head)
    {
        if (head == null || head.next == null) return false;
        ListNode slow = head;
        ListNode fast = head.next;
        while (slow != fast)
        {
            if (fast.next == null || fast.next.next == null) return false;
            slow = slow.next;
            fast = fast.next.next;
        }
        return true; //相遇了
    }
}

/// <summary>
/// 题号：142. 环形链表 II
/// 题目：
/// 给定一个链表，返回链表开始入环的第一个节点。 如果链表无环，则返回 null。
/// 为了表示给定链表中的环，我们使用整数 pos 来表示链表尾连接到链表中的位置（索引从 0 开始）。 如果 pos 是 -1，则在该链表中没有环。
/// 说明：不允许修改给定的链表。
/// 示例 1：
/// 输入：head = [3,2,0,-4], pos = 1
/// 输出：tail connects to node index 1
/// 解释：链表中有一个环，其尾部连接到第二个节点。
/// 示例 2：
/// 输入：head = [1,2], pos = 0
/// 输出：tail connects to node index 0
/// 解释：链表中有一个环，其尾部连接到第一个节点。
/// 示例 3：
/// 输入：head = [1], pos = -1
/// 输出：no cycle
/// 解释：链表中没有环。
/// 进阶：
/// 你是否可以不用额外空间解决此题？
/// 
/// 哈希表
/// 
/// 快慢指针：原理见leedcode
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
//哈希表
public class Solution
{
    public ListNode DetectCycle(ListNode head)
    {
        HashSet<ListNode> map = new HashSet<ListNode>();
        while (head != null)
        {
            if (map.Contains(head))
            {
                return head;
            }
            map.Add(head);
            head = head.next;
        }
        return null;
    }
}

//快慢指针
public class Solution
{
    public ListNode DetectCycle(ListNode head)
    {
        if (head == null || head.next == null) return null;
        ListNode slow = head;
        ListNode fast = head.next;
        ListNode temp = head;  //辅助节点
        while (slow != fast)
        {
            if (fast.next == null || fast.next.next == null) return null;
            slow = slow.next;
            fast = fast.next.next;
        }
        //相遇了
        //相遇点的下一位和原首位在n圈后肯定相遇
        //当slow与fast同时从首位出发的时候就是这一位
        slow = slow.next;
        while (temp != slow)
        {
            temp = temp.next;
            slow = slow.next;
        }
        return temp;
    }
}

/// <summary>
/// 题号：143. 重排链表
/// 题目：
/// 给定一个单链表 L：L0→L1→…→Ln-1→Ln ，
/// 将其重新排列后变为： L0→Ln→L1→Ln - 1→L2→Ln - 2→…
/// 你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
/// 示例 1:
/// 给定链表 1->2->3->4, 重新排列为 1->4->2->3.
/// 示例 2:
/// 给定链表 1->2->3->4->5, 重新排列为 1->5->2->4->3.
/// 
/// 快慢指针
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null) return;

        //快慢指针找中间值
        ListNode slow = head;
        ListNode fast = head.next;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        //分组
        ListNode List2 = slow.next;

        slow.next = null;
        ListNode List1 = head;

        //翻转List2
        List2 = Reverse(List2);

        //合并
        Merge(List1, List2);
        return;
    }

    private ListNode Reverse(ListNode head)
    {
        ListNode result = new ListNode(0);
        while (head != null)
        {
            ListNode temp = result.next;
            result.next = head;
            head = head.next;
            result.next.next = temp;
        }
        return result.next;
    }

    private void Merge(ListNode List1, ListNode List2)
    {
        while (List2 != null)
        {
            ListNode temp = List1.next;
            List1.next = List2;
            List2 = List2.next;
            List1.next.next = temp;
            List1 = temp;
        }
    }
}

/// <summary>
/// 题号：144. 二叉树的前序遍历
/// 题目：
/// 给定一个二叉树，返回它的 前序 遍历。
/// 示例:
/// 输入:[1,null,2,3]  
/// 1
///  \
///   2
///  /
/// 3
/// 输出:[1,2,3]
/// 进阶: 递归算法很简单，你可以通过迭代算法完成吗？
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    IList<int> result = new List<int>();

    public IList<int> PreorderTraversal(TreeNode root)
    {
        if (root == null) return result;
        Preorder(root);
        return result;
    }

    private void Preorder(TreeNode root)
    {
        if (root == null) return;
        result.Add(root.val);
        Preorder(root.left);
        Preorder(root.right);
    }
}

/// <summary>
/// 题号：147. 对链表进行插入排序
/// 题目：
/// 对链表进行插入排序。
/// 插入排序算法：
/// 插入排序是迭代的，每次只移动一个元素，直到所有元素可以形成一个有序的输出列表。
/// 每次迭代中，插入排序只从输入数据中移除一个待排序的元素，找到它在序列中适当的位置，并将其插入。
/// 重复直到所有输入数据插入完为止。
/// 示例 1：
/// 输入: 4->2->1->3
/// 输出: 1->2->3->4
/// 示例 2：
/// 输入: -1->5->3->4->0
/// 输出: -1->0->3->4->5
/// 
/// 插入排序的基本思想是，维护一个有序序列，初始时有序序列只有一个元素，每次将一个新的元素插入到有序序列中，将有序序列的长度增加 1，直到全部元素都加入到有序序列中。
/// 如果是数组的插入排序，则数组的前面部分是有序序列，每次找到有序序列后面的第一个元素（待插入元素）的插入位置，将有序序列中的插入位置后面的元素都往后移动一位，然后将待插入元素置于插入位置。
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode InsertionSortList(ListNode head)
    {
        if (head == null) return head;
        ListNode result = new ListNode(0);
        result.next = head;

        //比较指针
        ListNode left = head;
        ListNode right = head.next;
        while (right != null)
        {
            if (left.val <= right.val)
            {
                left = left.next;
            }
            else
            {
                //寻找对应的位置
                ListNode pre = result;
                //找到最后一个比right小的位置
                while (pre.next.val <= right.val)
                {
                    pre = pre.next;
                }
                //交换对应位置
                left.next = right.next;
                right.next = pre.next;
                pre.next = right;
            }
            right = left.next;
        }
        return result.next;
    }
}

/// <summary>
/// 题号：148. 排序链表
/// 题目：
/// 给你链表的头结点 head ，请将其按 升序 排列并返回 排序后的链表 。
/// 进阶：
/// 你可以在 O(n log n) 时间复杂度和常数级空间复杂度下，对链表进行排序吗？
/// 输入：head = [4,2,1,3]
/// 输出：[1,2,3,4]
/// 
/// 归并排序
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode SortList(ListNode head)
    {
        //当无节点或单节点时递归结束
        if (head == null || head.next == null)
        {
            return head;
        }
        //快慢指针找找兼职
        ListNode fast = head.next;
        ListNode slow = head;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        ListNode Mid = slow.next;
        slow.next = null;

        //递归分割
        ListNode Left = SortList(head);
        ListNode Right = SortList(Mid);

        //结果
        ListNode result = new ListNode(0);
        ListNode temp = result;

        //合并
        while (Left != null && Right != null)
        {
            if (Left.val < Right.val)
            {
                temp.next = Left;
                Left = Left.next;
            }
            else
            {
                temp.next = Right;
                Right = Right.next;
            }
            temp = temp.next;
        }
        temp.next = Left == null ? Right : Left;
        return result.next;
    }
}

/// <summary>
/// 题号：155. 最小栈
/// 题目：
/// 设计一个支持 push ，pop ，top 操作，并能在常数时间内检索到最小元素的栈。
/// push(x) —— 将元素 x 推入栈中。
/// pop() —— 删除栈顶的元素。
/// top() —— 获取栈顶元素。
/// getMin() —— 检索栈中的最小元素。
/// 示例:
/// 输入：
/// ["MinStack","push","push","push","getMin","pop","top","getMin"]
/// [[],[-2],[0],[-3],[],[],[],[]]
/// 输出：
/// [null,null,null,null,-3,null,0,-2]
/// 解释：
/// MinStack minStack = new MinStack();
/// minStack.push(-2);
/// minStack.push(0);
/// minStack.push(-3);
/// minStack.getMin(); --> 返回 - 3.
/// minStack.pop();
/// minStack.top(); --> 返回 0.
/// minStack.getMin(); --> 返回 - 2.
/// 提示：
/// pop、top 和 getMin 操作总是在 非空栈 上调用。
/// 
/// 额外创建一个辅助栈，每次都放入最小值
/// </summary>
public class MinStack
{
    Stack<int> result;
    Stack<int> temp;

    /** initialize your data structure here. */
    public MinStack()
    {
        result = new Stack<int>();
        temp = new Stack<int>();
        temp.Push(int.MaxValue);
    }

    public void Push(int x)
    {
        result.Push(x);
        //插入最小的
        temp.Push(Math.Min(x, temp.Peek()));
        return;
    }

    public void Pop()
    {
        result.Pop();
        temp.Pop();
        return;
    }

    public int Top()
    {
        return result.Peek();
    }

    public int GetMin()
    {
        return temp.Peek();
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(x);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */

/// <summary>
/// 题号：226. 翻转二叉树
/// 题目：
/// 翻转一棵二叉树。
/// 示例：
/// 输入：
/// 
///      4
///    /   \
///   2     7
///  / \   / \
/// 1   3 6   9
/// 输出：
///      4
///    /   \
///   7     2
///  / \   / \
/// 9   6 3   1
/// 
/// 递归
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null) return root;
        TreeNode left = InvertTree(root.left);
        TreeNode right = InvertTree(root.right);
        root.left = right;
        root.right = left;
        return root;
    }
}

/// <summary>
/// 题号：1114. 按序打印
/// 题目：
/// 我们提供了一个类：
/// public class Foo
/// {
/// public void first() { print("first"); }
/// public void second() { print("second"); }
/// public void third() { print("third"); }
/// }
/// 三个不同的线程将会共用一个 Foo 实例。
/// 线程 A 将会调用 first() 方法
/// 线程 B 将会调用 second() 方法
/// 线程 C 将会调用 third() 方法
/// 请设计修改程序，以确保 second() 方法在 first() 方法之后被执行，third() 方法在 second() 方法之后被执行。
/// 输入: [1,3,2]
/// 输出: "firstsecondthird"
/// 解释: 
/// 输入[1, 3, 2] 表示线程 A 将会调用 first() 方法，线程 B 将会调用 third() 方法，线程 C 将会调用 second() 方法。
/// 正确的输出是 "firstsecondthird"。
/// </summary>
public class Foo
{
    int step = 0;

    public Foo()
    {
    }

    public void First(Action printFirst)
    {

        // printFirst() outputs "first". Do not change or remove this line.
        printFirst();
        step = 1;
    }

    public void Second(Action printSecond)
    {
        while (step != 1)
        {
            Thread.Sleep(1);
        }
        // printSecond() outputs "second". Do not change or remove this line.
        printSecond();
        step = 2;
    }

    public void Third(Action printThird)
    {
        while (step != 2)
        {
            Thread.Sleep(1);
        }
        // printThird() outputs "third". Do not change or remove this line.
        printThird();
        step = 3;
    }
}

//Thread
using System.Threading;
public class Foo
{
    //判定的信号
    ManualResetEvent mr1 = new ManualResetEvent(false);
    ManualResetEvent mr2 = new ManualResetEvent(false);

    public Foo()
    {
    }

    public void First(Action printFirst)
    {
        // printFirst() outputs "first". Do not change or remove this line.
        printFirst();
        //Set()执行
        mr1.Set();
    }

    public void Second(Action printSecond)
    {
        //等待mr1执行
        mr1.WaitOne();
        printSecond();
        mr2.Set();
        // printSecond() outputs "second". Do not change or remove this line.
    }

    public void Third(Action printThird)
    {
        mr2.WaitOne();
        printThird();
        // printThird() outputs "third". Do not change or remove this line.

    }
}

/// <summary>
/// 题号：201
/// 题目：
/// 给定范围 [m, n]，其中 0 <= m <= n <= 2147483647，返回此范围内所有数字的按位与（包含 m, n 两端点）。
/// 输入: [5,7]
/// 输出: 4
/// 
/// 按位与：求最前位相等
/// 将两个数字分别向右移，直到相等，返回的值后面补上需要的移动的0
/// </summary>
public class Solution
{
    public int RangeBitwiseAnd(int m, int n)
    {
        int num = 0;     //移动的位数
        int result = 0;

        while (m < n)
        {
            m = m >> 1;  //右移一位
            //例4:100   变为2:10
            n = n >> 1;
            num++;
        }

        //两者相等即前面相等
        result = n << num;
        return result;
    }
}

/// <summary>
/// 206. 反转链表
/// 题目：
/// 反转一个单链表。
/// 示例:
/// 输入: 1->2->3->4->5->NULL
/// 输出: 5->4->3->2->1->NULL
/// 
/// 链表
/// 双指针
/// </summary>
/**
*Definition for singly - linked list.

* public class ListNode
{
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
*/
public class Solution
{
    public ListNode ReverseList(ListNode head)
    {
        if (head == null) return head;
        ListNode result = new ListNode(0);
        ListNode p = head;

        while (p != null)
        {
            ListNode temp = result.next;
            result.next = p;
            p = p.next;
            result.next.next = temp;
        }

        return result.next;
    }
}

/// <summary>
/// 题号：214. 最短回文串
/// 题目：
/// 给定一个字符串 s，你可以通过在字符串前面添加字符将其转换为回文串。找到并返回可以用这种方式转换的最短回文串。
/// 输入: "aacecaaa"
/// 输出: "aaacecaaa"
/// 
/// KMP
/// 马拉车
/// 回文
/// </summary>
public class Solution
{
    //自己做法
    //翻转字符串，找出第一个相等字母
    public string ShortestPalindrome(string s)
    {
        string result = s;   //复制一份s
        string temp2 = s;  //比较字符串
        s = Reverse(s);   //翻转s

        int i = 0;       //插入位置

        while (temp2 != s && s.Length > 0)
        {
            //去掉首项并保存
            result = result.Insert(i, s[0].ToString());
            i++;
            s = s.Remove(0, 1);
            temp2 = temp2.Remove(temp2.Length - 1, 1);
        }

        return result;
    }

    //翻转字符串
    private string Reverse(string s)
    {
        int i = 0;
        int j = s.Length - 1;
        while (i < j)
        {
            s = Swap(s, i, j);
            i++;
            j--;
        }
        return s;
    }

    //换位置
    private string Swap(string s, int i, int j)
    {
        char temp1 = s[i];
        char temp2 = s[j];
        s = s.Remove(i, 1).Insert(i, temp2.ToString());
        s = s.Remove(j, 1).Insert(j, temp1.ToString());
        return s;
    }
}

//使用KMP找含首项的最长回文子串
public class Solution
{
    public string ShortestPalindrome(string s)
    {
        //字符串交换位置
        //快的方法
        char[] m = s.ToArray();
        Array.Reverse(m);
        string rev_s = string.Join("", m);

        //自定义慢的方法
        //string rev_s = Reverse(s);
        string temp = s + "#" + rev_s;

        //求最长公共前后缀
        int[] KMPnum = KMP(temp);
        int num = KMPnum[temp.Length - 1];

        //对应前缀
        string qian = rev_s.Substring(0, s.Length - num);

        return qian + s;
    }

    //翻转字符串
    private string Reverse(string s)
    {
        int i = 0;
        int j = s.Length - 1;
        while (i < j)
        {
            s = Swap(s, i, j);
            i++;
            j--;
        }
        return s;
    }

    //换位置
    private string Swap(string s, int i, int j)
    {
        char temp1 = s[i];
        char temp2 = s[j];
        s = s.Remove(i, 1).Insert(i, temp2.ToString());
        s = s.Remove(j, 1).Insert(j, temp1.ToString());
        return s;
    }

    //KMP模板  背一下
    public int[] KMP(string s)
    {
        int[] next = new int[s.Length];  //保存next数组，存前后的相同项个数
        next[0] = 0;                     //next数组第一位一定为0
        int m = 0;                       //表示next[x-1]位的值
                                         //因为有时候不同时需要改变i的值，但不能改变next[i-1]的值
                                         //其实也是对应的原本字符串对应的位置
        int j = 1;                       //此时比较的位置 也是next数组的下标

        while (j < s.Length)
        {
            if (s[j] == s[m])
            {
                //当此时指向的位置上的值与原来字符串上的值相等时
                //next数组必然等于前一位加1
                m++;
                next[j] = m;
                j++;
            }
            else
            {
                if (m != 0)
                {
                    //当此时对应的next数组上有值时  前缩字符串，找对应的公共前后缀
                    //m对应为s[m-1]
                    m = next[m - 1];
                }
                else
                {
                    //m已经为0了，不能再缩小
                    next[j] = 0;
                    j++;
                }
            }
        }

        return next;
    }
}

/// <summary>
/// 题号：216. 组合总和 III
/// 题目：
/// 找出所有相加之和为 n 的 k 个数的组合。组合中只允许含有 1 - 9 的正整数，并且每种组合中不存在重复的数字。
/// 说明：
/// 所有数字都是正整数。
/// 解集不能包含重复的组合。 
/// 输入: k = 3, n = 7
/// 输出: [[1,2,4]]
/// 
/// dfs
/// 回溯
/// 剪枝
/// </summary>
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> CombinationSum3(int k, int n)
    {
        if (k == 0) return result;

        int[] nums = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Stack<int> temp = new Stack<int>();

        DFS(k, n, temp, nums, 0);

        return result;
    }

    private void DFS(int k, int n, Stack<int> temp, int[] nums, int i)
    {
        if (k == 0 && n == 0)
        {
            result.Add(temp.ToList<int>());
            return;
        }

        if (k == 0 || n == 0)
        {
            //当k，n有一个为0一个不为0时一定不可能
            return;
        }

        for (int m = i; m < nums.Length; m++)
        {
            if (n - nums[m] < 0) break;
            temp.Push(nums[m]);
            DFS(k - 1, n - nums[m], temp, nums, m + 1);
            temp.Pop();
            //k不用回归
            //k++;
        }
    }
}

/// <summary>
/// 题号：235. 二叉搜索树的最近公共祖先
/// 题目：
/// 给定一个二叉搜索树, 找到该树中两个指定节点的最近公共祖先。
/// 百度百科中最近公共祖先的定义为：“对于有根树 T 的两个结点 p、q，最近公共祖先表示为一个结点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”
/// 例如，给定如下二叉搜索树:  root = [6, 2, 8, 0, 4, 7, 9, null, null, 3, 5]
/// 输入: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
/// 输出: 6
/// 解释: 节点 2 和节点 8 的最近公共祖先是 6。
/// 
/// 最近公共祖先：两个节点在左右两侧或一个就是祖先
/// 二叉搜索性质
/// 二叉搜索树：当两个值都比根节点大，则肯定为右子树
/// 小的时候肯定为左子树，否则就为最近公共祖先
/// 
/// 二叉搜索树性质
/// 递归
/// </summary>
//解法一：递归
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q) return root;
        if (root.val > p.val && root.val > q.val) root = LowestCommonAncestor(root.left, p, q);
        if (root.val < p.val && root.val < q.val) root = LowestCommonAncestor(root.right, p, q);
        return root;
    }
}
//解法二：非递归
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        //如果值相减为正，则肯定在两侧
        while ((root.val - p.val) * (root.val - q.val) > 0)
        {
            root = root.val > p.val ? root.left : root.right;
        }
        //为负就在两侧即公共祖先
        return root;
    }
}

/// <summary>
/// 题号：236. 二叉树的最近公共祖先
/// 题目：
/// 给定一个二叉树, 找到该树中两个指定节点的最近公共祖先。
/// 百度百科中最近公共祖先的定义为：“对于有根树 T 的两个结点 p、q，最近公共祖先表示为一个结点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”
/// 输入: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
/// 输出: 3
/// 解释: 节点 5 和节点 1 的最近公共祖先是节点 3。
/// 
/// 递归
/// 后续遍历DFS
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q) return root;
        //向左右分别递归
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);
        if (left == null) return right;  //当左边为空时，p,q肯定在右侧
        if (right == null) return left;  //当右边为空时，p,q肯定在左侧
        return root;  //if(left != null && right != null) root在中间时即为公共祖先
    }
}

/// <summary>
/// 题号：257. 二叉树的所有路径
/// 题目：
/// 给定一个二叉树，返回所有从根节点到叶子节点的路径。
/// 说明: 叶子节点是指没有子节点的节点。
/// 输入:
/// 1
/// /   \
/// 2     3
/// \
/// 5
/// 输出:["1->2->5", "1->3"]
/// 
/// dfs
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    IList<string> result = new List<string>();

    public IList<string> BinaryTreePaths(TreeNode root)
    {
        if (root == null) return result;

        string temp = root.val.ToString();

        if (root.left != null)
        {
            Solve(temp, root.left);
        }

        if (root.right != null)
        {
            Solve(temp, root.right);
        }

        if (root.left == null && root.right == null)
        {
            result.Add(temp);
        }

        return result;
    }

    private void Solve(string temp, TreeNode root)
    {
        temp = temp + "->" + root.val;
        if (root.left == null && root.right == null)
        {
            result.Add(temp);
        }
        else
        {
            if (root.left != null)
            {
                Solve(temp, root.left);
            }

            if (root.right != null)
            {
                Solve(temp, root.right);
            }
        }
    }
}

/// <summary>
/// 题号：332
/// 题目：
/// 给定一个机票的字符串二维数组 [from, to]，子数组中的两个成员分别表示飞机出发和降落的机场地点，对该行程进行重新规划排序。所有这些机票都属于一个从 JFK（肯尼迪国际机场）出发的先生，所以该行程必须从 JFK 开始。
/// 说明:
/// 如果存在多种有效的行程，你可以按字符自然排序返回最小的行程组合。例如，行程["JFK", "LGA"] 与["JFK", "LGB"] 相比就更小，排序更靠前
/// 所有的机场都用三个大写字母表示（机场代码）。
/// 假定所有机票至少存在一种合理的行程。
/// 
/// 输入:[["MUC", "LHR"], ["JFK", "MUC"], ["SFO", "SJC"], ["LHR", "SFO"]]
/// 输出:["JFK", "MUC", "LHR", "SFO", "SJC"]
/// 
/// 欧拉环
/// dfs
/// 欧拉环计算使用Hierholzer 算法
/// 逆序插入无回路的路径
/// </summary>
public class Solution
{
    Dictionary<string , List<string>> map = new Dictionary<string, List<string>>();   //储存起点与对应目的地的地图
    IList<string> result = new List<string>();

    public IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        foreach (IList<string> ticket in tickets)
        {
            //如果不存在路径则添加
            if (!map.ContainsKey(ticket[0]))
            {
                map[ticket[0]] = new List<string>();
            }

            //存在路径的话就把目的地添加进去
            map[ticket[0]].Add(ticket[1]);
        }

        //按顺序排序目的地
        foreach (string item in map.Keys)
        {
            map[item].Sort();
        }

        dfs("JFK");
        return result;
    }

    //dfs示例
    private void dfs(string first)
    {
        while (map.ContainsKey(first) && map[first].Count() != 0)
        {
            //如果有目的地的话，将第一个目的地递归进行
            string temp = map[first][0];
            map[first].RemoveAt(0);

            dfs(temp);
        }

        //逆序插入
        result.Insert(0, first);
    }
}

/// <summary>
/// 题号：347. 前 K 个高频元素
/// 题目：
/// 给定一个非空的整数数组，返回其中出现频率前 k 高的元素。
/// 输入: nums = [1, 1, 1, 2, 2, 3], k = 2
/// 输出:[1,2]
/// 
/// 最小堆（不会编，如果使用C++看精选代码）
/// 桶排：使用频率作为下标排序
/// </summary>
//桶排
public class Solution
{
    public int[] TopKFrequent(int[] nums, int k)
    {
        List<int> result = new List<int>();//最后再转数组，可以简单些
                                           
        //将数组中元素按频率放进哈希表中
        Dictionary<int, int> HashMap = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (!HashMap.ContainsKey(nums[i]))
            {
                HashMap.Add(nums[i], 1);
            }
            else
            {
                HashMap[nums[i]]++;
            }
        }

        //创建一个新的数组，按照频率将数字放入数组中
        //注意，数组元素为列表，即相同频率元素放在一起
        List<int>[] temp = new List<int>[nums.Length + 1];  //比原数组多一位，因为0号位没东西
        foreach (int key in HashMap.Keys)
        {
            int n = HashMap[key];
            if (temp[n] == null)
            {
                temp[n] = new List<int>();
            }
            temp[n].Add(key);
        }

        //翻转后输出即为频率从高往低
        int m = nums.Length;
        while (m >= 0 && result.Count < k)
        {
            if (temp[m] != null)
            {
                foreach (int item in temp[m])
                {
                    result.Add(item);
                }
            }
            m--;
        }

        return result.ToArray();
    }
}

/// <summary>
/// 题号：40. 组合总和 II
/// 题目：
/// 给定一个由正整数组成且不存在重复数字的数组，找出和为给定目标正整数的组合的个数。
/// 示例:
/// nums = [1, 2, 3]
/// target = 4
/// 所有可能的组合为：(1, 1, 1, 1)(1, 1, 2)(1, 2, 1)(1, 3)(2, 1, 1)(2, 2)(3, 1)
/// 请注意，顺序不同的序列被视作不同的组合。
/// 因此输出为 7。
/// 
/// 动态规划
/// 用回溯超时了
/// 问有多少种结果或者有没有最优解时一定要用动态规划
/// 
/// 动态规划三种情况
/// 组合问题：dp[i] += dp[i-num]
/// True，False问题：dp[i] = dp[i] or dp[i-num]
/// 最大最小问题：dp[i] = min(dp[i], dp[i-num]+1)或者dp[i] = max(dp[i], dp[i-num]+1)
/// </summary>
public class Solution
{
    public int CombinationSum4(int[] nums, int target)
    {
        if (nums.Length == 0) return 0;

        //动态规划
        //dp[4] = dp[4-1] + dp[4-2] + dp[4-3] + dp[4-4]
        //4的话相当于1加上和为3的dp值，以此类推
        //dp[0]=1   只有空解一种
        int[] dp = new int[target + 1];
        dp[0] = 1;

        //i为目标值
        for (int i = 0; i < dp.Length; i++)
        {
            //j为取出的值
            for (int j = 0; j < nums.Length; j++)
            {
                if (i >= nums[j])
                {
                    dp[i] += dp[i - nums[j]];
                }
            }
        }

        return dp[target];
    }
}

/// <summary>
/// 题号：381. O(1) 时间插入、删除和获取随机元素 - 允许重复
/// 题目：
/// 设计一个支持在平均 时间复杂度 O(1) 下， 执行以下操作的数据结构。
/// 注意: 允许出现重复元素。
/// insert(val)：向集合中插入元素 val。
/// remove(val)：当 val 存在时，从集合中移除一个 val。
/// getRandom：从现有集合中随机获取一个元素。每个元素被返回的概率应该与其在集合中的数量呈线性相关。
/// 示例:
/// // 初始化一个空的集合。
/// RandomizedCollection collection = new RandomizedCollection();
/// // 向集合中插入 1 。返回 true 表示集合不包含 1 。
/// collection.insert(1);
/// // 向集合中插入另一个 1 。返回 false 表示集合包含 1 。集合现在包含 [1,1] 。
/// collection.insert(1);
/// // 向集合中插入 2 ，返回 true 。集合现在包含 [1,1,2] 。
/// collection.insert(2);
/// // getRandom 应当有 2/3 的概率返回 1 ，1/3 的概率返回 2 。
/// collection.getRandom();
/// // 从集合中删除 1 ，返回 true 。集合现在包含 [1,2] 。
/// collection.remove(1);
/// // getRandom 应有相同概率返回 1 和 2 。
/// collection.getRandom();
/// </summary>
public class RandomizedCollection
{
    List<int> result;
    //对应数字的个数
    Dictionary<int, int> val_num;
    //对应数字序号
    Dictionary<int, List<int>> val_index;
    //总个数
    int num;

    /** Initialize your data structure here. */
    public RandomizedCollection()
    {
        result = new List<int>();

        val_num = new Dictionary<int, int>();

        val_index = new Dictionary<int, List<int>>();

        num = 0;
    }

    /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
    public bool Insert(int val)
    {
        result.Add(val);
        if (!val_num.ContainsKey(val))
        {
            val_num.Add(val, 1);
            val_index.Add(val, new List<int>() { num });
            num++;
            return true;
        }
        else
        {
            val_num[val]++;
            val_index[val].Add(num);
            num++;
            if (val_num[val] == 1)
            {
                return true;
            }
            return false;
        }
    }

    /** Removes a value from the collection. Returns true if the collection contained the specified element. */
    public bool Remove(int val)
    {
        if (!val_num.ContainsKey(val))
        {
            return false;
        }
        else
        {
            if (val_num[val] == 0)
            {
                return false;
            }
            val_num[val]--;
            //将当前需要删除的数字任取一个放列表最后
            //再把最后一位对应数字序号改为当前值
            //这样后面序号不用大变
            int LastNum = result[num - 1];
            int tempindex = val_index[val][0];
            val_index[val].RemoveAt(0);
            val_index[LastNum].Add(tempindex);
            val_index[LastNum].Remove(num - 1);
            result[tempindex] = LastNum;
            result.RemoveAt(num - 1);
            num--;
            return true;
        }
    }

    /** Get a random element from the collection. */
    public int GetRandom()
    {
        Random rand = new Random();
        return result[rand.Next(num)];
    }
}

/**
* Your RandomizedCollection object will be instantiated and called as such:
* RandomizedCollection obj = new RandomizedCollection();
* bool param_1 = obj.Insert(val);
* bool param_2 = obj.Remove(val);
* int param_3 = obj.GetRandom();
*/

/// <summary>
/// 题号：394. 字符串解码
/// 题目：
/// 给定一个经过编码的字符串，返回它解码后的字符串。
/// 编码规则为: k[encoded_string]，表示其中方括号内部的 encoded_string 正好重复 k 次。注意 k 保证为正整数。
/// 你可以认为输入字符串总是有效的；输入字符串中没有额外的空格，且输入的方括号总是符合格式要求的。
/// 此外，你可以认为原始数据不包含数字，所有的数字只表示重复的次数 k ，例如不会出现像 3a 或 2[4] 的输入。
/// 示例 1：
/// 输入：s = "3[a]2[bc]"
/// 输出："aaabcbc"
/// 示例 2：
/// 输入：s = "3[a2[c]]"
/// 输出："accaccacc"
/// 示例 3：
/// 输入：s = "2[abc]3[cd]ef"
/// 输出："abcabccdcdcdef"
/// 示例 4：
/// 输入：s = "abc3[cd]xyz"
/// 输出："abccdcdcdxyz"
/// </summary>
public class Solution
{
    //下标
    int index = 0;

    public string DecodeString(string s)
    {
        int n = s.Length;
        if (n == 0) return "";

        return Decode(s, n);
    }

    private bool IsNum(char c)
    {
        if (c >= '0' && c <= '9') return true;
        return false;
    }

    private string Decode(string s, int n)
    {
        int num = 0;
        string result = "";

        while (index < n)
        {
            if (IsNum(s[index]))
            {
                num = num * 10 + s[index] - '0';
                index++;
            }
            else if (s[index] == '[')
            {
                index++;
                string temp = Decode(s, n);
                for (int i = 0; i < num; i++)
                {
                    result += temp;
                }
                num = 0;
                temp = "";
            }
            else if (s[index] == ']')
            {
                index++;
                return result;
            }
            else
            {
                result += s[index];
                index++;
            }
        }
        return result;
    }
}

/// <summary>
/// 题号：402. 移掉K位数字
/// 题目：
/// 给定一个以字符串表示的非负整数 num，移除这个数中的 k 位数字，使得剩下的数字最小。
/// 注意:
/// num 的长度小于 10002 且 ≥ k。
/// num 不会包含任何前导零。
/// 示例 1 :
/// 输入: num = "1432219", k = 3
/// 输出: "1219"
/// 解释: 移除掉三个数字 4, 3, 和 2 形成一个新的最小的数字 1219。
/// 示例 2 :
/// 输入: num = "10200", k = 1
/// 输出: "200"
/// 解释: 移掉首位的 1 剩下的数字为 200.注意输出不能有任何前导零。
/// 示例 3 :
/// 输入: num = "10", k = 2
/// 输出: "0"
/// 解释: 从原数字移除所有的数字，剩余为空就是0。
/// </summary>
/// 单调栈
/// 动态获取最小的
/// 删除临近的数值使用栈
public class Solution
{
    public string RemoveKdigits(string num, int k)
    {
        int n = num.Length;
        if (n <= k) return "0";

        Stack<char> temp = new Stack<char>();
        char[] result = new char[n - k];
        int len = result.Length;

        //不断找到更小的值
        //k个停止
        //如果还剩的话去掉最后几个
        for (int i = 0; i < n; i++)
        {
            while (k > 0 && temp.Count != 0 && temp.Peek() > num[i])
            {
                temp.Pop();
                k--;
            }
            temp.Push(num[i]);
        }

        //如果k有剩下的，去掉末尾值
        for (int i = 0; i < k; i++)
        {
            temp.Pop();
        }

        for (int i = len - 1; i >= 0; i--)
        {
            result[i] = temp.Pop();
        }
        //去掉前面的0
        int index = 0;
        while (index < len && result[index] == '0')
        {
            index++;
        }
        if (index == len) return "0";

        string result1 = String.Join("", result);
        return result1.Substring(index, len - index);
    }
}

/// <summary>
/// 题号：406. 根据身高重建队列
/// 题目：
/// 假设有打乱顺序的一群人站成一个队列。 每个人由一个整数对(h, k)表示，其中h是这个人的身高，k是排在这个人前面且身高大于或等于h的人数。 编写一个算法来重建这个队列。
/// 注意：
/// 总人数少于1100人。
/// 示例
/// 输入:
/// [[7,0], [4,4], [7,1], [5,0], [6,1], [5,2]]
/// 输出:
/// [[5,0], [7,0], [5,2], [6,1], [4,4], [7,1]]
/// </summary>
/// 从小往大依次插入，每次都找空挡
public class Solution
{
    public int[][] ReconstructQueue(int[][] people)
    {
        int n = people.Length;
        if (n == 0) return new int[0][];

        /// 从小到大排，如果相等就从大到小排
        Array.Sort(people, (a, b) =>
        {
            if (a[0] != b[0])
            {
                return a[0] - b[0];
            }
            else
            {
                return b[1] - a[1];
            }
        });

        int[][] result = new int[n][];
        foreach (int[] man in people)
        {
            int num = man[1];
            for (int i = 0; i < n; i++)
            {
                if (result[i] == null)
                {
                    num--;
                    if (num == -1)
                    {
                        result[i] = man;
                        break;
                    }
                }
            }
        }
        return result;
    }
}

/// <summary>
/// 题号：416. 分割等和子集
/// 题目：
/// 给定一个只包含正整数的非空数组。是否可以将这个数组分割成两个子集，使得两个子集的元素和相等。
/// 注意:
/// 每个数组中的元素不会超过 100
/// 数组的大小不会超过 200
/// 示例 1:
/// 
/// 输入:[1, 5, 11, 5]
/// 输出: true
/// 解释: 数组可以分割成[1, 5, 5] 和[11].
/// 示例 2:
/// 输入:[1, 2, 3, 5]
/// 
/// 输出: false
/// 解释: 数组不能分割成两个元素和相等的子集.
/// 
/// 先求和取一半后求是否有解
/// 动态规划
/// 0-1背包问题
/// 状态转移方程：
/// dp[i][j] = dp[i - 1][j] or dp[i - 1][j - nums[i]]
/// 且当nums[i] == j时，输出true
/// </summary>
public class Solution
{
    public bool CanPartition(int[] nums)
    {
        int n = nums.Length;
        if (n < 2) return false;
        int sum = 0;
        for (int i = 0; i < n; i++) sum += nums[i];
        if (sum % 2 == 1) return false;  //奇数肯定不对
        int target = sum / 2;
        Array.Sort(nums);
        //剪枝
        if (nums[n - 1] > target) return false;
        if (nums[n - 1] < target && nums[0] + nums[n - 1] > target) return false;
        //设置n行，target+1的数组
        //表示：第0位到第i位值为j的情况
        //需要考虑目标值为0的情况
        bool[,] dp = new bool[n, target + 1];
        //边界条件
        //当选择的目标值为0时，及所有都不选，肯定为真
        for (int i = 0; i < n; i++) dp[i, 0] = true;
        //当只选第一位值为自身时肯定为真
        //其余肯定为假
        dp[0, nums[0]] = true;
        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j <= target; j++)
            {
                if (j >= nums[i])
                {
                    dp[i, j] = dp[i - 1, j] || dp[i - 1, j - nums[i]];
                }
                else
                {
                    dp[i, j] = dp[i - 1, j];
                }
            }
        }
        return dp[n - 1, target];
    }
}

/// <summary>
/// 题号：459
/// 题目：
/// 给定一个非空的字符串，判断它是否可以由它的一个子串重复多次构成。给定的字符串只含有小写英文字母，并且长度不超过10000。
/// 输入: "abab"
/// 输出: True
/// 解释: 可由子字符串 "ab" 重复两次构成。
/// 
/// </summary>

/// 暴力的办法，不可行
/// 滑动窗口的操作，每次移动一位到最后面
/// 如果最终移动了字符串长度，则肯定没有重复的
public class Solution
{
    public bool RepeatedSubstringPattern(string s)
    {
        int len = s.Length;
        //建立字符串进行比对
        string temp = s;

        if (len < 2)
        {
            return false;
        }

        //只需要判断前面len/2个数是否可以重复即可，否则肯定没有重复项
        for (int i = 0; i < ( len + 1 ) / 2; i++)
        {
            char charTemp;
            charTemp = s[i];
            temp = temp + charTemp;

            if (temp.Substring(i+1 , len) == s)
            {
                return true;
            }
        }

        return false;
    }
}

/// 将字符串重复一次
/// 如果去掉第一个字符与最后一个字符的话
/// 如果还有原来的字符串为子串的话，肯定有重复
/// 假设 s 可由 子串 x 重复 n 次构成，即 s = nx
/// 则有 s+s = 2nx
/// 移除 s+s 开头和结尾的字符，变为 (s+s)[1:-1]，则破坏了开头和结尾的子串 x
/// 此时只剩 2n-2 个子串
/// 若 s 在 (s+s)[1:-1] 中，则有 2n - 2 >= n，即 n >= 2
/// 即 s 至少可由 x 重复 2 次构成
/// 否则，n < 2，n 为整数，只能取 1，说明 s 不能由其子串重复多次构成
public class Solution
{
    public bool RepeatedSubstringPattern(string s)
    {
        string temp = s + s;   //构造一个由两个字符串组成的字符串
        //去掉首尾两个字符
        temp = temp.Substring(1, temp.Length - 2);
        if (temp.Contains(s))
        {
            return true;
        }

        return false;
    }
}

/// 大神做法
public class Solution
{
    public bool RepeatedSubstringPattern(string s)
    {
        return (s + s).IndexOf(s, 1) != s.Length;
    }
}

/// <summary>
/// 题号：486. 预测赢家
/// 题目：
/// 给定一个表示分数的非负整数数组。 玩家 1 从数组任意一端拿取一个分数，随后玩家 2 继续从剩余数组任意一端拿取分数，然后玩家 1 拿，…… 。每次一个玩家只能拿取一个分数，分数被拿取之后不再可取。直到没有剩余分数可取时游戏结束。最终获得分数总和最多的玩家获胜。
/// 给定一个表示分数的数组，预测玩家1是否会成为赢家。你可以假设每个玩家的玩法都会使他的分数最大化。
/// 输入：[1, 5, 2]
/// 输出：False
/// 解释：一开始，玩家1可以从1和2中进行选择。
/// 如果他选择 2（或者 1 ），那么玩家 2 可以从 1（或者 2 ）和 5 中进行选择。如果玩家 2 选择了 5 ，那么玩家 1 则只剩下 1（或者 2 ）可选。
/// 所以，玩家 1 的最终分数为 1 + 2 = 3，而玩家 2 为 5 。
/// 因此，玩家 1 永远不会成为赢家，返回 False 。
/// 
/// 博弈论
/// </summary>
public class Solution
{
    public bool PredictTheWinner(int[] nums)
    {
        int score1 = Score(nums, 0, nums.Length - 1);
        int score2 = Sum(nums, 0, nums.Length - 1) - score1;
        return score1 >= score2;
    }

    private int Sum(int[] nums, int left, int right)
    {
        int sum = 0;
        for (int i = left; i <= right; i++)
        {
            sum += nums[i];
        }
        return sum;
    }

    //0和博弈
    //后手选最差的最优解
    private int Score(int[] nums, int left, int right)
    {
        if (left == right)
        {
            return nums[left];
        }

        return Sum(nums, left, right) - Math.Min(Score(nums, left + 1, right), Score(nums, left, right - 1));
    }
}

/// <summary>
/// 题号：514. 自由之路
/// 题目：
/// 视频游戏“辐射4”中，任务“通向自由”要求玩家到达名为“Freedom Trail Ring”的金属表盘，并使用表盘拼写特定关键词才能开门。
/// 给定一个字符串 ring，表示刻在外环上的编码；给定另一个字符串 key，表示需要拼写的关键词。您需要算出能够拼写关键词中所有字符的最少步数。
/// 最初，ring 的第一个字符与12:00方向对齐。您需要顺时针或逆时针旋转 ring 以使 key 的一个字符在 12:00 方向对齐，然后按下中心按钮，以此逐个拼写完 key 中的所有字符。
/// 旋转 ring 拼出 key 字符 key[i] 的阶段中：
/// 您可以将 ring 顺时针或逆时针旋转一个位置，计为1步。旋转的最终目的是将字符串 ring 的一个字符与 12:00 方向对齐，并且这个字符必须等于字符 key[i] 。
/// 如果字符 key[i] 已经对齐到12: 00方向，您需要按下中心按钮进行拼写，这也将算作 1 步。按完之后，您可以开始拼写 key 的下一个字符（下一阶段）, 直至完成所有拼写。
/// 示例：
/// 输入: ring = "godding", key = "gd"
/// 输出: 4
/// 解释:
/// 对于 key 的第一个字符 'g'，已经在正确的位置, 我们只需要1步来拼写这个字符。 
/// 对于 key 的第二个字符 'd'，我们需要逆时针旋转 ring "godding" 2步使它变成 "ddinggo"。
/// 当然, 我们还需要1步进行拼写。
/// 因此最终的输出是 4。
/// 提示：
/// ring 和 key 的字符串长度取值范围均为 1 至 100；
/// 两个字符串中都只有小写字符，并且均可能存在重复字符；
/// 字符串 key 一定可以由字符串 ring 旋转拼出。
/// </summary>
public class Solution
{
    public int FindRotateSteps(string ring, string key)
    {
        int ringlen = ring.Length;
        int keylen = key.Length;

        //对应值下标集合
        Dictionary<char, List<int>> Hash = new Dictionary<char, List<int>>();
        for (int i = 0; i < ringlen; i++)
        {
            if (!Hash.ContainsKey(ring[i])) Hash.Add(ring[i], new List<int>() { i });
            else Hash[ring[i]].Add(i);
        }

        //动态规划
        //dp[i][j]
        //i为key的下标
        //j为ring的下标
        //遍历每一个i-1的下标时的位置，确定继续进行需要多少步，找最少
        //当key[i-1]时，这时候ring指向k，则要求key[i]时，需要遍历所有的i-1情况，再计算需要多少步
        //dp[i][j] = Min(dp[i-1][k]+Min(abs(j-k),n-abs(j-k))+1)

        int[,] dp = new int[keylen, ringlen];
        for (int i = 0; i < keylen; i++)
        {
            for (int j = 0; j < ringlen; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        //初始化
        //到第一位需要的距离
        //ring从0到第一个下标的距离
        foreach (int i in Hash[key[0]])
        {
            dp[0, i] = Math.Min(i, ringlen - i) + 1;
        }

        for (int i = 1; i < keylen; i++)
        {
            foreach (int j in Hash[key[i]])
            {
                foreach (int k in Hash[key[i - 1]])
                {
                    dp[i, j] = Math.Min(dp[i, j], dp[i - 1, k] + Math.Min(Math.Abs(j - k), ringlen - Math.Abs(j - k)) + 1);
                }
            }
        }

        //不管从哪个方向走，找到最后一位key的最小值
        int result = int.MaxValue;
        for (int i = 0; i < ringlen; i++)
        {
            result = Math.Min(result, dp[keylen - 1, i]);
        }
        return result;
    }
}

/// <summary>
/// 题号：530. 二叉搜索树的最小绝对差
/// 题目：
/// 给你一棵所有节点为非负值的二叉搜索树，请你计算树中任意两节点的差的绝对值的最小值。
/// 示例：
/// 输入：
///    1
///     \
///      3
///     /
///    2
/// 输出：1
/// 解释：
/// 最小绝对差为 1，其中 2 和 1 的差的绝对值为 1（或者 2 和 3）。
/// 
/// 中序遍历
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    int result = int.MaxValue;
    List<int> temp = new List<int>();
    public int GetMinimumDifference(TreeNode root)
    {
        inorder(root);
        int n = temp.Count;
        for (int i = 0; i < n - 1; i++)
        {
            result = Math.Min(result, Math.Abs(temp[i] - temp[i + 1]));
        }
        return result;
    }

    private void inorder(TreeNode root)
    {
        if (root == null) return;
        inorder(root.left);
        temp.Add(root.val);
        inorder(root.right);
    }
}

/// <summary>
/// 题号：538. 把二叉搜索树转换为累加树
/// 题目：
/// 给定一个二叉搜索树（Binary Search Tree），把它转换成为累加树（Greater Tree)，使得每个节点的值是原来的节点值加上所有大于它的节点值之和。
/// 例如：
/// 输入: 原始二叉搜索树:
/// 5
/// /   \
/// 2     13
/// 输出: 转换为累加树:
/// 18
/// /   \
/// 20     13
/// 
/// 中序遍历
/// 二叉搜索树：左边比右边小
/// 就是中位查找
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    int sum = 0;
    public TreeNode ConvertBST(TreeNode root)
    {
        if (root != null)
        {
            ConvertBST(root.right);
            sum += root.val;
            root.val = sum;
            ConvertBST(root.left);
        }

        return root;
    }
}

/// <summary>
/// 题号：557. 反转字符串中的单词 III
/// 题目：
/// 给定一个字符串，你需要反转字符串中每个单词的字符顺序，同时仍保留空格和单词的初始顺序。
/// 示例：
/// 输入："Let's take LeetCode contest"
/// 输出："s'teL ekat edoCteeL tsetnoc"
/// 提示：在字符串中，每个单词由单个空格分隔，并且字符串中不会有任何额外的空格。
/// 
/// </summary>

public class Solution
{
    public string ReverseWords(string s)
    {
        string[] temp = s.Split(" ");
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = Reverse(temp[i]);
        }
        return String.Join(" ", temp);
    }

    //反转字符串
    private string Reverse(string s)
    {
        char[] temp = s.ToArray();
        Array.Reverse(temp);
        string result = String.Join("", temp);
        return result;
    }
}

/// <summary>
/// 题号：617. 合并二叉树
/// 题目：
/// 给定两个二叉树，想象当你将它们中的一个覆盖到另一个上时，两个二叉树的一些节点便会重叠。
/// 你需要将他们合并为一个新的二叉树。合并的规则是如果两个节点重叠，那么将他们的值相加作为节点合并后的新值，否则不为 NULL 的节点将直接作为新二叉树的节点。
/// 
/// 自递归
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{

    public TreeNode MergeTrees(TreeNode t1, TreeNode t2)
    {
        if (t1 == null) return t2;
        if (t2 == null) return t1;
        TreeNode result = new TreeNode(t1.val + t2.val);
        result.left = MergeTrees(t1.left, t2.left);
        result.right = MergeTrees(t1.right, t2.right);
        return result;
    }
}

/// <summary>
/// 题号：647
/// 题目：
/// 给定一个字符串，你的任务是计算这个字符串中有多少个回文子串。
/// 具有不同开始位置或结束位置的子串，即使是由相同的字符组成，也会被视作不同的子串。
/// 输入："aaa"
/// 输出：6
/// 解释：6个回文子串: "a", "a", "a", "aa", "aa", "aaa"
/// 
/// 回文子串
/// 对于每一个首相都判断，当中间一个相等时，如果左边与右边都相等时，必然是一个回文子串
/// 需要判断s的长度为奇还是偶
/// </summary>
public class Solution
{
    int num = 0;
    public int CountSubstrings(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            Count(s, i, i);    //s长度为奇
            Count(s, i, i + 1);//s长度为偶
        }
        return num;
    }

    public void Count(string s, int start, int end)
    {
        while (start >= 0 && end < s.Length && s[start] == s[end])
        {
            num++;
            start--;
            end++;
        }
    }
}

/// <summary>
/// 题号：657  机器人能否返回原点
/// 题目：
/// 在二维平面上，有一个机器人从原点 (0, 0) 开始。给出它的移动顺序，判断这个机器人在完成移动后是否在 (0, 0) 处结束。
/// 移动顺序由字符串表示。字符 move[i] 表示其第 i 次移动。机器人的有效动作有 R（右），L（左），U（上）和 D（下）。如果机器人在完成所有动作后返回原点，则返回 true。否则，返回 false。
/// 注意：机器人“面朝”的方向无关紧要。 “R” 将始终使机器人向右移动一次，“L” 将始终向左移动等。此外，假设每次移动机器人的移动幅度相同。
/// 输入: "UD"
/// 输出: true
/// 解释：机器人向上移动一次，然后向下移动一次。所有动作都具有相同的幅度，因此它最终回到它开始的原点。因此，我们返回 true。
/// 
/// 
/// </summary>
public class Solution
{
    public bool JudgeCircle(string moves)
    {
        int left = 0;
        int right = 0;
        int up = 0;
        int down = 0;

        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i] == 'R') right++;
            if (moves[i] == 'L') left++; 
            if (moves[i] == 'U') up++; 
            if (moves[i] == 'D') down++;
        }

        if (left == right && up == down)
        {
            return true;
        }
        return false;
    }
}

/// <summary>
/// 题号：679
/// 题目：
/// 你有 4 张写有 1 到 9 数字的牌。你需要判断是否能通过 *，/，+，-，(，) 的运算得到 24。
/// 输入: [4, 1, 8, 7]
/// 输出: True
/// 解释: (8 - 4) * (7 - 1) = 24
/// 
/// 递归
/// 每次取两个数，两个数计算的结果替代原来的两个数，使最后列表只余下一个数
/// </summary>
public class Solution
{
    public bool JudgePoint24(int[] nums)
    {
        //原来的数复制一份到新的数组中
        double[] numsTemp = new double[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            numsTemp[i] = (double)nums[i];
        }

        if (nums.Length != 4)
        {
            return false;
        }

        return(Solve(numsTemp));
    }

    //把列表中两个数去掉，替换为一个数
    //返回数组
    private bool Solve(double[] list)
    {
        bool result = false;

        if (list.Length == 1)
        {
            //去除因为除法导致的误差
            if (Math.Abs(list[0] - 24.0) < 0.001)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        else if (list.Length == 0)
        {
            result = false;
        }
        else
        {
            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    foreach (double newNum in Judge(list[i], list[j]))
                    {
                        double[] listTemp = new double[list.Length - 1];

                        //设置第一个数为新的数
                        listTemp[0] = newNum;

                        //其他的先除掉两个数剩下的新建到新的1数组中
                        int k = 1;  //新数组的序号
                        for (int m = 0; m < list.Length; m++)
                        {
                            if (m != i && m != j)
                            {
                                listTemp[k] = list[m];
                                k++;
                            }
                        }

                        result = result || Solve(listTemp);
                    }

                }
            }
        }

        return result;
    }

    //常规的加减乘除
    private List<double> Judge(double num1 , double num2)
    {
        //计算任意两项的结果
        //注意不一定为整数，需要小数
        List<double> temp = new List<double>();

        temp.Add(num1+num2);
        temp.Add(num1*num2);
        temp.Add(num1-num2);
        temp.Add(num2-num1);
        if(num1 != 0)
        {
            temp.Add(num2/num1);
        }
        if (num2 != 0)
        {
            temp.Add(num1/num2);
        }

        return temp;
    }
}

/// <summary>
/// 题号：701. 二叉搜索树中的插入操作
/// 题目：
/// 给定二叉搜索树（BST）的根节点和要插入树中的值，将值插入二叉搜索树。 返回插入后二叉搜索树的根节点。 输入数据保证，新值和原始二叉搜索树中的任意节点值都不同。
/// 注意，可能存在多种有效的插入方式，只要树在插入后仍保持为二叉搜索树即可。 你可以返回任意有效的结果。
/// 
/// 递归
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
        {
            return new TreeNode(val);
        }

        if (root.val < val)
        {
            root.right = InsertIntoBST(root.right, val);
        }
        else
        {
            root.left = InsertIntoBST(root.left, val);
        }
        return root;
    }
}

/// <summary>
/// 题号：721. 账户合并
/// 题目：
/// 给定一个列表 accounts，每个元素 accounts[i] 是一个字符串列表，其中第一个元素 accounts[i][0] 是 名称 (name)，其余元素是 emails 表示该账户的邮箱地址。
/// 现在，我们想合并这些账户。如果两个账户都有一些共同的邮箱地址，则两个账户必定属于同一个人。
/// 请注意，即使两个账户具有相同的名称，它们也可能属于不同的人，因为人们可能具有相同的名称。一个人最初可以拥有任意数量的账户，但其所有账户都具有相同的名称。
/// 合并账户后，按以下格式返回账户：每个账户的第一个元素是名称，其余元素是按顺序排列的邮箱地址。账户本身可以以任意顺序返回。
/// 示例 1：
/// 输入：
/// accounts = [["John", "johnsmith@mail.com", "john00@mail.com"], ["John", "johnnybravo@mail.com"], ["John", "johnsmith@mail.com", "john_newyork@mail.com"], ["Mary", "mary@mail.com"]]
/// 输出：
/// [["John", 'john00@mail.com', 'john_newyork@mail.com', 'johnsmith@mail.com'],  ["John", "johnnybravo@mail.com"], ["Mary", "mary@mail.com"]]
/// 解释：
/// 第一个和第三个 John 是同一个人，因为他们有共同的邮箱地址 "johnsmith@mail.com"。 
/// 第二个 John 和 Mary 是不同的人，因为他们的邮箱地址没有被其他帐户使用。
/// 可以以任何顺序返回这些列表，例如答案 [['Mary'，'mary@mail.com']，['John'，'johnnybravo@mail.com']，
/// ['John'，'john00@mail.com'，'john_newyork@mail.com'，'johnsmith@mail.com']] 也是正确的。
/// 提示：
/// accounts的长度将在[1，1000]的范围内。
/// accounts[i]的长度将在[1，10]的范围内。
/// accounts[i][j]的长度将在[1，30]的范围内。
/// 
/// 并查集
/// </summary>
using System.Collections;
public class Solution
{
    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        //将邮箱与序号对应，方便并查集
        Dictionary<string, int> Hash_index = new Dictionary<string, int>();
        //邮箱与人对应
        Dictionary<string, string> Hash_name = new Dictionary<string, string>();

        //邮箱序号
        int emailindex = 0;
        foreach (IList<string> account in accounts)
        {
            string name = account[0];  //人名
            int size = account.Count;  //总长度
            for (int i = 1; i < size; i++)  
            {
                string email = account[i]; //从第1位起为邮箱
                if (!Hash_index.ContainsKey(email))
                {
                    Hash_index.Add(email, emailindex++);
                    Hash_name.Add(email, name);
                }
            }
        }

        //建立并查集
        JoinFind joinFind = new JoinFind(emailindex);

        //建立联系
        foreach (IList<string> account in accounts)
        {
            string email1 = account[1];  //第一个邮箱
            int email1_index = Hash_index[email1]; //第一个邮箱对应序号
            int size = account.Count;  //总长度
            for (int i = 2; i < size; i++)  //从第二个邮箱开始建立联系
            {
                string email2 = account[i];
                int email2_index = Hash_index[email2];
                joinFind.join(email1_index, email2_index);
            }
        }

        //序号对应的邮箱组
        //即每一个邮箱在哪一个邮箱组内
        //以父节点为头
        Dictionary<int, IList<string>> Hash_emails = new Dictionary<int, IList<string>>();
        //所有邮箱
        foreach (string email in Hash_index.Keys)
        {
            int email_index = Hash_index[email];
            int parentindex = joinFind.find(email_index);//找到父节点
            if (Hash_emails.ContainsKey(parentindex))
            {
                Hash_emails[parentindex].Add(email);
            }
            else
            {
                Hash_emails.Add(parentindex, new List<string>() { email });
            }
        }

        //答案
        IList<IList<string>> result = new List<IList<string>>();

        //合并答案
        foreach (List<string> emails in Hash_emails.Values)
        {
            string name = Hash_name[emails[0]];  //第一个邮箱对应的人物名称
            emails.Insert(0, name);
            result.Add(emails);
        }
        return result;
    }
}

//并查集模板
public class JoinFind
{
    int[] parent;

    //初始化，先各自为政，有几个认为父节点为自身
    public JoinFind(int n)
    {
        parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;  //父节点为自身
        }
    }

    //查找自身的父节点
    //对应几号的父节点
    //同时压缩
    public int find(int index)
    {
        //如果当前节点的父节点不为自身，即还存在上一级情况
        //继续寻找，直到找到父节点
        //同时压缩节点深度
        if (parent[index] != index)
        {
            parent[index] = find(parent[index]);
        }
        //返回父节点
        return parent[index];
    }

    //连接
    public void join(int index1, int index2)
    {
        //让两者的父节点相互联系
        parent[find(index1)] = find(index2);
    }
}

/// <summary>
/// 题号：763. 划分字母区间
/// 题目：
/// 字符串 S 由小写字母组成。我们要把这个字符串划分为尽可能多的片段，同一个字母只会出现在其中的一个片段。返回一个表示每个字符串片段的长度的列表。
/// 示例 1：
/// 输入：S = "ababcbacadefegdehijhklij"
/// 输出：[9,7,8]
/// 划分结果为 "ababcbaca", "defegde", "hijhklij"。
/// 每个字母最多出现在一个片段中。
/// 像 "ababcbacadefegde", "hijhklij" 的划分是错误的，因为划分的片段数较少。
/// 提示：
/// S的长度在[1, 500]之间。
/// S只包含小写字母 'a' 到 'z' 。
/// 
/// 哈希表
/// 合并数组
/// </summary>
public class Solution
{
    public IList<int> PartitionLabels(string S)
    {
        IList<int> result = new List<int>();
        int n = S.Length;
        if (n == 0) return result;

        //建立字母对应哈希表,储存对应的首位与最后位
        Dictionary<char, int[]> Hash = new Dictionary<char, int[]>();

        for (int i = 0; i < n; i++)
        {
            if (!Hash.ContainsKey(S[i]))
            {
                Hash.Add(S[i], new int[2] { i, i });
            }
            else
            {
                Hash[S[i]][1] = i;
            }
        }

        //合并数组
        //第一个的边界
        int left = Hash[S[0]][0];
        int right = Hash[S[0]][1];
        foreach (char key in Hash.Keys)
        {
            if (Hash[key][0] > right)
            {
                result.Add(right - left + 1);
                left = Hash[key][0];
                right = Hash[key][1];
            }
            else
            {
                if (Hash[key][1] > right)
                {
                    right = Hash[key][1];
                }
            }
            if (right == n - 1)
            {
                result.Add(right - left + 1);
                break;
            }
        }

        return result;
    }
}

//官方双指针加贪心
public class Solution
{
    public IList<int> PartitionLabels(string S)
    {
        IList<int> result = new List<int>();
        int n = S.Length;
        if (n == 0) return result;

        //记录每次的最后一位
        int[] last = new int[26];

        for (int i = 0; i < n; i++)
        {
            last[S[i] - 'a'] = i;
        }

        //记录对应起点与终点
        int start = 0;
        int end = 0;
        //如果到当前值则返回
        for (int i = 0; i < n; i++)
        {
            //更新结尾
            end = Math.Max(end, last[S[i] - 'a']);
            //如果到结尾了
            if (end == i)
            {
                result.Add(end - start + 1);
                start = end + 1;
            }
        }
        return result;
    }
}

/// <summary>
/// 题号：834. 树中距离之和
/// 题目：
/// 给定一个无向、连通的树。树中有 N 个标记为 0...N-1 的节点以及 N-1 条边 。
/// 第 i 条边连接节点 edges[i][0] 和 edges[i][1] 。
/// 返回一个表示节点 i 与其他所有节点距离之和的列表 ans。
/// 示例 1:
/// 输入: N = 6, edges = [[0, 1],[0,2],[2,3],[2,4],[2,5]]
/// 输出:[8,12,6,10,10,10]
/// 解释:
/// 如下为给定的树的示意图：
///   0
///  / \
/// 1   2
///    /|\
///   3 4 5
/// 我们可以计算出 dist(0,1) +dist(0, 2) + dist(0, 3) + dist(0, 4) + dist(0, 5)
/// 也就是 1 + 1 + 2 + 2 + 2 = 8。 因此，answer[0] = 8，以此类推。
/// 
/// 树形动态规划
/// </summary>
public class Solution
{
    //动态规划
    int[] dp;
    //子树
    int[] sz;
    //答案
    int[] result;
    //连接树
    List<List<int>> graph;

    public int[] SumOfDistancesInTree(int N, int[][] edges)
    {
        //初始化
        dp = new int[N];
        sz = new int[N];
        result = new int[N];
        graph = new List<List<int>>();
        for (int i = 0; i < N; i++)
        {
            graph.Add(new List<int>());
        }

        //建立连接树
        foreach (int[] edge in edges)
        {
            int u = edge[0];
            int v = edge[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }

        dfs1(0, -1);
        dfs2(0, -1);
        return result;
    }

    //更新dp数据
    private void dfs1(int u, int father)
    {
        //初始化默认设置
        //原理见leedcode
        sz[u] = 1;
        dp[u] = 0;
        foreach (int v in graph[u])
        {
            //如果为父节点直接跳过，只看子节点
            if (v == father) continue;

            //找子树
            dfs1(v, u);

            dp[u] += (dp[v] + sz[v]);
            sz[u] += sz[v];
        }
    }

    //当子节点变为父节点时
    private void dfs2(int u, int father)
    {
        //更新返回时即为答案
        result[u] = dp[u];

        foreach (int v in graph[u])
        {
            if (v == father) continue;

            //中间值
            int pu = dp[u];
            int pv = dp[v];
            int su = sz[u];
            int sv = sz[v];

            //更新
            dp[u] -= (dp[v] + sz[v]);
            sz[u] -= sz[v];
            dp[v] += (dp[u] + sz[u]);
            sz[v] += sz[u];

            dfs2(v, u);

            //还原
            dp[u] = pu;
            dp[v] = pv;
            sz[u] = su;
            sz[v] = sv;
        }
    }
}

/// <summary>
/// 题号：841. 钥匙和房间
/// 题目：
/// 有 N 个房间，开始时你位于 0 号房间。每个房间有不同的号码：0，1，2，...，N-1，并且房间里可能有一些钥匙能使你进入下一个房间。
/// 在形式上，对于每个房间 i 都有一个钥匙列表 rooms[i]，每个钥匙 rooms[i][j] 由[0, 1，...，N - 1] 中的一个整数表示，其中 N = rooms.length。 钥匙 rooms[i][j] = v 可以打开编号为 v 的房间。
/// 最初，除 0 号房间外的其余所有房间都被锁住。
/// 你可以自由地在房间之间来回走动。
/// 如果能进入每个房间返回 true，否则返回 false。
/// 输入: [[1],[2],[3],[]]
/// 输出: true
/// 解释:  
/// 我们从 0 号房间开始，拿到钥匙 1。
/// 之后我们去 1 号房间，拿到钥匙 2。
/// 然后我们去 2 号房间，拿到钥匙 3。
/// 最后我们去了 3 号房间。
/// 由于我们能够进入每个房间，我们返回 true。
/// 
/// 深度优先搜索 DFS
/// 广度优先搜索 BFS
/// </summary>
//DFS模板
public class Solution
{
    int num = 0;   //计算已经统计过得房间

    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        bool[] use = new bool[rooms.Count];
        DFS(use, rooms, 0);
        return num == rooms.Count;
    }

    //DFS模板
    private void DFS(bool[] use, IList<IList<int>> rooms, int now)
    {
        use[now] = true;
        num++;
        foreach (int next in rooms[now])
        {
            if (!use[next])
            {
                DFS(use, rooms, next);
            }
        }
    }
}

//BFS模板
//使用队列
public class Solution
{
    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        int num = 0;   //计算已经统计过得房间
        bool[] use = new bool[rooms.Count];
        Queue<int> que = new Queue<int>();
        use[0] = true;
        que.Enqueue(0);
        while (que.Count != 0)
        {
            int now = que.Dequeue();
            num++;
            foreach (int next in rooms[now])
            {
                if (!use[next])
                {
                    use[next] = true;
                    que.Enqueue(next);
                }
            }
        }

        return num == rooms.Count;
    }

}

/// <summary>
/// 题号：844. 比较含退格的字符串
/// 题目：
/// 给定 S 和 T 两个字符串，当它们分别被输入到空白的文本编辑器后，判断二者是否相等，并返回结果。 # 代表退格字符。
/// 注意：如果对空文本输入退格字符，文本继续为空。
/// 示例 1：
/// 输入：S = "ab#c", T = "ad#c"
/// 输出：true
/// 解释：S 和 T 都会变成 “ac”。
/// 示例 2：
/// 输入：S = "ab##", T = "c#d#"
/// 输出：true
/// 解释：S 和 T 都会变成 “”。
/// 示例 3：
/// 输入：S = "a##c", T = "#a#c"
/// 输出：true
/// 解释：S 和 T 都会变成 “c”。
/// 示例 4：
/// 输入：S = "a#c", T = "b"
/// 输出：false
/// 解释：S 会变成 “c”，但 T 仍然是 “b”。
/// 提示：
/// 1 <= S.length <= 200
/// 1 <= T.length <= 200
/// S 和 T 只含有小写字母以及字符 '#'。
/// 进阶：
/// 你可以用 O(N) 的时间复杂度和 O(1) 的空间复杂度解决该问题吗？
/// 
/// 双指针
/// 逆序遍历
/// </summary>
public class Solution
{
    public bool BackspaceCompare(string S, string T)
    {
        //总长度
        int m = S.Length - 1;
        int n = T.Length - 1;

        //#号个数
        int Ssub = 0;
        int Tsub = 0;

        while (m >= 0 || n >= 0)
        {
            //分次比较
            //用while分次比较，尽量不要使用Ssub与Tsub等于0作为边界条件，防止溢出
            while (m >= 0)
            {
                if (S[m] == '#')
                {
                    Ssub++;
                    m--;
                }
                else
                {
                    if (Ssub != 0)
                    {
                        Ssub--;
                        m--;
                    }
                    else
                    {
                        //防止边界溢出
                        break;
                    }
                }
            }
            while (n >= 0)
            {
                if (T[n] == '#')
                {
                    Tsub++;
                    n--;
                }
                else
                {
                    if (Tsub != 0)
                    {
                        Tsub--;
                        n--;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //防止溢出
            if (m >= 0 && n >= 0)
            {
                if (S[m] != T[n])
                {
                    return false;
                }
            }
            else
            {
                //需要同步为-1才对
                if (m >= 0 || n >= 0)
                {
                    return false;
                }
            }
            //相等时
            m--;
            n--;
        }
        return true;
    }
}

/// <summary>
/// 题号：845	数组中的最长山脉
/// 题目：
/// 我们把数组 A 中符合下列属性的任意连续子数组 B 称为 “山脉”：
/// B.length >= 3
/// 存在 0 < i < B.length - 1 使得 B[0] < B[1] < ... B[i - 1] < B[i] > B[i + 1] > ... > B[B.length - 1]
/// （注意：B 可以是 A 的任意子数组，包括整个数组 A。）
/// 给出一个整数数组 A，返回最长 “山脉” 的长度。
/// 如果不含有 “山脉” 则返回 0。
/// 示例 1：
/// 输入：[2,1,4,7,3,2,5]
/// 输出：5
/// 解释：最长的 “山脉” 是[1, 4, 7, 3, 2]，长度为 5。
/// 示例 2：
/// 输入：[2,2,2]
/// 输出：0
/// 解释：不含 “山脉”。
/// 提示：
/// 0 <= A.length <= 10000
/// 0 <= A[i] <= 10000
/// 
/// </summary>
public class Solution
{
    //左右各寻找不递增的范围
    public int LongestMountain(int[] A)
    {
        int n = A.Length;
        if (n == 0) return 0;

        int[] left = new int[n];
        int[] right = new int[n];
        for (int i = 0; i < n; i++)
        {
            right[i] = n - 1;
        }

        //左边界
        for (int i = 1; i < n; i++)
        {
            if (A[i] > A[i - 1])
            {
                left[i] = left[i - 1];
            }
            else
            {
                left[i] = i;
            }
        }

        //右边界
        for (int i = n - 2; i >= 0; i--)
        {
            if (A[i] > A[i + 1])
            {
                right[i] = right[i + 1];
            }
            else
            {
                right[i] = i;
            }
        }

        int result = 0;
        for (int i = 0; i < n; i++)
        {
            //判断是否光递增或递减
            if (left[i] == i || right[i] == i) continue;
            result = Math.Max(result, right[i] - left[i] + 1);
        }
        //如果只剩一个也不对
        return result == 1 ? 0 : result;
    }
}

/// <summary>
/// 题号：925. 长按键入
/// 题目：
/// 你的朋友正在使用键盘输入他的名字 name。偶尔，在键入字符 c 时，按键可能会被长按，而字符可能被输入 1 次或多次。
/// 你将会检查键盘输入的字符 typed。如果它对应的可能是你的朋友的名字（其中一些字符可能被长按），那么就返回 True。
/// 示例 1：
/// 输入：name = "alex", typed = "aaleex"
/// 输出：true
/// 解释：'alex' 中的 'a' 和 'e' 被长按。
/// 示例 2：
/// 输入：name = "saeed", typed = "ssaaedd"
/// 输出：false
/// 解释：'e' 一定需要被键入两次，但在 typed 的输出中不是这样。
/// 示例 3：
/// 输入：name = "leelee", typed = "lleeelee"
/// 输出：true
/// 示例 4：
/// 输入：name = "laiden", typed = "laiden"
/// 输出：true
/// 解释：长按名字中的字符并不是必要的。
/// 提示：
/// name.length <= 1000
/// typed.length <= 1000
/// name 和 typed 的字符都是小写字母。
/// 
/// 递归
/// </summary>
public class Solution
{
    public bool IsLongPressedName(string name, string typed)
    {
        int m = name.Length;
        int n = typed.Length;
        //输入的小肯定不是
        if (n < m) return false;

        //在对应字符前加“ ”，初始肯定为真
        return Match(" " + name, " " + typed, m, n);
    }

    /// <summary>
    /// 匹配
    /// </summary>
    /// <param name="name">原始字符</param>
    /// <param name="typed">匹配的字符</param>
    /// <param name="i">下标</param>
    /// <param name="j">下标</param>
    /// <returns></returns>
    private bool Match(string name, string typed, int i, int j)
    {
        //边界条件
        //同时为空时对
        if (name[i] == ' ' && typed[j] == ' ')
        {
            return true;
        }
        //不同时为空
        if (name[i] == ' ' || typed[j] == ' ')
        {
            return false;
        }

        //递增条件
        if (name[i] == typed[j])
        {
            return Match(name, typed, i - 1, j - 1) || Match(name, typed, i, j - 1);
        }
        else
        {
            return false;
        }
    }
}

/// <summary>
/// 题号：946. 验证栈序列
/// 题目：
/// 给定 pushed 和 popped 两个序列，每个序列中的 值都不重复，只有当它们可能是在最初空栈上进行的推入 push 和弹出 pop 操作序列的结果时，返回 true；否则，返回 false 。
/// 示例 1：
/// 输入：pushed = [1, 2, 3, 4, 5], popped = [4, 5, 3, 2, 1]
/// 输出：true
/// 解释：我们可以按以下顺序执行：
/// push(1), push(2), push(3), push(4), pop()-> 4,
/// push(5), pop()-> 5, pop()-> 3, pop()-> 2, pop()-> 1
/// 示例 2：
/// 输入：pushed = [1, 2, 3, 4, 5], popped = [4, 3, 5, 1, 2]
/// 输出：false
/// 解释：1 不能在 2 之前弹出。
/// 提示：
/// 0 <= pushed.length == popped.length <= 1000
/// 0 <= pushed[i], popped[i] < 1000
/// pushed 是 popped 的排列。
/// 
/// 贪心
/// 模拟实现
/// </summary>
public class Solution
{
    public bool ValidateStackSequences(int[] pushed, int[] popped)
    {
        int len = pushed.Length;
        Stack<int> temp = new Stack<int>();

        int index = 0;
        //正常压入栈
        //需要弹出时一定在栈顶
        for (int i = 0; i < len; i++)
        {
            temp.Push(pushed[i]);
            while (temp.Count != 0 && index < len && temp.Peek() == popped[index])
            {
                temp.Pop();
                index++;
            }
        }

        return index == len;
    }
}

/// <summary>
/// 题号：973. 最接近原点的 K 个点
/// 题目：
/// 我们有一个由平面上的点组成的列表 points。需要从中找出 K 个距离原点 (0, 0) 最近的点。
/// （这里，平面上两点之间的距离是欧几里德距离。）
/// 你可以按任何顺序返回答案。除了点坐标的顺序之外，答案确保是唯一的。
/// 示例 1：
/// 输入：points = [[1, 3],[-2,2]], K = 1
/// 输出：[[-2,2]]
/// 解释： 
/// (1, 3) 和原点之间的距离为 sqrt(10)，
/// (-2, 2) 和原点之间的距离为 sqrt(8)，
/// 由于 sqrt(8) < sqrt(10)，(-2, 2) 离原点更近。
/// 我们只需要距离原点最近的 K = 1 个点，所以答案就是 [[-2,2]]。
/// 示例 2：
/// 输入：points = [[3, 3],[5,-1],[-2,4]], K = 2
/// 输出：[[3,3],[-2,4]]
/// （答案[[-2, 4],[3,3]] 也会被接受。）
/// 提示：
/// 1 <= K <= points.length <= 10000
/// - 10000 < points[i][0] < 10000
/// - 10000 < points[i][1] < 10000
/// </summary>
public class Solution
{
    public int[][] KClosest(int[][] points, int K)
    {
        int n = points.Length;
        int[][] temp = new int[n][];
        for (int i = 0; i < n; i++)
        {
            temp[i] = new int[2];
            int x = points[i][0];
            int y = points[i][1];
            temp[i][0] = (x * x + y * y);
            temp[i][1] = i;
        }
        Array.Sort(temp, (a, b) => a[0] - b[0]);
        int[][] result = new int[K][];
        for (int i = 0; i < K; i++)
        {
            result[i] = points[temp[i][1]];
        }
        return result;
    }
}

/// <summary>
/// 题号：1002. 查找常用字符
/// 题目：
/// 给定仅有小写字母组成的字符串数组 A，返回列表中的每个字符串中都显示的全部字符（包括重复字符）组成的列表。例如，如果一个字符在每个字符串中出现 3 次，但不是 4 次，则需要在最终答案中包含该字符 3 次。
/// 你可以按任意顺序返回答案。
/// 示例 1：
/// 输入：["bella","label","roller"]
/// 输出：["e","l","l"]
/// 示例 2：
/// 输入：["cool","lock","cook"]
/// 输出：["c","o"]
/// 提示：
/// 1 <= A.length <= 100
/// 1 <= A[i].length <= 100
/// A[i][j] 是小写字母
/// 
/// 哈希表
/// 出现频率、小写字母，这种关键词用哈希表
/// 维护一个字母长度（26）的哈希表，每次看字符串时建立一个哈希表，对应取最小的
/// </summary>
public class Solution
{
    public IList<string> CommonChars(string[] A)
    {
        //共26个字母
        int[] minChar = new int[26];
        for (int i = 0; i < minChar.Length; i++) minChar[i] = int.MaxValue;
        foreach (string str in A)
        {
            int[] Char = new int[26];
            foreach (char chartemp in str)
            {
                //用字符串的位置关系做下标
                Char[chartemp - 'a']++;
            }
            for (int i = 0; i < 26; i++)
            {
                minChar[i] = Math.Min(minChar[i], Char[i]);
            }
        }

        IList<string> result = new List<string>();
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < minChar[i]; j++)
            {
                char temp = (char)(i + 'a');
                result.Add(temp.ToString());
            }
        }
        return result;
    }
}

/// <summary>
/// 题号：1024. 视频拼接
/// 题目：
/// 你将会获得一系列视频片段，这些片段来自于一项持续时长为 T 秒的体育赛事。这些片段可能有所重叠，也可能长度不一。
/// 视频片段 clips[i] 都用区间进行表示：开始于 clips[i][0] 并于 clips[i][1] 结束。我们甚至可以对这些片段自由地再剪辑，
/// 例如片段[0, 7] 可以剪切成[0, 1] + [1, 3] + [3, 7] 三部分。
/// 我们需要将这些片段进行再剪辑，并将剪辑后的内容拼接成覆盖整个运动过程的片段（[0, T]）。返回所需片段的最小数目，如果无法完成该任务，则返回 - 1 。
/// 示例 1：
/// 输入：clips = [[0, 2],[4,6],[8,10],[1,9],[1,5],[5,9]], T = 10
/// 输出：3
/// 解释：
/// 我们选中[0, 2], [8,10], [1,9] 这三个片段。
/// 然后，按下面的方案重制比赛片段：
/// 将[1, 9] 再剪辑为[1, 2] + [2, 8] + [8, 9] 。
/// 现在我们手上有[0, 2] + [2, 8] + [8, 10]，而这些涵盖了整场比赛[0, 10]。
/// 示例 2：
/// 输入：clips = [[0, 1],[1,2]], T = 5
/// 输出：-1
/// 解释：
/// 我们无法只用[0, 1] 和[1, 2] 覆盖[0, 5] 的整个过程。
/// 示例 3：
/// 输入：clips = [[0, 1],[6,8],[0,2],[5,6],[0,4],[0,3],[6,7],[1,3],[4,7],[1,4],[2,5],[2,6],[3,4],[4,5],[5,7],[6,9]], T = 9
/// 输出：3
/// 解释： 
/// 我们选取片段[0, 4], [4,7] 和[6, 9] 。
/// 示例 4：
/// 输入：clips = [[0, 4],[2,8]], T = 5
/// 输出：2
/// 解释：
/// 注意，你可能录制超过比赛结束时间的视频。
/// 提示：
/// 1 <= clips.length <= 100
/// 0 <= clips[i][0] <= clips[i][1] <= 100
/// 0 <= T <= 100
/// </summary>
public class Solution
{
    public int VideoStitching(int[][] clips, int T)
    {
        if (T == 0) return 0;

        int n = clips.Length;

        //从某一起点的最长距离
        Dictionary<int, int> temp = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            //没有起点时添加
            if (!temp.ContainsKey(clips[i][0]))
            {
                temp.Add(clips[i][0], clips[i][1]);
            }
            else
            {
                //更新更长起点
                if (clips[i][1] > temp[clips[i][0]])
                {
                    temp[clips[i][0]] = clips[i][1];
                }
            }
        }

        //没有从0开始的起点，肯定不对
        if (!temp.ContainsKey(0)) return -1;

        //先取一个z最左边的值
        int result = 0;
        //左右边界
        int left = 0;
        int right = 0;

        while (right < T)
        {
            //临时左右边界
            int right_temp = right;
            for (int i = left; i <= right; i++)
            {
                if (!temp.ContainsKey(i)) continue;
                if (temp[i] > right_temp)
                {
                    //更新更长的
                    right_temp = temp[i];
                }
            }
            //更新完后如果右边界没更新，及存在突变，直接报错
            if (right_temp == right) return -1;
            result++;
            //更新左右边界
            left = right;
            right = right_temp;
        }

        return result;
    }
}

/// <summary>
/// 题号：1207. 独一无二的出现次数
/// 题目：
/// 给你一个整数数组 arr，请你帮忙统计数组中每个数的出现次数。
/// 如果每个数的出现次数都是独一无二的，就返回 true；否则返回 false。
/// 示例 1：
/// 输入：arr = [1, 2, 2, 1, 1, 3]
/// 解释：在该数组中，1 出现了 3 次，2 出现了 2 次，3 只出现了 1 次。没有两个数的出现次数相同。
/// 示例 2
/// 输入：arr = [1, 2]
/// 输出：false
/// 示例 3：
/// 输入：arr = [-3, 0, 1, -3, 1, 1, 1, -3, 10, 0]
/// 输出：true
/// 提示：
/// 1 <= arr.length <= 1000
/// - 1000 <= arr[i] <= 1000
/// </summary>
public class Solution
{
    public bool UniqueOccurrences(int[] arr)
    {
        int n = arr.Length;
        Dictionary<int, int> Hash = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            if (!Hash.ContainsKey(arr[i]))
            {
                Hash.Add(arr[i], 1);
            }
            else
            {
                Hash[arr[i]]++;
            }
        }

        HashSet<int> temp = new HashSet<int>();
        foreach (int value in Hash.Values)
        {
            if (temp.Contains(value))
            {
                return false;
            }
            temp.Add(value);
        }
        return true;
    }
}

/// <summary>
/// 题号：1356. 根据数字二进制下 1 的数目排序
/// 题目：
/// 给你一个整数数组 arr 。请你将数组中的元素按照其二进制表示中数字 1 的数目升序排序。
/// 如果存在多个数字二进制中 1 的数目相同，则必须将它们按照数值大小升序排列。
/// 请你返回排序后的数组。
/// 示例 1：
/// 输入：arr = [0, 1, 2, 3, 4, 5, 6, 7, 8]
/// 输出：[0,1,2,4,8,3,5,6,7]
/// 解释：[0] 是唯一一个有 0 个 1 的数。
/// [1,2,4,8] 都有 1 个 1 。
/// [3,5,6] 有 2 个 1 。
/// [7] 有 3 个 1 。
/// 按照 1 的个数排序得到的结果数组为[0, 1, 2, 4, 8, 3, 5, 6, 7]
/// 示例 2：
/// 输入：arr = [1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1]
/// 输出：[1,2,4,8,16,32,64,128,256,512,1024]
/// 解释：数组中所有整数二进制下都只有 1 个 1 ，所以你需要按照数值大小将它们排序。
/// 示例 3：
/// 输入：arr = [10000, 10000]
/// 输出：[10000,10000]
/// 示例 4：
/// 输入：arr = [2, 3, 5, 7, 11, 13, 17, 19]
/// 输出：[2,3,5,17,7,11,13,19]
/// 示例 5：
/// 输入：arr = [10, 100, 1000, 10000]
/// 输出：[10,100,10000,1000]
/// 提示：
/// 1 <= arr.length <= 500
/// 0 <= arr[i] <= 10 ^ 4
/// </summary>
public class Solution
{
    public int[] SortByBits(int[] arr)
    {
        //最多到10001，含0
        int[] temp = new int[10001];
        foreach (int num in arr) temp[num] = Get(num);
        Array.Sort(arr, (a, b) => 
        {
            if (temp[a] != temp[b]) return temp[a] - temp[b];
            else return a - b;
        });
        return arr;
    }

    private int Get(int num)
    {
        int result = 0;
        while (num > 0)
        {
            result += num % 2;
            num /= 2;
        }
        return result;
    }
}

/// <summary>
/// 题号：1365. 有多少小于当前数字的数字
/// 题目：
/// 给你一个数组 nums，对于其中每个元素 nums[i]，请你统计数组中比它小的所有数字的数目。
/// 换而言之，对于每个 nums[i] 你必须计算出有效的 j 的数量，其中 j 满足 j != i 且 nums[j] < nums[i] 。
/// 以数组形式返回答案。
/// 示例 1：
/// 输入：nums = [8, 1, 2, 2, 3]
/// 解释： 
/// 对于 nums[0] = 8 存在四个比它小的数字：（1，2，2 和 3）。 
/// 对于 nums[1]= 1 不存在比它小的数字。
/// 对于 nums[2] = 2 存在一个比它小的数字：（1）。 
/// 对于 nums[3]= 2 存在一个比它小的数字：（1）。 
/// 对于 nums[4] = 3 存在三个比它小的数字：（1，2 和 2）。
/// 示例 2：
/// 输入：nums = [6,5,4,8]
/// 输出：[2,1,0,3]
/// 示例 3：
/// 输入：nums = [7, 7, 7, 7]
/// 输出：[0,0,0,0]
/// 提示：
/// 2 <= nums.length <= 500
/// 0 <= nums[i] <= 100
/// </summary>
public class Solution
{
    public int[] SmallerNumbersThanCurrent(int[] nums)
    {
        int n = nums.Length;
        int[] temp = new int[n];
        for (int i = 0; i < n; i++)
        {
            temp[i] = nums[i];
        }
        Array.Sort(temp);
        //对应序号
        Dictionary<int, int> hash = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            if (hash.ContainsKey(temp[i])) continue;
            hash.Add(temp[i], i);
        }

        int[] result = new int[n];
        for (int i = 0; i < n; i++)
        {
            result[i] = hash[nums[i]];
        }
        return result;
    }
}

/// <summary>
/// 题号：剑指 Offer 04. 二维数组中的查找
/// 题目：
/// 在一个 n * m 的二维数组中，每一行都按照从左到右递增的顺序排序，每一列都按照从上到下递增的顺序排序。请完成一个函数，输入这样的一个二维数组和一个整数，判断数组中是否含有该整数。
/// 示例:
/// 现有矩阵 matrix 如下：
/// [
/// [1,   4,  7, 11, 15],
/// [3,   6,  9, 16, 22],
/// [10, 13, 14, 17, 24],
/// [18, 21, 23, 26, 30]
/// ]
/// 给定 target = 5，返回 true。
/// 给定 target = 20，返回 false。
/// 限制：
/// 0 <= n <= 1000
/// 0 <= m <= 1000
/// </summary>
public class Solution
{
    public bool FindNumberIn2DArray(int[][] matrix, int target)
    {
        int n = matrix.Length;
        if (n == 0) return false;
        int m = matrix[0].Length;
        if (m == 0) return false;

        //对应行列
        //第0行最后一列
        int row = 0;
        int column = m - 1;
        while (row < n && column >= 0)
        {
            if (matrix[row][column] == target) return true;
            if (matrix[row][column] < target)
            {
                row++;
            }
            else
            {
                column--;
            }
        }
        return false;
    }
}

/// <summary>
/// 题号：剑指 Offer 05. 替换空格
/// 题目：
/// 请实现一个函数，把字符串 s 中的每个空格替换成"%20"。
/// 示例 1：
/// 输入：s = "We are happy."
/// 输出："We%20are%20happy."
/// 限制：
/// 0 <= s 的长度 <= 10000
/// </summary>
public class Solution
{
    public string ReplaceSpace(string s)
    {
        int n = s.Length;
        if (n == 0) return s;

        return s.Replace(" ", "%20");
    }
}

/// <summary>
/// 题号：剑指 Offer 07. 重建二叉树
/// 题目：
/// 输入某二叉树的前序遍历和中序遍历的结果，请重建该二叉树。假设输入的前序遍历和中序遍历的结果中都不含重复的数字。
/// 例如，给出
/// 前序遍历 preorder = [3,9,20,15,7]
/// 中序遍历 inorder = [9, 3, 15, 20, 7]
/// 返回如下的二叉树：
/// 
///    3
///   / \
///  9  20
///    /  \
///   15   7
///   限制：
///   0 <= 节点个数 <= 5000
///   
/// 层序遍历
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    //一定要全局变量，否则会超边界
    int preorderindex = 0;
    //对应哈希表
    Dictionary<int, int> Hash = new Dictionary<int, int>();
    //见二叉树层序遍历原理
    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        int n = preorder.Length;
        if (n == 0) return null;

        for (int i = 0; i < n; i++)
        {
            Hash.Add(inorder[i], i);
        }

        return Build(preorder, inorder, 0, n - 1);
    }

    private TreeNode Build(int[] preorder, int[] inorder, int begin, int end)
    {
        if (begin > end)
        {
            return null;
        }

        TreeNode result = new TreeNode(preorder[preorderindex]);

        int inorderindex = Hash[preorder[preorderindex]];

        preorderindex++;

        result.left = Build(preorder, inorder, begin, inorderindex - 1);
        result.right = Build(preorder, inorder, inorderindex + 1, end);

        return result;
    }
}

/// <summary>
/// 题号：剑指 Offer 10- I. 斐波那契数列
/// 题目：
/// 写一个函数，输入 n ，求斐波那契（Fibonacci）数列的第 n 项。斐波那契数列的定义如下：
/// F(0) = 0,   F(1) = 1
/// F(N) = F(N - 1) + F(N - 2), 其中 N > 1.
/// 斐波那契数列由 0 和 1 开始，之后的斐波那契数就是由之前的两数相加而得出。
/// 答案需要取模 1e9+7（1000000007），如计算初始结果为：1000000008，请返回 1。
/// 示例 1：
/// 输入：n = 2
/// 输出：1
/// 示例 2：
/// 输入：n = 5
/// 输出：5 
/// 提示：
/// 0 <= n <= 100
/// 
/// 动态规划
/// </summary>
public class Solution
{
    public int Fib(int n)
    {
        if (n == 0) return 0;
        if (n == 1) return 1;
        int[] dp = new int[n + 1];
        dp[0] = 0;
        dp[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            dp[i] = ((dp[i - 1] % 1000000007) + (dp[i - 2] % 1000000007)) % 1000000007;
    }

        return dp[n];
    }
}

/// <summary>
/// 题号：剑指 Offer 09. 用两个栈实现队列
/// 题目：
/// 用两个栈实现一个队列。队列的声明如下，请实现它的两个函数 appendTail 和 deleteHead ，
/// 分别完成在队列尾部插入整数和在队列头部删除整数的功能。(若队列中没有元素，deleteHead 操作返回 -1 )
/// 提示：
/// 1 <= values <= 10000
/// 最多会对 appendTail、deleteHead 进行 10000 次调用
/// 
/// 两个栈，一个做输入，一个做输出
/// </summary>
public class CQueue
{
    Stack<int> stack1, stack2; 
    public CQueue()
    {
        stack1 = new Stack<int>();
        stack2 = new Stack<int>();
    }

    public void AppendTail(int value)
    {
        stack1.Push(value);
    }

    public int DeleteHead()
    {
        //只有当stack2都删掉后才是首位
        //如果不为空，则当前值就是首位
        if (stack2.Count == 0)
        {
            while (stack1.Count != 0)
            {
                stack2.Push(stack1.Pop());
            }
        }
        if (stack2.Count == 0) return -1;
        return stack2.Pop();
    }
}

/// <summary>
/// 题号：剑指 Offer 11. 旋转数组的最小数字
/// 题目：
/// 把一个数组最开始的若干个元素搬到数组的末尾，我们称之为数组的旋转。输入一个递增排序的数组的一个旋转，输出旋转数组的最小元素。例如，数组 [3,4,5,1,2] 为 [1,2,3,4,5] 的一个旋转，该数组的最小值为1。  
/// 示例 1：
/// 输入：[3,4,5,1,2]
/// 输出：1
/// 示例 2：
/// 输入：[2,2,2,0,1]
/// 输出：0
/// 
/// 二分查找
/// </summary>
public class Solution
{
    public int MinArray(int[] numbers)
    {
        int n = numbers.Length;
        if (n == 0) return -1;
        return Fen(numbers, 0, n - 1);
    }

    private int Fen(int[] numbers, int i, int j)
    {
        if (i > j)
        {
            return -1;
        }

        if (i == j)
        {
            return numbers[i];
        }

        int mid = (i + j) / 2;

        //找最小就是找不规则的那个
        //中间值比右边大，最小肯定在右侧
        if (numbers[mid] > numbers[j])
        {
            return Fen(numbers, mid + 1, j);
        }
        else if (numbers[mid] == numbers[j])
        {
            //中间值正好相等时额外判断
            return Math.Min(Fen(numbers, i, mid), Fen(numbers, mid + 1, j));
        }
        else
        {
            //中间值比右边小，即右侧递增
            //最小值肯定不在右边，右边可以去掉
            return Fen(numbers, i, mid);
        }
    }
}

/// <summary>
/// 题号：剑指 Offer 12. 矩阵中的路径
/// 题目：
/// 请设计一个函数，用来判断在一个矩阵中是否存在一条包含某字符串所有字符的路径。路径可以从矩阵中的任意一格开始，每一步可以在矩阵中向左、右、上、下移动一格。
/// 如果一条路径经过了矩阵的某一格，那么该路径不能再次进入该格子。例如，在下面的3×4的矩阵中包含一条字符串“bfce”的路径（路径中的字母用加粗标出）。
/// [["a","b","c","e"],
/// ["s","f","c","s"],
/// ["a","d","e","e"]]
/// 但矩阵中不包含字符串“abfb”的路径，因为字符串的第一个字符b占据了矩阵中的第一行第二个格子之后，路径不能再次进入这个格子。
/// 示例 1：
/// 输入：board = [["A", "B", "C", "E"],["S","F","C","S"],["A","D","E","E"]], word = "ABCCED"
/// 输出：true
/// 示例 2：
/// 输入：board = [["a", "b"],["c","d"]], word = "abcd"
/// 输出：false
/// 提示：
/// 1 <= board.length <= 200
/// 1 <= board[i].length <= 200
/// </summary>
public class Solution
{
    int m;
    int n;

    //四个方向
    int[,] dir = new int[4, 2];

    bool[,] used;

    public bool Exist(char[][] board, string word)
    {
        //字母长度
        int len = word.Length;
        if (len == 0) return true;

        m = board.Length;
        n = board[0].Length;

        //判断是否使用
        used = new bool[m, n];

        dir[0, 0] = 1;
        dir[0, 1] = 0;
        dir[1, 0] = 0;
        dir[1, 1] = 1;
        dir[2, 0] = -1;
        dir[2, 1] = 0;
        dir[3, 0] = 0;
        dir[3, 1] = -1;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (Try(board, word, 0, len, i, j))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 找对应字符
    /// </summary>
    /// <param name="board">原数组</param>
    /// <param name="word">目标字符串</param>
    /// <param name="wordindex">字符串指针</param>
    /// <param name="len">字符串长度</param>
    /// <param name="i">当前位置i</param>
    /// <param name="j">当前位置j</param>
    /// <returns></returns>
    private bool Try(char[][] board, string word, int wordindex, int len, int i, int j)
    {
        if (!used[i, j])
        {
            //当前值符合条件
            if (board[i][j] == word[wordindex])
            {
                //已经判断到最后了，肯定有
                if (wordindex == len - 1) return true;

                used[i, j] = true;
                for (int x = 0; x < 4; x++)
                {
                    int i_next = i + dir[x, 0];
                    int j_next = j + dir[x, 1];
                    if (i_next >= 0 && i_next < m && j_next >= 0 && j_next < n)
                    {
                        if (Try(board, word, wordindex + 1, len, i_next, j_next))
                        {
                            return true;
                        }
                    }
                }
                used[i, j] = false;
                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

/// <summary>
/// 题号：剑指 Offer 13. 机器人的运动范围
/// 题目：
/// 地上有一个m行n列的方格，从坐标[0, 0] 到坐标[m - 1, n - 1] 。
/// 一个机器人从坐标[0, 0] 的格子开始移动，它每次可以向左、右、上、下移动一格（不能移动到方格外），也不能进入行坐标和列坐标的数位之和大于k的格子。
/// 例如，当k为18时，机器人能够进入方格[35, 37] ，因为3 + 5 + 3 + 7 = 18。但它不能进入方格[35, 38]，因为3 + 5 + 3 + 8 = 19。请问该机器人能够到达多少个格子？
/// 示例 1：
/// 输入：m = 2, n = 3, k = 1
/// 输出：3
/// 示例 2：
/// 输入：m = 3, n = 1, k = 0
/// 输出：1
/// 提示：
/// 1 <= n,m <= 100
/// 0 <= k <= 20
/// 
/// BFS或动态规划
/// 只需要考虑向右和向下
/// </summary>
public class Solution
{
    public int MovingCount(int m, int n, int k)
    {
        bool[,] used = new bool[m, n];
        used[0, 0] = true;
        //开始的0，0肯定对
        int result = 1;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                //剪枝
                if ((i == 0 && j == 0) || Get(i) + Get(j) > k) continue;
                //used[i,j] = used[i-1,j] || used[i,j-1];
                //因为只能从上边或左边过来
                //初始化
                if (i == 0) used[i, j] = used[i, j - 1];
                if (j == 0) used[i, j] = used[i - 1, j];
                if (i > 0 && j > 0)
                {
                    used[i, j] = used[i - 1, j] || used[i, j - 1];
                }
                result += used[i, j] ? 1 : 0;
            }
        }
        return result;
    }

    private int Get(int num)
    {
        int result = 0;
        while (num != 0)
        {
            result += num % 10;
            num /= 10;
        }
        return result;
    }
}

/// <summary>
/// 题号：剑指 Offer 14- I. 剪绳子
/// 题目：
/// 给你一根长度为 n 的绳子，请把绳子剪成整数长度的 m 段（m、n都是整数，n>1并且m>1），每段绳子的长度记为 k[0],k[1]...k[m-1] 。
/// 请问 k[0]*k[1]*...*k[m-1] 可能的最大乘积是多少？例如，当绳子的长度是8时，我们把它剪成长度分别为2、3、3的三段，此时得到的最大乘积是18。
/// 示例 1：
/// 输入: 2
/// 解释: 2 = 1 + 1, 1 × 1 = 1
/// 示例 2:
/// 输入: 10
/// 输出: 36
/// 解释: 10 = 3 + 3 + 4, 3 × 3 × 4 = 36
/// 提示：
/// 2 <= n <= 58
/// 
/// 动态规划
/// 1*(n-1)的最大 2*(n-2)的最大……
/// </summary>
public class Solution
{
    public int CuttingRope(int n)
    {
        int[] dp = new int[n + 1];

        //初始化
        dp[0] = 0;
        dp[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            int mid = (i + 1) / 2;
            //最多到中间即可
            for (int j = 1; j <= mid; j++)
            {
                dp[i] = Math.Max(Math.Max(dp[i], j * dp[i - j]), j * (i - j));
            }
        }
        return dp[n];
    }
}

/// <summary>
/// 题号：剑指 Offer 15. 二进制中1的个数
/// 题目：
/// 请实现一个函数，输入一个整数，输出该数二进制表示中 1 的个数。例如，把 9 表示成二进制是 1001，有 2 位是 1。因此，如果输入 9，则该函数输出 2。
/// 示例 1：
/// 输入：00000000000000000000000000001011
/// 输出：3
/// 解释：输入的二进制串 00000000000000000000000000001011 中，共有三位为 '1'。
/// 示例 2：
/// 输入：00000000000000000000000010000000
/// 输出：1
/// 解释：输入的二进制串 00000000000000000000000010000000 中，共有一位为 '1'。
/// 示例 3：
/// 输入：11111111111111111111111111111101
/// 输出：31
/// 解释：输入的二进制串 11111111111111111111111111111101 中，共有 31 位为 '1'。
/// </summary>
public class Solution
{
    public int HammingWeight(uint n)
    {
        int result = 0;
        int mask = 1;
        //int一共32位
        for (int i = 0; i < 32; i++)
        {
            if ((mask & n) != 0)
            {
                result++;
            }
            mask <<= 1;
        }
        return result;
    }
}

/// <summary>
/// 题号：剑指 Offer 16. 数值的整数次方
/// 题目：
/// 实现函数double Power(double base, int exponent)，求base的exponent次方。不得使用库函数，同时不需要考虑大数问题。
/// 示例 1:
/// 输入: 2.00000, 10
/// 输出: 1024.00000
/// 示例 2:
/// 输入: 2.10000, 3
/// 输出: 9.26100
/// 示例 3:
/// 输入: 2.00000, -2
/// 输出: 0.25000
/// 解释: 2 - 2 = 1 / 22 = 1 / 4 = 0.25
/// 说明:
/// -100.0 < x < 100.0
/// n 是 32 位有符号整数，其数值范围是 [−231, 231 − 1] 。
/// </summary>
public class Solution
{
    public double MyPow(double x, int n)
    {
        if (n == 0) return 1;
        if (n > 0)
        {
            return Pow(x, n);
        }
        else
        {
            return 1 / Pow(x, -n);
        }
    }

    private double Pow(double x, int n)
    {
        if (n == 1) return x;

        double y = Pow(x, n / 2);

        y *= y;

        return n % 2 != 0 ? y * x : y;
    }
}

/// <summary>
/// 题号：剑指 Offer 20. 表示数值的字符串
/// 题目：
/// 请实现一个函数用来判断字符串是否表示数值（包括整数和小数）。例如，字符串"+100"、"5e2"、"-123"、"3.1416"、"-1E-16"、"0123"都表示数值，但"12e"、"1a3.14"、"1.2.3"、"+-5"及"12e+5.4"都不是。
/// 
/// 状态机
/// </summary>
public class Solution
{
    public bool IsNumber(string s)
    {
        //构建哈希表
        Dictionary<int, Dictionary<char, int>> HashMap = new Dictionary<int, Dictionary<char, int>>();
        HashMap.Add(0, new Dictionary<char, int>() { { ' ', 0 }, { 's', 1 }, { 'd', 2 }, { '.', 4 } });
        HashMap.Add(1, new Dictionary<char, int>() { { 'd', 2 }, { '.', 4 } });
        HashMap.Add(2, new Dictionary<char, int>() { { 'd', 2 }, { '.', 3 }, { 'e', 5 }, { ' ', 8 } });
        HashMap.Add(3, new Dictionary<char, int>() { { 'd', 3 }, { 'e', 5 }, { ' ', 8 } });
        HashMap.Add(4, new Dictionary<char, int>() { { 'd', 3 } });
        HashMap.Add(5, new Dictionary<char, int>() { { 'd', 7 }, { 's', 6 } });
        HashMap.Add(6, new Dictionary<char, int>() { { 'd', 7 } });
        HashMap.Add(7, new Dictionary<char, int>() { { 'd', 7 }, { ' ', 8 } });
        HashMap.Add(8, new Dictionary<char, int>() { { ' ', 8 } });

        int p = 0;  //初始状态
        for (int i = 0; i < s.Length; i++)
        {
            char temp = ChangeChar(s[i]);
            //当不存在状态转移时直接返回false
            if (!HashMap[p].ContainsKey(temp)) return false;
            p = HashMap[p][temp];
        }

        return p == 2 || p == 3 || p == 7 || p == 8;
    }

    private char ChangeChar(char t)
    {
        if ('0' <= t && t <= '9') return 'd';
        if (t == '+' || t == '-') return 's';
        if (t == '.' || t == ' ') return t;
        if (t == 'e' || t == 'E') return 'e';
        else return '?';
    }
}

/// <summary>
/// 题号：剑指 Offer 26. 树的子结构
/// 题目：
/// 输入两棵二叉树A和B，判断B是不是A的子结构。(约定空树不是任意一个树的子结构)
/// B是A的子结构， 即 A中有出现和B相同的结构和节点值。
/// 例如:
/// 给定的树 A:
/// 3
/// / \
/// 4   5
/// / \
/// 1   2
/// 给定的树 B：
///    4 
///    /
///    1
///    返回 true，因为 B 与 A 的一个子树拥有相同的结构和节点值。
///    
/// 输入：A = [1,2,3], B = [3,1]
/// 输出：false
/// 
/// dfs 深度优先搜索
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public bool IsSubStructure(TreeNode A, TreeNode B)
    {
        if (A == null || B == null) return false;

        //自身递归,取左右值
        return DFS(A, B) || IsSubStructure(A.left, B) || IsSubStructure(A.right, B);
    }

    private bool DFS(TreeNode A, TreeNode B)
    {
        //超过B节点了，所以前面都匹配到了
        if (B == null) return true;

        //在不超过B节点时超过A节点一定为假
        if (A == null) return false;

        if (A.val != B.val) return false;

        return DFS(A.left, B.left) && DFS(A.right, B.right);
    }
}

/// <summary>
/// 题号：LCP 19. 秋叶收藏集
/// 题目：
/// 小扣出去秋游，途中收集了一些红叶和黄叶，他利用这些叶子初步整理了一份秋叶收藏集 leaves， 字符串 leaves 仅包含小写字符 r 和 y， 其中字符 r 表示一片红叶，字符 y 表示一片黄叶。
/// 出于美观整齐的考虑，小扣想要将收藏集中树叶的排列调整成「红、黄、红」三部分。每部分树叶数量可以不相等，但均需大于等于 1。每次调整操作，小扣可以将一片红叶替换成黄叶或者将一片黄叶替换成红叶。
/// 请问小扣最少需要多少次调整操作才能将秋叶收藏集调整完毕。
/// 示例 1：
/// 输入：leaves = "rrryyyrryyyrr"
/// 输出：2
/// 解释：调整两次，将中间的两片红叶替换成黄叶，得到 "rrryyyyyyyyrr"
/// 示例 2：
/// 输入：leaves = "ryr"
/// 输出：0
/// 解释：已符合要求，不需要额外操作
/// 提示：
/// 3 <= leaves.length <= 10^5
/// leaves 中只包含字符 'r' 和字符 'y'
/// 
/// 动态规划
/// 状态1：红；状态2：黄；状态3：红
/// dp[i][j]:0-i位置的叶子且第i位置叶子为状态j
/// dp[0][0] = isRed(0);
/// dp[i][0] = dp[i-1][0] + isRed(i) 前面为状态1的红时，i位置如果是状态0，前面一定为状态0
/// dp[i][1] = min{dp[i-1][0],dp[i-1][1]} + isYe(i) i位置为状态1 ，前面可以为状态0或1
/// dp[i][2] = min{dp[i-1][1],dp[i-1][2]} + idRed(i) i位置为状态2时，前面可以为状态1或2，不能为0，因为每种状态至少为1
/// </summary>
public class Solution
{
    public int MinimumOperations(string leaves)
    {
        int n = leaves.Length;
        int[,] dp = new int[n, 3];
        dp[0, 0] = isRed(leaves[0]);  //第一个位置一定为红
        dp[0, 1] = dp[0, 2] = dp[1, 2] = Int32.MaxValue;  //dp[1,2]肯定为正无穷，因为某种状态都至少为1
        dp[1, 0] = dp[0, 0] + isRed(leaves[1]);
        dp[1, 1] = Math.Min(dp[0, 0], dp[0, 1]) + isYe(leaves[1]);
        for (int i = 2; i < n; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + isRed(leaves[i]);
            dp[i, 1] = Math.Min(dp[i - 1, 0], dp[i - 1, 1]) + isYe(leaves[i]);
            dp[i, 2] = Math.Min(dp[i - 1, 1], dp[i - 1, 2]) + isRed(leaves[i]);
        }
        return dp[n - 1, 2];
    }

    private int isRed(char c)
    {
        return c == 'r' ? 0 : 1;
    }

    private int isYe(char c)
    {
        return c == 'y' ? 0 : 1;
    }
}