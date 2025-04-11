using PBL3_DanTaPhaiBietSuTa.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3_DanTaPhaiBietSuTa
{
    class BLL
    {
        public static BLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static BLL _Instance;
        private BLL()
        {

        }
        /// <summary>
        /// Kiểm tra xem userName và passWord đã tồn tại trong database hay chưa.
        /// </summary>
        /// <param name="userName">Tên tài khoản</param>
        /// <param name="passWord">Mật khẩu</param>
        /// <returns>true nếu trong database tồn tại tài khoản, false nếu ngược lại</returns>
        public bool CheckLogin(string userName, string passWord)
        {
            return DAL.Instance.CheckLogin(userName, passWord);
        }
        /// <summary>
        /// Thêm tài khoản vào database.
        /// </summary>
        /// <param name="user">Đối tượng UserInfo để thêm vào database</param>
        /// <returns>true nếu thêm tài khoản thành công, false nếu ngược lại</returns>
        public bool AddNewUser(UserInfo user)
        {
            return DAL.Instance.AddUser(user);
        }
        /// <summary>
        /// Trả về 1 danh sách chứa thông tin của toàn bộ user trong database.
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetListUserInfor()
        {
            return DAL.Instance.GetListUserInfo();
        }
        /// <summary>
        /// Cập nhập thông tin của 1 user trong database.
        /// </summary>
        /// <param name="user">Đối tượng user cần cập nhập</param>
        /// <returns>true nếu cập nhập thành công, false nếu ngược lại</returns>
        public bool UpdateUserInfor(UserInfo user)
        {
            return DAL.Instance.UpdateUser(user);
        }
        /// <summary>
        /// Lấy thông tin của 1 user.
        /// </summary>
        /// <param name="userName">Tên tài khoản</param>
        /// <returns>Đối tượng UserInfo chứa thông tin của user</returns>
        public UserInfo GetUserInforByUserName(string userName)
        {
            return DAL.Instance.GetUserInfoByUsername(userName);
        }
        /// <summary>
        /// Lấy 1 Video ứng với stageID.
        /// </summary>
        /// <param name="stageID">ID của mản chơi.</param>
        /// <returns>Đối tượng Video ứng với stageID</returns>
        public Video GetVideo(int stageID)
        {
            return DAL.Instance.GetVideo(stageID);
        }
        /// <summary>
        /// Lấy danh sách câu hỏi ứng với stageID.
        /// </summary>
        /// <param name="stageID">ID của màn chơi</param>
        /// <returns>List câu hỏi ứng với stageID</returns>
        public List<Question> GetQuestionsByStage(int stageID)
        {
            return DAL.Instance.GetListQuestion(stageID);
        }
        /// <summary>
        /// Lấy ngẫu nhiên 1 câu hỏi trong List câu hỏi ứng với stageID có cùng mốc thời gian.
        /// </summary>
        /// <param name="stageID">ID của màn chơi</param>
        /// <param name="timeStop">Mốc thời gian</param>
        /// <returns>1 câu hỏi ngẫu nhiên.</returns>
        public Question GetRandomQuestionByTimeStop(int stageID, int timeStop)
        {
            var listQuestion = DAL.Instance.GetListQuestionByTimeStop(stageID, timeStop);
            Random rd = new Random();
            int idQuestion = rd.Next(0, listQuestion.Count - 1);
            return listQuestion[idQuestion];
        }
        /// <summary>
        /// Lấy thông tin về vị thứ của user.
        /// </summary>
        /// <param name="userID">ID của user</param>
        /// <returns>Đối tượng Standing.</returns>
        public Standing GetStandingByUserID(int userID)
        {
            return DAL.Instance.GetStandingByUserID(userID);
        }
        /// <summary>
        /// Sắp xếp thứ hạng của các user.
        /// </summary>
        /// <returns>List thứ hạng của các user sau khi được sắp xếp</returns>
        public List<Standing> SortListStandings()
        {
            List<Standing> standings = DAL.Instance.GetListStanding();
            standings = standings.OrderByDescending(p => p.Point).ToList();
            return standings;
        }
        /// <summary>
        /// Lấy thứ hạng của user.
        /// </summary>
        /// <param name="userID">ID của user</param>
        /// <returns>Thứ hạng của user nếu tồn tại, ngược lại trả về -1.</returns>
        public int GetRankByUserID(int userID)
        {
            int pos = 1;
            List<Standing> standings = SortListStandings();
            foreach (var i in standings)
            {
                if (i.UserID == userID)
                    return pos;
                pos++;
            }
            return -1;
        }
        /// <summary>
        /// Cập nhập điểm của user.
        /// </summary>
        /// <param name="gameProcess">Tiến trình chơi của user.</param>
        public void UpdatePoint(GameProcess gameProcess)
        {
            DAL.Instance.UpdatePointTable(gameProcess);
        }
        /// <summary>
        /// Lấy thông tin của user ứng với userID.
        /// </summary>
        /// <param name="userID">ID của user</param>
        /// <returns>Đối tượng UserInfo chứa thông tin của user.</returns>
        public UserInfo GetUserInfoByUserID(int userID)
        {
            return DAL.Instance.GetUserInfoByUserID(userID);
        }
        /// <summary>
        /// Lấy danh sách toàn bộ câu hỏi có trong database.
        /// </summary>
        /// <returns>List toàn bộ câu hỏi có trong Database</returns>
        public List<Question> GetAllQuestion()
        {
            return DAL.Instance.GetAllQuestion();
        }
        /// <summary>
        /// Xóa 1 user ứng với userID
        /// </summary>
        /// <param name="userID">ID của user.</param>
        /// <returns>true nếu xóa user thành công, false nếu ngược lại</returns>
        public bool DeleteUser(int userID)
        {
            return DAL.Instance.DeleteUser(userID);
        }
        /// <summary>
        /// Lấy 1 câu hỏi ứng với questionID
        /// </summary>
        /// <param name="questionID">ID của câu hỏi</param>
        /// <returns>Đối tượng Question ứng với questionID</returns>
        public Question GetQuestionByQuestionID(int questionID)
        {
            return DAL.Instance.GetQuestionByQuestionID(questionID);
        }
        /// <summary>
        /// Xóa 1 câu hỏi ứng với questionID.
        /// </summary>
        /// <param name="questionID">ID của câu hỏi</param>
        /// <returns>true nếu xóa câu hỏi thành công, false nếu ngược lại</returns>
        public bool DeleteQuestion(int questionID)
        {
            return DAL.Instance.DeleteQuestion(questionID);
        }
        /// <summary>
        /// Cập nhập 1 câu hỏi trong database.
        /// </summary>
        /// <param name="question">Đối tượng câu hỏi cần cập nhập</param>
        /// <returns>true nếu cập nhập thành công, false nếu ngược lại</returns>
        public bool UpdateQuestion(Question question)
        {
            return DAL.Instance.UpdateQuestion(question);
        }
    }
}
