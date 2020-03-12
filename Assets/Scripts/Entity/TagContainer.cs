using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity
{
    /// <summary>
    /// 태그와 태그 갯수
    /// </summary>
    public class TagContainer
    {
        private Tag tag;
        public Tag Tag { get { return Tag; } }

        private int tagCount;
        public int TagCount { set { tagCount = value; } get { return tagCount; } }

        public TagContainer(Tag myTag)
        {
            tag = myTag;
            TagCount = 0;
        }
    }
}