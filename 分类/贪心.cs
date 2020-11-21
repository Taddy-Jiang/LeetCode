using System;
using System.Collections;

/// <summary>
/// 贪心算法：暴力解法
/// 模拟实际情况不断试错
/// </summary>


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