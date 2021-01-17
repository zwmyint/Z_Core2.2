using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.APIClient.Models
{
    public class m_cls_ZGithub
    {
        //
        public string login { get; set; }
        public string id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }

        public string type { get; set; }
        public bool site_admin { get; set; }
        public string name { get; set; }
        public string location { get; set; }

        public bool hireable { get; set; }
        //
    }
}


//{
//  "login": "zwmyint",
//  "id": 45326766,
//  "node_id": "MDQ6VXNlcjQ1MzI2NzY2",
//  "avatar_url": "https://avatars0.githubusercontent.com/u/45326766?v=4",
//  "gravatar_id": "",
//  "url": "https://api.github.com/users/zwmyint",
//  "html_url": "https://github.com/zwmyint",
//  "followers_url": "https://api.github.com/users/zwmyint/followers",
//  "following_url": "https://api.github.com/users/zwmyint/following{/other_user}",
//  "gists_url": "https://api.github.com/users/zwmyint/gists{/gist_id}",
//  "starred_url": "https://api.github.com/users/zwmyint/starred{/owner}{/repo}",
//  "subscriptions_url": "https://api.github.com/users/zwmyint/subscriptions",
//  "organizations_url": "https://api.github.com/users/zwmyint/orgs",
//  "repos_url": "https://api.github.com/users/zwmyint/repos",
//  "events_url": "https://api.github.com/users/zwmyint/events{/privacy}",
//  "received_events_url": "https://api.github.com/users/zwmyint/received_events",
//  "type": "User",
//  "site_admin": false,
//  "name": "ZAW WIN MYINT",
//  "company": null,
//  "blog": "https://io01.app",
//  "location": "Singapore",
//  "email": null,
//  "hireable": true,
//  "bio": "To Become a Full Stack Developer",
//  "twitter_username": null,
//  "public_repos": 298,
//  "public_gists": 0,
//  "followers": 2,
//  "following": 27,
//  "created_at": "2018-11-25T07:01:57Z",
//  "updated_at": "2021-01-16T16:42:49Z"
//}