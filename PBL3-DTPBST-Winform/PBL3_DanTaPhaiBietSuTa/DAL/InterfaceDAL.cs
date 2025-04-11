using PBL3_DanTaPhaiBietSuTa.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3_DanTaPhaiBietSuTa
{
    interface InterfaceDAL
    {
        bool AddUser(UserInfo user);
        bool CheckLogin(string userName, string passWord);
        bool IsExistUser(string userName);
        List<UserInfo> GetListUserInfo();
        bool UpdateUser(UserInfo usr);
        UserInfo GetUserInfoByUsername(string username);
        Video GetVideo(int stageID);
        List<Question> GetListQuestion(int stageID);
        List<Question> GetListQuestionByTimeStop(int stageID, int TimeStop);
        void UpdatePointTable(GameProcess gameProcess);
        Standing GetStandingByUserID(int UserID);
        List<Standing> GetListStanding();
        UserInfo GetUserInfoByUserID(int userID);
        List<Question> GetAllQuestion();
        bool DeleteUser(int userID);
        Question GetQuestionByQuestionID(int questionID);
        bool DeleteQuestion(int questionID);
        bool UpdateQuestion(Question question);
    }
}
