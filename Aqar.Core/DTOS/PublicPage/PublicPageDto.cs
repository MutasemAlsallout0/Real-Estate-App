using Aqar.Core.DTOS.Following;

namespace Aqar.Core.DTOS.PublicPage
{
    public class PublicPageDto
    {
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public string Name { get; set; }
        public bool ActiveState { get; set; }
        public string UserId { get; set; }
        public List<FollowingDto> FollowingDtos { get; set; }
    }
}