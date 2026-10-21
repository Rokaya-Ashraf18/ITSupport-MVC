using System;
using System.Collections.Generic;
using System.Linq;
using LibraryBooksSystem.Models;

namespace LibraryBooksSystem.Services
{
    public class MemberService
    {
        private List<Member> _members;
        private List<BorrowRecord> _records;

        public MemberService(List<Member> members, List<BorrowRecord> records)
        {
            _members = members;
            _records = records;
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Contains("@") && !email.Contains(" ");
        }

        public ServiceResult AddMember(int id, string name, string email)
        {
            if (id <= 0)
                return ServiceResult.Fail("Member ID must be greater than 0.");

            if (_members.Any(m => m.Id == id))
                return ServiceResult.Fail("A member with ID " + id + " already exists.");

            if (string.IsNullOrWhiteSpace(name))
                return ServiceResult.Fail("Name must not be empty.");

            if (!IsEmailValid(email))
                return ServiceResult.Fail("E-mail address is not valid.");

            Member member = new Member(id, name.Trim(), email.Trim());
            _members.Add(member);

            return ServiceResult.Ok("Member registered.");
        }

        public List<Member> GetAllMembers()
        {
            return _members.ToList();
        }

        public Member GetMemberById(int id)
        {
            return _members.FirstOrDefault(m => m.Id == id);
        }

        public List<Member> SearchMembers(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Member>();

            return _members
                .Where(m => m.Name.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase))
                .OrderBy(m => m.Name)
                .ToList();
        }

        public ServiceResult UpdateMember(int id, string name, string email)
        {
            Member member = GetMemberById(id);

            if (member == null)
                return ServiceResult.Fail("Member not found.");

            if (string.IsNullOrWhiteSpace(name))
                return ServiceResult.Fail("Name must not be empty.");

            if (!IsEmailValid(email))
                return ServiceResult.Fail("E-mail address is not valid.");

            member.Name = name.Trim();
            member.Email = email.Trim();

            return ServiceResult.Ok("Member updated.");
        }

        public ServiceResult DeleteMember(int id)
        {
            Member member = GetMemberById(id);

            if (member == null)
                return ServiceResult.Fail("Member not found.");

            bool hasBorrowedBook = _records.Any(r => r.MemberId == id && r.ReturnDate == null);

            if (hasBorrowedBook)
                return ServiceResult.Fail("This member still holds borrowed books and cannot be deleted.");

            _members.Remove(member);

            return ServiceResult.Ok("Member deleted.");
        }
    }
}