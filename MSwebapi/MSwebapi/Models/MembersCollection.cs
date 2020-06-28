using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace MSwebapi.Models
{
    public class MembersCollection
    {
        public ObjectId _id { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
    public class AddMemberRequest
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
    public class AddMemberResponse
    {
        public bool ok { get; set; }
        public string errMsg { get; set; }
        public AddMemberResponse()
        {
            this.ok = true;
            this.errMsg = "";
        }
    }
    public class EditMemberRequest
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
    public class EditMemberResponse
    {
        public bool ok { get; set; }
        public string errMsg { get; set; }
        public EditMemberResponse()
        {
            this.ok = true;
            this.errMsg = "";
        }
    }
    public class DeleteMemberResponse
    {
        public bool ok { get; set; }
        public string errMsg { get; set; }
        public DeleteMemberResponse()
        {
            this.ok = true;
            this.errMsg = "";
        }
    }
    public class GetMemberListResponse
    {
        public bool ok { get; set; }
        public string errMsg { get; set; }
        public List<MemberInfo> member_list { get; set; }
        public GetMemberListResponse()
        {
            this.ok = true;
            this.errMsg = "";
            member_list = new List<MemberInfo>();
        }
    }
    public class MemberInfo
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
    public class GetMemberInfoResponse
    {
        public bool ok { get; set; }
        public string errMsg { get; set; }
        public MemberInfo data { get; set; }
        public GetMemberInfoResponse()
        {
            this.ok = true;
            this.errMsg = "";
            this.data = new MemberInfo();
        }
    }
}