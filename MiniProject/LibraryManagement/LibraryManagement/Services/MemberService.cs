using LibraryManagement.Models;
namespace LibraryManagement.Services;

internal class MemberService
{
    private readonly List<Member> members;
    public MemberService(List<Member> members)
    {
        this.members = members;
    }
    public Member? GetMemberById(int id) => members.FirstOrDefault(m => m.Id == id);
    public bool Add(Member member)
    {
        if (members.Any(m => m.Id == member.Id))
            return false;
        members.Add(member);
        return true;
    }
    public bool Update(int id, string name, string email)
    {
        var m = GetMemberById(id);
        if (m == null) return false;
        m.Name = name;
        m.Email = email;
        return true;
    }
    public bool Delete(int id, List<BorrowRecord> records)
    {
        var m = GetMemberById(id);
        if (m == null || records.Any(r => r.MemberID == id && r.ReturnDate == null))
            return false;
        members.Remove(m);
        return true;
    }
    public IReadOnlyList<Member> All() => members.OrderBy(m => m.Id).ToList();
}
