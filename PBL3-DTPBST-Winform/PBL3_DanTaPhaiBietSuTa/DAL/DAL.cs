using PBL3_DanTaPhaiBietSuTa.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace PBL3_DanTaPhaiBietSuTa
{
    public class DAL : InterfaceDAL
    {
        public static DAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static DAL _Instance;
        private DAL() { }
        public bool AddUser(UserInfo user)
        {
            if (IsExistUser(user.Username)) return false;
            else
            {
                using (DB db = new DB())
                {
                    db.UserInfos.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public bool CheckLogin(string userName, string passWord)
        {
            using (var db = new DB())
            {
                var User = db.UserInfos.Where(s => s.Username == userName && s.Password == passWord).FirstOrDefault<UserInfo>();
                if (User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsExistUser(string userName)
        {
            using (var db = new DB())
            {
                var User = db.UserInfos.Where(s => s.Username == userName).FirstOrDefault<UserInfo>();
                if (User != null)
                {

                    return true; //Đã tồn tại
                }
                else
                {
                    return false;
                }
            }

        }
        public List<UserInfo> GetListUserInfo()
        {
            List<UserInfo> list = new List<UserInfo>();
            using (DB db = new DB())
            {
                list = db.UserInfos.ToList();
            }
            return list;
        }
        public bool UpdateUser(UserInfo usr)
        {
            using(DB db = new DB())
            {
                var user = db.UserInfos.Find(usr.UserID);
                if (user == null) return false;
                else
                {
                    user.Email = usr.Email;
                    user.Name = usr.Name;
                    user.Password = usr.Password;
                    db.SaveChanges();
                    return true;
                }
            }    
        }
        public UserInfo GetUserInfoByUsername(string username)
        {
            using (DB db = new DB())
            {
                UserInfo user = db.UserInfos.Where(s => s.Username == username).FirstOrDefault();
                return user ;
            }
        }
        public Video GetVideo(int stageID)
        {
            using (DB db = new DB())
            {
                Stage stage = db.Stages.Where(s => s.StageID == stageID).FirstOrDefault();
                Video video = stage.Video;
                return video;
            }
        }
        public List<Question> GetListQuestion(int stageID)
        {
            using (DB db = new DB())
            {
                List<Question> list = db.Stages.Find(stageID).Questions.ToList();
                
                return list;
            }
        }
        public List<Question> GetListQuestionByTimeStop(int stageID,int TimeStop)
        {
            using (DB db = new DB())
            {
                List<Question> list = new List<Question>();
                List<Question> list1 = db.Stages.Find(stageID).Questions.ToList();
                foreach (var question in list1)
                {
                    if(question.TimeStop == TimeStop)
                    list.Add(question);
                }    
                return list;
            }
        }
        public void UpdatePointTable(GameProcess gameProcess)
        {
            using (DB db = new DB())
            {
                bool kt = true;
                foreach(var point in db.Points)
                {
                    if(point.StageID == gameProcess.StageID && point.UserID == gameProcess.UserID)
                    {
                        if (point.point < gameProcess.Point && gameProcess.IsPass==true) point.point = gameProcess.Point;
  
                        kt = false;
                        break;
                    }    
                }
    
                if (kt && gameProcess.IsPass == true) db.Points.Add(new Point
                {
                    StageID = gameProcess.StageID,
                    UserID = gameProcess.UserID,
                    point = gameProcess.Point
                });
                db.SaveChanges();
                
            }    
        }
        public Standing GetStandingByUserID(int UserID)
        {
            using(DB db = new DB())
            {
                Standing standing = new Standing()
                {
                    UserID = UserID,
                    Point = 0,
                    StageID = 0
                };
                
                foreach (var point in db.Points)
                {
                    if(point.UserID == UserID)
                    {
                        standing.Point += point.point;
                        if (point.StageID > standing.StageID) standing.StageID = point.StageID;
                    }    
                }    
                return standing;
            }    
        }
        public List<Standing> GetListStanding()
        {
            List<Standing> list = new List<Standing>();
            //123
            using(DB db = new DB())
            {
                foreach(var user in db.UserInfos)
                {
                    var standing = GetStandingByUserID(user.UserID);
                    if(standing.StageID != 0) list.Add(standing);
                }    
            }    
            return list;
        }
        public UserInfo GetUserInfoByUserID(int userID)
        {
            using (DB db = new DB())
            {
                UserInfo user = db.UserInfos.Where(s => s.UserID == userID).FirstOrDefault();
                return user;
            }
        }
        public string Hash(string str)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public List<Question> GetAllQuestion()
        {
            DB db = new DB();
            return db.Questions.Select(p => p).ToList();
        }
        public bool DeleteUser(int userID)
        {
            using (DB db = new DB())
            {
                var user = db.UserInfos.Find(userID);
                if (user == null) return false;
                else
                {
                    db.UserInfos.Remove(user);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public Question GetQuestionByQuestionID(int questionID)
        {
            Question question = new Question();
            var listAllQuestion = GetAllQuestion();
            foreach (var q in listAllQuestion)
            {
                if (q.QuestionID == questionID)
                    question = q;
            }
            return question;
        }
        public bool DeleteQuestion(int questionID)
        {
            using (DB db = new DB())
            {
                var question = db.Questions.Find(questionID);
                if (question == null) return false;
                else
                {
                    db.Questions.Remove(question);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public bool UpdateQuestion(Question question)
        {
            using (DB db = new DB())
            {
                var q = db.Questions.Find(question.QuestionID);
                if (q == null)
                    return false;
                else
                {
                    q.QuestionContent = question.QuestionContent;
                    q.KeyA = question.KeyA;
                    q.KeyB = question.KeyB;
                    q.KeyC = question.KeyC;
                    q.KeyD = question.KeyD;
                    q.Answer = question.Answer;
                    db.SaveChanges();
                    return true;
                }
            }
        }





    }
}
