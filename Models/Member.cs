namespace TutorialMVC.Models
{
    public class Member
    {
        // ID
        public int? id { get; set; }
        // 氏名
        public string? Name { get; set; }
        // 登録日
        public DateTime registDate { get; set; } = DateTime.Now;
    }
}
