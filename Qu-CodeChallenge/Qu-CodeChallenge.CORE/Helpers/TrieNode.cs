namespace Qu_CodeChallenge.CORE.Helpers;

public class TrieNode
{
    public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
    public int OccurrenceCount = 0;  // To count occurrences of each word
    public bool IsEndOfWord = false;
}