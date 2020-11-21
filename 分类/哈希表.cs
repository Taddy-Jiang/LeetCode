using System.Collections.Generic;

/// <summary>
/// 技巧
/// 哈希表
/// 出现频率、小写字母，这种关键词用哈希表
/// </summary>

/// <summary>
/// 题号：1. 两数之和
/// 题目：
/// 给定一个整数数组 nums 和一个目标值 target，请你在该数组中找出和为目标值的那 两个 整数，并返回他们的数组下标。
/// 你可以假设每种输入只会对应一个答案。但是，数组中同一个元素不能使用两遍。
/// 示例:
/// 给定 nums = [2, 7, 11, 15], target = 9
/// 因为 nums[0] + nums[1] = 2 + 7 = 9
/// 所以返回[0, 1]
/// 
/// 哈希表
/// </summary>
public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        int n = nums.Length;
        Dictionary<int, int> temp = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            if (temp.ContainsKey(target - nums[i]))
            {
                return new int[] { temp[target - nums[i]], i };
            }
            if (!temp.ContainsKey(nums[i]))
            {
                temp.Add(nums[i], i);
            }
        }
        return new int[0];
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